using HoursBank.Domain.Dtos;
using HoursBank.Domain.Interfaces.Services;
using HoursBank.Service.Test.User;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HoursBank.Service.Test.Login
{
    public class LoginTest
    {
        private ILoginService? _service;
        private Mock<ILoginService>? _serviceMock;


        [Fact(DisplayName = "Possible execute Login")]
        public void PossibleLogin()
        {
            var userTest = new UserTest();
            var response = new
            {
                authenticated = true,
                created = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = DateTime.Now.AddMinutes(15).ToString("yyyy-MM-dd HH:mm:ss"),
                acessToken = "ABCDEFG.HIJKLM.123XyZ",
                userEmail = userTest.userDto.Email,
                message = "Usuário autenticado com sucesso"
            };

            LoginDto login = new LoginDto
            {
                Email = userTest.userDto.Email,
                Password = "Abc12345"
            };

            _serviceMock = new Mock<ILoginService>();
            _serviceMock.Setup(m => m.Login(login)).ReturnsAsync(response);

            _service = _serviceMock.Object;

            var result = _service.Login(login);
            Assert.NotNull(result);
        }
    }
}
