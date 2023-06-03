using HoursBank.Api.Controllers;
using HoursBank.Domain.Dtos;
using HoursBank.Domain.Interfaces.Services;
using HoursBank.Domain.Responses;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace HoursBank.Api.Test.User
{
    public class UserTest
    {
        private UserController? _controller;

        [Fact(DisplayName = "Possible get a user")]
        public async Task PossibleGetUser()
        {

            var randomGenerator = new Random();
            var id = randomGenerator.Next(1, 999);
            var email = Faker.Internet.Email();
            var name = Faker.Name.FullName();

            #region Get Test
            var serviceMock = new Mock<IUserService>();
            var loginServiceMock = new Mock<ILoginService>();

            serviceMock.Setup(m => m.Get(It.IsAny<int>())).ReturnsAsync(
                new UserResponse
                {
                    Id = id,
                    Email = email,
                    Name = name,
                    Active = true,
                    Admin = false,
                    TeamId = 2,
                    Hours = 0
                }
            );

            _controller = new UserController(serviceMock.Object, loginServiceMock.Object);
            Mock<IUrlHelper> url = new Mock<IUrlHelper>();
            url.Setup(u => u.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost:5000");
            _controller.Url = url.Object;

            var result = await _controller.Get(id);
            Assert.True(result is OkObjectResult);

            var userResponse = ((OkObjectResult)result).Value as UserResponse;

            Assert.NotNull(userResponse);
            Assert.Equal(userResponse.Name, name);
            Assert.Equal(userResponse.Email, email);
            Assert.True(userResponse.Active.HasValue && userResponse.Active.Value);
            Assert.False(userResponse.Admin);
            Assert.NotNull(userResponse.TeamId);
            #endregion

            #region Bad Request Test
            _controller = new UserController(serviceMock.Object, loginServiceMock.Object);
            _controller.ModelState.AddModelError("Id", "Campo obrigatório!");
            _controller.Url = url.Object;

            result = await _controller.Get(0);
            Assert.True(result is BadRequestObjectResult);
            #endregion
        }

        [Fact(DisplayName = "Possible get all users")]
        public async Task PossibleGetAllUsers()
        {
            var listUsers = new List<UserResponse>();
            var randomGenerator = new Random();
            
            for (var i = 0; i < 10; i++)
            {
                var user = new UserResponse
                {
                    Id = randomGenerator.Next(i * 10, 9 + (i * 10)),
                    Email = Faker.Internet.Email(),
                    Name = Faker.Name.FullName(),
                    Active = true,
                    Admin = false,
                    TeamId = 2,
                    Hours = 0
                };
                listUsers.Add(user);
            }

            #region GetAll Test
            var serviceMock = new Mock<IUserService>();
            var loginServiceMock = new Mock<ILoginService>();
            serviceMock.Setup(m => m.GetAll()).ReturnsAsync( listUsers );

            _controller = new UserController(serviceMock.Object, loginServiceMock.Object);
            Mock<IUrlHelper> url = new Mock<IUrlHelper>();
            url.Setup(u => u.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost:5000");
            _controller.Url = url.Object;

            var result = await _controller.GetAll();
            Assert.True(result is OkObjectResult);

            var listUserResponse = ((OkObjectResult)result).Value as List<UserResponse>;

            Assert.NotNull(listUserResponse);
            Assert.True(listUserResponse.Count == 10);
            #endregion

            #region Bad Request Test
            _controller = new UserController(serviceMock.Object, loginServiceMock.Object);
            _controller.ModelState.AddModelError("Id", "Campo obrigatório!");
            _controller.Url = url.Object;

            result = await _controller.GetAll();
            Assert.True(result is BadRequestObjectResult);
            #endregion
        }

        [Fact(DisplayName = "Possible get users by team")]
        public async Task PossibleGetUsersByTeam()
        {
            var listUsers = new List<UserResponse>();
            var randomGenerator = new Random();
            var teamId = 2;
            
            for (var i = 0; i < 5; i++)
            {
                var user = new UserResponse
                {
                    Id = randomGenerator.Next(i * 10, 9 + (i * 10)),
                    Email = Faker.Internet.Email(),
                    Name = Faker.Name.FullName(),
                    Active = true,
                    Admin = false,
                    TeamId = 2,
                    Hours = 0
                };
                listUsers.Add(user);
            }

            #region Get by team Test
            var serviceMock = new Mock<IUserService>();
            var loginServiceMock = new Mock<ILoginService>();
            serviceMock.Setup(m => m.GetByTeam(It.IsAny<int>())).ReturnsAsync( listUsers );

            _controller = new UserController(serviceMock.Object, loginServiceMock.Object);
            Mock<IUrlHelper> url = new Mock<IUrlHelper>();
            url.Setup(u => u.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost:5000");
            _controller.Url = url.Object;

            var result = await _controller.GetByTeam(teamId);
            Assert.True(result is OkObjectResult);

            var listUserResponse = ((OkObjectResult)result).Value as List<UserResponse>;

            Assert.NotNull(listUserResponse);
            Assert.True(listUserResponse.Count == 5);
            Assert.True(listUserResponse.Where(u => u.TeamId != teamId).ToList().Count == 0);
            #endregion

            #region Bad Request Test
            _controller = new UserController(serviceMock.Object, loginServiceMock.Object);
            _controller.ModelState.AddModelError("Id", "Campo obrigatório!");
            _controller.Url = url.Object;

            result = await _controller.GetByTeam(0);
            Assert.True(result is BadRequestObjectResult);
            #endregion
        }

        [Fact(DisplayName = "Possible get a user by email")]
        public async Task PossibleGetByEmailUser()
        {

            var randomGenerator = new Random();
            var id = randomGenerator.Next(1, 999);
            var email = Faker.Internet.Email();
            var name = Faker.Name.FullName();

            #region Get Test
            var serviceMock = new Mock<IUserService>();
            var loginServiceMock = new Mock<ILoginService>();
            serviceMock.Setup(m => m.GetByEmail(It.IsAny<string>())).ReturnsAsync(
                new UserResponse
                {
                    Id = id,
                    Email = email,
                    Name = name,
                    Active = true,
                    Admin = false,
                    TeamId = 2,
                    Hours = 0
                }
            );

            _controller = new UserController(serviceMock.Object, loginServiceMock.Object);
            Mock<IUrlHelper> url = new Mock<IUrlHelper>();
            url.Setup(u => u.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost:5000");
            _controller.Url = url.Object;

            var result = await _controller.GetByEmail(email);
            Assert.True(result is OkObjectResult);

            var userResponse = ((OkObjectResult)result).Value as UserResponse;

            Assert.NotNull(userResponse);
            Assert.Equal(userResponse.Name, name);
            Assert.Equal(userResponse.Email, email);
            Assert.True(userResponse.Active);
            Assert.False(userResponse.Admin);
            Assert.NotNull(userResponse.TeamId);
            #endregion

            #region Bad Request Test
            _controller = new UserController(serviceMock.Object, loginServiceMock.Object);
            _controller.ModelState.AddModelError("Email", "Campo obrigatório!");
            _controller.Url = url.Object;

            result = await _controller.GetByEmail(string.Empty);
            Assert.True(result is BadRequestObjectResult);
            #endregion
        }

        [Fact(DisplayName = "Possible create a user")]
        public async Task PossibleCreateUser()
        {
            var serviceMock = new Mock<IUserService>();
            var loginServiceMock = new Mock<ILoginService>();

            var email = Faker.Internet.Email();
            var randomGenerator = new Random();
            var number = randomGenerator.Next(1, 999);
            var password = Faker.Name.First() + number.ToString();
            var name = Faker.Name.FullName();

            #region Created Test
            serviceMock.Setup(m => m.Post(It.IsAny<UserDto>())).ReturnsAsync(
                new UserResponse
                {
                    Id = randomGenerator.Next(1, 9),
                    Email = email,
                    Name = name,
                    Active = false,
                    Admin = false,
                    TeamId = null,
                    Hours = 0
                }
            );

            var userDto = new UserDto
            {
                Name = name,
                Email = email,
                Password = password
            };

            _controller = new UserController(serviceMock.Object, loginServiceMock.Object);
            Mock<IUrlHelper> url = new Mock<IUrlHelper>();
            url.Setup(u => u.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost:5000");
            _controller.Url = url.Object;

            var result = await _controller.Post(userDto);
            Assert.True(result is ObjectResult);

            var userResponse = ((ObjectResult)result).Value as UserResponse;

            Assert.NotNull(userResponse);
            Assert.Equal(userResponse.Name, userDto.Name);
            Assert.Equal(userResponse.Email, userDto.Email);
            Assert.False(userResponse.Active);
            Assert.False(userResponse.Admin);
            Assert.Null(userResponse.TeamId);
            #endregion

            #region Bad Request Test
            _controller = new UserController(serviceMock.Object, loginServiceMock.Object);
            _controller.ModelState.AddModelError("Name", "Campo obrigatório!");
            _controller.Url = url.Object;

            result = await _controller.Post(userDto);
            Assert.True(result is BadRequestObjectResult);
            #endregion
        }

        [Fact(DisplayName = "Possible update a user")]
        public async Task PossibleUpdateUser()
        {
            var serviceMock = new Mock<IUserService>();
            var loginServiceMock = new Mock<ILoginService>();

            var randomGenerator = new Random();
            var id = randomGenerator.Next(1, 9);
            var email = Faker.Internet.Email();
            var number = randomGenerator.Next(1, 999);
            var password = Faker.Name.First() + number.ToString();
            var name = Faker.Name.FullName();

            #region Created Test
            serviceMock.Setup(m => m.Put(It.IsAny<UserDto>())).ReturnsAsync(
                new UserResponse
                {
                    Id = id,
                    Email = email,
                    Name = name,
                    Active = true,
                    Admin = false,
                    TeamId = 2,
                    Hours = 0
                }
            );

            var userDto = new UserDto
            {
                Id = id,
                Name = name,
                Email = email,
                Password = password,
                Active = true,
                TeamId = 2
            };

            _controller = new UserController(serviceMock.Object, loginServiceMock.Object);
            Mock<IUrlHelper> url = new Mock<IUrlHelper>();
            url.Setup(u => u.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost:5000");
            _controller.Url = url.Object;

            var result = await _controller.Put(userDto);
            Assert.True(result is ObjectResult);

            var userResponse = ((ObjectResult)result).Value as UserResponse;

            Assert.NotNull(userResponse);
            Assert.Equal(userResponse.Id, userDto.Id);
            Assert.Equal(userResponse.Name, userDto.Name);
            Assert.Equal(userResponse.Email, userDto.Email);
            Assert.Equal(userResponse.TeamId, userDto.TeamId);
            Assert.True(userResponse.Active);
            Assert.False(userResponse.Admin);
            #endregion

            #region Bad Request Test
            _controller = new UserController(serviceMock.Object, loginServiceMock.Object);
            _controller.ModelState.AddModelError("Name", "Campo obrigatório!");
            _controller.Url = url.Object;

            result = await _controller.Put(userDto);
            Assert.True(result is BadRequestObjectResult);
            #endregion
        }

        [Fact(DisplayName = "Possible execute DELETE")]
        public async Task PossibleDeleteUser()
        {
            var randomGenerator = new Random();
            var id = randomGenerator.Next(1, 999);

            #region Delete Test
            var serviceMock = new Mock<IUserService>();
            var loginServiceMock = new Mock<ILoginService>();
            serviceMock.Setup(m => m.Delete(It.IsAny<int>())).ReturnsAsync(true);

            _controller = new UserController(serviceMock.Object, loginServiceMock.Object);
            Mock<IUrlHelper> url = new Mock<IUrlHelper>();
            url.Setup(u => u.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost:5000");
            _controller.Url = url.Object;

            var result = await _controller.Delete(id);
            Assert.True(result is OkObjectResult);

            var response = ((OkObjectResult)result).Value;
            Assert.NotNull(response);
            Assert.True((bool) response);
            #endregion

            #region Bad Request Test
            _controller = new UserController(serviceMock.Object, loginServiceMock.Object);
            _controller.ModelState.AddModelError("Id", "Não pode ser 0");
            _controller.Url = url.Object;

            result = await _controller.Delete(0);
            Assert.True(result is BadRequestObjectResult);
            #endregion
        }
    }
}