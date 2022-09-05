using HoursBank.Data.Context;
using HoursBank.Data.Implementations;
using HoursBank.Domain.Entities;
using HoursBank.Util;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace HoursBank.Data.Test.Data
{
    public class UserTest : BaseTest, IClassFixture<DbTest>
    {
        private ServiceProvider _serviceProvider;

        public UserTest(DbTest dbTest) => _serviceProvider = dbTest.ServiceProvider;

        [Fact(DisplayName = "User CRUD")]
        [Trait("CRUD", "UserEntity")]
        public async Task UserCrud()
        {
            using (var context = _serviceProvider.GetService<MyContext>())
            {
                UserImplementation _repository = new UserImplementation(context);

                #region Create
                var randomGenerator = new Random();
                var number = randomGenerator.Next(1, 999);
                var password = Faker.Name.First() + number.ToString();

                UserEntity _entity = new UserEntity
                {
                    Email = Faker.Internet.Email(),
                    Password = BHCrypto.Encode(password),
                    Active = true,
                    Name = Faker.Name.FullName()
                };
                var _created = await _repository.InsertAsync(_entity);
                _entity.Id = _created.Id;
                Assert.NotNull(_created);
                Assert.Equal(_entity.Email, _created.Email);
                Assert.Equal(_entity.Password, _created.Password);
                Assert.Equal(_entity.Name, _created.Name);
                Assert.True(_created.Id > 0);
                #endregion
                
                #region Update
                password = Faker.Name.Last() + number.ToString();
                _entity.Name = Faker.Name.First();
                _entity.Email = Faker.Internet.Email();
                _entity.Active = false;
                number = randomGenerator.Next(1, 999);
                _entity.Password = BHCrypto.Encode(password);

                var _updated = await _repository.UpdateAsync(_entity);

                Assert.NotNull(_updated);
                Assert.Equal(_entity.Email, _updated.Email);
                Assert.Equal(_entity.Password, _updated.Password);
                Assert.Equal(_entity.Name, _updated.Name);
                #endregion

                #region Read
                var _find = await _repository.FindAsync(_entity.Id);
                Assert.True(_find);

                var _exists = await _repository.SelectAsync(_entity.Id);
                Assert.NotNull(_exists);
                Assert.Equal(_entity.Email, _exists.Email);
                Assert.Equal(_entity.Password, _exists.Password);
                Assert.Equal(_entity.Name, _exists.Name);

                var _all = await _repository.SelectAsync();
                Assert.NotNull(_all);
                Assert.True(_all.Count() > 1);

                var _authenticaded = await _repository.Login(_entity.Email, password);
                Assert.NotNull(_authenticaded);
                Assert.Equal(_entity.Email, _authenticaded.Email);
                #endregion

                #region Delete
                var _deleted = await _repository.DeleteAsync(_entity.Id);
                Assert.True(_deleted);
                #endregion
            }
        }
    }
}
