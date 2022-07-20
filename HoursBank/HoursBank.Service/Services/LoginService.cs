using HoursBank.Domain.Dtos;
using HoursBank.Domain.Interfaces.Repository;
using HoursBank.Domain.Interfaces.Services;
using HoursBank.Domain.Security;
using Microsoft.Extensions.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace HoursBank.Service.Services
{
    public class LoginService : ILoginService
    {
        private IUserRepository _repository;
        private SigningConfigurations _signingConfigurations;
        private TokenConfigurations _tokenConfiguration;
        private IConfiguration _configuration { get; }

        public LoginService(IUserRepository repository,
            SigningConfigurations signingConfigurations,
            TokenConfigurations tokenConfiguration,
            IConfiguration configuration)
        {
            _repository = repository;
            _signingConfigurations = signingConfigurations;
            _tokenConfiguration = tokenConfiguration;
            _configuration = configuration;
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

                var token = CreateToken(identity, createDate, expirationDate, handler);
                return SuccessObject(createDate, expirationDate, token, login);
            }
            return null;
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

        private object SuccessObject(DateTime createDate, DateTime expirationDate, string token, LoginDto user)
        {
            return new
            {
                authenticated = true,
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                acessToken = token,
                userEmail = user.Email,
                message = "Usuário autenticado com sucesso"
            };
        }
    }
}
