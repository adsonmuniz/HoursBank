using HoursBank.Domain.Dtos;
using HoursBank.Domain.Entities;
using HoursBank.Domain.Responses;
using HoursBank.Util;

namespace HoursBank.Service.Test.User
{
    public class UserTest
    {
        public static int Id { get; set; }
        public static string? Name { get; set; }
        public static string? Email { get; set; }
        public static string? NameUpdated { get; set; }
        public static string? EmailUpdated { get; set; }
        public static string? Password { get; set; }
        public static string? PasswordUpdate { get; set; }
        public static long Hours { get; set; }
        public static bool Admin { get; set; }
        public static bool Active { get; set; }
        public static int? TeamId { get; set; }

        public List<UserResponse> listUserResponse = new List<UserResponse>();
        public UserEntity userEntity;
        public UserDto userDto;
        public UserDto userUpdateDto;
        public UserResponse userResponse;
        public UserResponse userUpdateResponse;

        public UserTest()
        {
            Id = new Random().Next(1, 9);
            Name = Faker.Name.FullName();
            Email = Faker.Internet.Email();
            NameUpdated = Faker.Name.FullName();
            EmailUpdated = Faker.Internet.Email();
            Password = BHCrypto.Encode("Abc12345");
            Password = BHCrypto.Encode("Abc12346");
            Admin = true;
            Active = true;
            Hours = 0;
            TeamId = 1;

            userEntity = new UserEntity()
            {
                Id = Id,
                Name = Name,
                Email = Email,
                Password = Password,
                Admin = true,
                Active = true,
                TeamId = 1
            };

            userDto = new UserDto()
            {
                Id = Id,
                Name = Name,
                Email = Email,
                Password = Password,
                Admin = true,
                Active = true,
                TeamId = 1
            };

            userResponse = new UserResponse()
            {
                Id = Id,
                Name = Name,
                Email = Email,
                Admin = true,
                Active = true,
                TeamId = 1
            };

            userUpdateDto = new UserDto()
            {
                Id = Id,
                Name = NameUpdated,
                Email = EmailUpdated,
                Password = PasswordUpdate,
                Admin = true,
                Active = true,
                TeamId = 1
            };

            userUpdateResponse = new UserResponse()
            {
                Id = Id,
                Name = NameUpdated,
                Email = EmailUpdated,
                Admin = true,
                Active = true,
                TeamId = 1
            };

            for (int i = 1; i <= 10; i++)
            {
                var response = new UserResponse()
                {
                    Id = new Random().Next(i * 10, i * 10 + 9),
                    Name = Faker.Name.FullName(),
                    Email = Faker.Internet.Email(),
                    Admin = false,
                    Active = false,
                    TeamId = 1
                };
                listUserResponse.Add(response);
            }

        }
    }
}
