using AutoMapper;
using HoursBank.Domain.Dtos;
using HoursBank.Domain.Interfaces.Repository;
using HoursBank.Domain.Interfaces.Services;
using HoursBank.Domain.Responses;
using HoursBank.Domain.Security;
using HoursBank.Util;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace HoursBank.Service.Services
{
    public class LoginService : ILoginService
    {
        private IUserRepository _repository;
        private ITeamService _teamService;
        private ICoordinatorService _coordinatorService;
        private readonly IMapper _mapper;
        private SigningConfigurations _signingConfigurations;
        private TokenConfigurations _tokenConfiguration;
        private IConfiguration _configuration { get; }

        public LoginService(IUserRepository repository,
            ITeamService teamService,
            ICoordinatorService coordinatorService,
            IMapper mapper,
            SigningConfigurations signingConfigurations,
            TokenConfigurations tokenConfiguration,
            IConfiguration configuration)
        {
            _repository = repository;
            _teamService = teamService;
            _coordinatorService = coordinatorService;
            _mapper = mapper;
            _signingConfigurations = signingConfigurations;
            _tokenConfiguration = tokenConfiguration;
            _configuration = configuration;
        }

        public async Task<bool> ValidateToken(string token)
        {
            var result = this.DecriptToken(token);

            if (result != null && result.Contains("Error")) {
                return false;
            }
            return true;
        }

        public async Task<object> Login(LoginDto login)
        {
            if (login != null && !string.IsNullOrWhiteSpace(login.Email)
                && !string.IsNullOrWhiteSpace(login.Password))
            {
                var user = await _repository.Login(login.Email, login.Password)??null;
                if (user == null)
                {
                    return new
                    {
                        authenticated = false,
                        message = "Falha na autenticação"
                    };
                }

                user.Password = login.Password;
                UserDto userDto = _mapper.Map<UserDto>(user);
                TeamDto teamDto = new TeamDto();
                if (userDto.TeamId.HasValue) 
                {
                    teamDto = _mapper.Map<TeamDto>(await _teamService.Get(userDto.TeamId.Value));
                }

                List<CoordinatorResponse> coordinators = 
                    (List<CoordinatorResponse>) await _coordinatorService.GetByUser(userDto.Id);
               
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(user.Email),
                    new []
                    {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
                    }
                );
                DateTime createDate = DateTime.Now;
                DateTime expirationDate = createDate + TimeSpan.FromSeconds(_tokenConfiguration.Seconds);
                var handler = new JwtSecurityTokenHandler();
                
                var token = "";
                if (user.Active.HasValue && user.Active.Value)
                {
                    token = CreateToken(identity, createDate, expirationDate, handler);
                }

                return SuccessObject(createDate, expirationDate, token, userDto, teamDto, coordinators);
            }
            return null;
        }

        public string ReadToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token)) { return ""; }
            return this.DecriptToken(token);
        }

        private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
            {
                Issuer = _tokenConfiguration.Issuer,
                Audience = _tokenConfiguration.Audience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate,
            });

            return handler.WriteToken(securityToken);
        }

        private string DecriptToken(string token)
        {
            try
            {
                SecurityKey signingKey = _signingConfigurations.SigningCredentials.Key;

                // Opções de validação do token
                TokenValidationParameters validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = _tokenConfiguration.Issuer,
                    ValidAudience = _tokenConfiguration.Audience,
                    IssuerSigningKey = signingKey
                };
                var handler = new JwtSecurityTokenHandler();
                SecurityToken decryptedToken;
                ClaimsPrincipal claimsPrincipal = handler.ValidateToken(token, validationParameters, out decryptedToken);
                return claimsPrincipal.Identity.Name;
            }
            catch (SecurityTokenException ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        private object SuccessObject(DateTime createDate, DateTime expirationDate, string token, UserDto user, TeamDto team, List<CoordinatorResponse> coordinators)
        {
            return new
            {
                authenticated = true,
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = token,
                user = user,
                team = team,
                coordinators = coordinators,
                message = "Usuário autenticado com sucesso"
            };
        }
    }
}
