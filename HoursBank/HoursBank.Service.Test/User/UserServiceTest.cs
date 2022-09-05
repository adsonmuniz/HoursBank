using HoursBank.Domain.Interfaces.Services;
using HoursBank.Domain.Responses;
using Moq;
using Xunit;

namespace HoursBank.Service.Test.User
{
    public class UserServiceTest : UserTest
    {
        private IUserService? _service;
        private Mock<IUserService>? _serviceMock;

        [Fact(DisplayName = "When execute GET")]
        public async Task PossibleGet()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.Get(Id)).ReturnsAsync(userResponse);
            _service = _serviceMock.Object;

            var result = await _service.Get(Id);
            Assert.NotNull(result);
            Assert.True(result.Id == Id);
            Assert.Equal(Name, result.Name);
            Assert.Equal(Email, result.Email);

            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.Get(It.IsAny<int>())).Returns(Task.FromResult((UserResponse) null));
            _service = _serviceMock.Object;

            result = await _service.Get(Id);
            Assert.Null(result);
        }

        [Fact(DisplayName = "When execute GETBYEMAIL")]
        public async Task PossibleGetByEmail()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.GetByEmail(Email)).ReturnsAsync(userResponse);
            _service = _serviceMock.Object;

            var result = await _service.GetByEmail(Email);
            Assert.NotNull(result);
            Assert.True(result.Id == Id);
            Assert.Equal(Name, result.Name);
            Assert.Equal(Email, result.Email);
        }

        [Fact(DisplayName = "When execute GETBYTEAM")]
        public async Task PossibleGetByTeam()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.GetByTeam(It.IsAny<int>())).ReturnsAsync(listUserResponse);
            _service = _serviceMock.Object;

            var result = await _service.GetByTeam(1);
            Assert.NotNull(result);
            Assert.True(result.Count() == 10);
        }

        [Fact(DisplayName = "When execute GET ALL")]
        public async Task PossibleGetAll()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.GetAll()).ReturnsAsync(listUserResponse);
            _service = _serviceMock.Object;

            var result = await _service.GetAll();
            Assert.NotNull(result);
            Assert.True(result.Count() == 10);
        }

        [Fact(DisplayName = "When execute CREATE")]
        public async Task PossibleCreate()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.Post(userDto)).ReturnsAsync(userResponse);
            _service = _serviceMock.Object;

            var result = await _service.Post(userDto);
            Assert.NotNull(result);
            Assert.Equal(Id, result.Id);
            Assert.Equal(Name, result.Name);
            Assert.Equal(Email, result.Email);
        }

        [Fact(DisplayName = "When execute UPDATE")]
        public async Task PossibleUpdate()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.Put(userUpdateDto)).ReturnsAsync(userUpdateResponse);
            _service = _serviceMock.Object;

            var result = await _service.Put(userUpdateDto);
            Assert.NotNull(result);
            Assert.Equal(Id, result.Id);
            Assert.Equal(NameUpdated, result.Name);
            Assert.Equal(EmailUpdated, result.Email);
        }

        [Fact(DisplayName = "When execute DELETE")]
        public async Task PossibleDelete()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.Delete(Id)).ReturnsAsync(true);
            _service = _serviceMock.Object;

            var result = await _service.Delete(Id);
            Assert.True(result);

            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.Delete(Id)).ReturnsAsync(false);
            _service = _serviceMock.Object;

            result = await _service.Delete(Id);
            Assert.False(result);
        }
    }
}
