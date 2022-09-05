using HoursBank.Domain.Dtos;
using HoursBank.Domain.Entities;
using HoursBank.Domain.Responses;
using HoursBank.Service.Test.User;
using Xunit;

namespace HoursBank.Service.Test.AutoMapper
{
    public class UserMapperTest : BaseTestService 
    {
        [Fact(DisplayName = "Possible mapper UserDto")]
        public void PossibleMapperUserDto()
        {
            UserTest userTest = new UserTest();

            // Dto => Entity
            var dtoToEntity = Mapper.Map<UserEntity>(userTest.userDto);
            Assert.NotNull(dtoToEntity);
            Assert.Equal(dtoToEntity.Id, userTest.userDto.Id);
            Assert.Equal(dtoToEntity.Name, userTest.userDto.Name);
            Assert.Equal(dtoToEntity.Email, userTest.userDto.Email);
            Assert.Equal(dtoToEntity.Password, userTest.userDto.Password);
            Assert.Equal(dtoToEntity.Active, userTest.userDto.Active);
            Assert.Equal(dtoToEntity.Admin, userTest.userDto.Admin);
            Assert.Equal(dtoToEntity.TeamId, userTest.userDto.TeamId);

            // Entity => Dto
            var userDto = Mapper.Map<UserDto>(dtoToEntity);
            Assert.NotNull(userDto);
            Assert.Equal(dtoToEntity.Id, userDto.Id);
            Assert.Equal(dtoToEntity.Name, userDto.Name);
            Assert.Equal(dtoToEntity.Email, userDto.Email);
            Assert.Equal(dtoToEntity.Password, userDto.Password);
            Assert.Equal(dtoToEntity.Active, userDto.Active);
            Assert.Equal(dtoToEntity.Admin, userDto.Admin);
            Assert.Equal(dtoToEntity.TeamId, userDto.TeamId);
        }

        [Fact(DisplayName = "Possible mapper Response")]
        public void PossibleMapperUserResponse()
        {
            UserTest userTest = new UserTest();

            // Dto => Entity
            var entityToResponse = Mapper.Map<UserResponse>(userTest.userEntity);
            Assert.NotNull(entityToResponse);
            Assert.Equal(entityToResponse.Id, userTest.userDto.Id);
            Assert.Equal(entityToResponse.Name, userTest.userDto.Name);
            Assert.Equal(entityToResponse.Email, userTest.userDto.Email);
            Assert.Equal(entityToResponse.Active, userTest.userDto.Active);
            Assert.Equal(entityToResponse.Admin, userTest.userDto.Admin);
            Assert.Equal(entityToResponse.TeamId, userTest.userDto.TeamId);

            // Entity => Dto
            var userEntity = Mapper.Map<UserEntity>(entityToResponse);
            Assert.NotNull(userEntity);
            Assert.Equal(entityToResponse.Id, userEntity.Id);
            Assert.Equal(entityToResponse.Name, userEntity.Name);
            Assert.Equal(entityToResponse.Email, userEntity.Email);
            Assert.Equal(entityToResponse.Active, userEntity.Active);
            Assert.Equal(entityToResponse.Admin, userEntity.Admin);
            Assert.Equal(entityToResponse.TeamId, userEntity.TeamId);
        }
    }
}
