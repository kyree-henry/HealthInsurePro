using FakeItEasy;
using HealthInsurePro.Application.Commands;
using HealthInsurePro.Contract.AccountContracts;
using HealthInsurePro.Contract.UserContracts;
using HealthInsurePro.Domain;
using HealthInsurePro.Presentation.Controllers.V1;
using HealthInsurePro.Presentation.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shouldly;
using System.Net;

namespace HealthInsurePro.UnitTest.Presentation.V1
{
    internal class AccountControllerTest
    {
        private AccountController _accountController;
        private IMediator _mediator;

        [SetUp]
        public void SetUp()
        {
            _mediator = A.Fake<IMediator>();
            _accountController = new AccountController(_mediator);
        }

        [Test]
        public async Task Register_Should_Return_Ok_With_UserModel()
        {
            // Arrange
            RegisterModel newUser = GetRandomRegisterData();
            UserModel expectedResult = GetRandomUserModel();

            A.CallTo(() => _mediator.Send(A<RegisterCommand>._, A<CancellationToken>._)).Returns(expectedResult);

            // Act
            OkObjectResult? result = await _accountController.Register(newUser, CancellationToken.None) as OkObjectResult;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe((int)HttpStatusCode.OK);
            result.Value.ShouldSatisfyAllConditions(
            () => result.Value.ShouldBeAssignableTo<ServiceResponse>(),
            () => result.Value.ShouldBeAssignableTo<ServiceResponse>()!.IsSuccess.ShouldBeTrue(),
            () => result.Value.ShouldBeAssignableTo<ServiceResponse>()!.Result.ShouldBe(expectedResult));
        }

        [Test]
        public async Task Login_Should_Return_Ok_With_TokenResponseModel()
        {
            // Arrange
            TokenRequestModel loginInfo = GetRandomTokenRequestModel();
            TokenResponseModel expectedResult = GetRandomTokenResponseModel();

            A.CallTo(() => _mediator.Send(A<LoginCommand>._, A<CancellationToken>._)).Returns(expectedResult);

            // Act
            OkObjectResult? result = await _accountController.Login(loginInfo, CancellationToken.None) as OkObjectResult;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);
            result.Value.ShouldSatisfyAllConditions(
            () => result.Value.ShouldBeAssignableTo<ServiceResponse>(),
            () => result.Value.ShouldBeAssignableTo<ServiceResponse>()!.IsSuccess.ShouldBeTrue(),
            () => result.Value.ShouldBeAssignableTo<ServiceResponse>()!.Result.ShouldBe(expectedResult));
        }



        private static RegisterModel GetRandomRegisterData()
        {
            Random random = new ();
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

        private static UserModel GetRandomUserModel()
        {
            Random random = new ();
            return new UserModel
            {
                FullNames = $"User {random.Next(1000)}",
                Email = $"user{random.Next(1000)}@example.com"
            };
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
    }
}
