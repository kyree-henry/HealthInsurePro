using FakeItEasy;
using HealthInsurePro.Application.Abstracts.Repositories;
using HealthInsurePro.Contract.AccountContracts;
using HealthInsurePro.Contract.UserContracts;
using HealthInsurePro.Domain;
using Shouldly;

namespace HealthInsurePro.UnitTest.Application
{
    internal class IAccountRepositoryTest
    {
        private IAccountRepository _accountRepository;

        [SetUp]
        public void SetUp()
        {
            //Dependencies
            _accountRepository = A.Fake<IAccountRepository>();
        }

        [Test]
        public async Task LoginAsync_Should_Return_TokenResponseModel()
        {
            // Arrange
            TokenRequestModel requestModel = GetRandomTokenRequestModel();
            TokenResponseModel expectedResponse = GetRandomTokenResponseModel();

            A.CallTo(() => _accountRepository.LoginAsync(requestModel)).Returns(expectedResponse);

            // Act
            TokenResponseModel result = await _accountRepository.LoginAsync(requestModel);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBe(expectedResponse);
        }

        [Test]
        public async Task RegisterAsync_Should_Return_UserModel()
        {
            // Arrange
            RegisterModel newUser = GetRandomRegisterData();
            UserModel expectedUser = GetRandomUserData();

            A.CallTo(() => _accountRepository.RegisterAsync(newUser)).Returns(expectedUser);

            // Act
            UserModel result = await _accountRepository.RegisterAsync(newUser);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBe(expectedUser);
        }

        [Test]
        public async Task IsEmailInUse_Should_Return_True_If_Email_In_Use()
        {
            // Arrange
            string email = "test@example.com";

            A.CallTo(() => _accountRepository.IsEmailInUse(email)).Returns(true);

            // Act
            bool result = await _accountRepository.IsEmailInUse(email);

            // Assert
            result.ShouldBeTrue();
        }

        [Test]
        public async Task IsEmailInUse_Should_Return_False_If_Email_Not_In_Use()
        {
            // Arrange
            string email = "test@example.com";

            A.CallTo(() => _accountRepository.IsEmailInUse(email)).Returns(false);

            // Act
            bool result = await _accountRepository.IsEmailInUse(email);

            // Assert
            result.ShouldBeFalse();
        }

        private static TokenRequestModel GetRandomTokenRequestModel()
        {
            Random random = new ();
            return new TokenRequestModel
            {
                Email = $"user{random.Next(1000)}@example.com",
                Password = Guid.NewGuid().ToString()
            };
        }

        private static TokenResponseModel GetRandomTokenResponseModel()
        {
            Random random = new ();
            return new TokenResponseModel
            {
                FullNames = $"User {random.Next(1000)}",
                IsLockedOut = false,
                Token = Guid.NewGuid().ToString(),
                Message = "Login successful",
                Succeeded = true
            };
        }

        private static RegisterModel GetRandomRegisterData()
        {
            var random = new Random();
            string password = Guid.NewGuid().ToString();
            return new RegisterModel
            {
                FirstName = $"First {random.Next(1000)}",
                SurName = $"Surname {random.Next(1000)}",
                Email = $"user{random.Next(1000)}@example.com",
                UserType = UserType.User,
                Password = password,
                ConfirmPassword = password
            };
        }

        private static UserModel GetRandomUserData()
        {
            var random = new Random();
            return new UserModel
            {
                FullNames = $"User {random.Next(1000)}",
                Email = $"user{random.Next(1000)}@example.com"
            };
        }

    }
}
