using FakeItEasy;
using HealthInsurePro.Application.Commands;
using HealthInsurePro.Application.Queries;
using HealthInsurePro.Contract.ClaimContracts;
using HealthInsurePro.Domain;
using HealthInsurePro.Presentation.Controllers.V1;
using HealthInsurePro.Presentation.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shouldly;

namespace HealthInsurePro.UnitTest.Presentation.V1
{
    internal class ClaimsControllerTest
    {
        private ClaimsController _claimsController;
        private IMediator _mediator;

        [SetUp]
        public void SetUp()
        {
            _mediator = A.Fake<IMediator>();
            _claimsController = new ClaimsController(_mediator);
        }

        [Test]
        public async Task Get_By_Id_Should_Return_Ok_With_ClaimModel()
        {
            // Arrange
            Guid claimId = Guid.NewGuid();
            ClaimModel claim = GetRandomClaimModel();

            A.CallTo(() => _mediator.Send(A<GetClaimByIdQuery>._, A<CancellationToken>._)).Returns(claim);

            // Act
            OkObjectResult? result = await _claimsController.Get(claimId, CancellationToken.None) as OkObjectResult;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);
            result.Value.ShouldSatisfyAllConditions(
            () => result.Value.ShouldBeAssignableTo<ServiceResponse>(),
            () => result.Value.ShouldBeAssignableTo<ServiceResponse>()!.IsSuccess.ShouldBeTrue(),
            () => result.Value.ShouldBeAssignableTo<ServiceResponse>()!.Result.ShouldBe(claim));
        }

        [Test]
        public async Task Get_By_PolicyHolderNationalId_Should_Return_Ok_With_ClaimModels()
        {
            // Arrange
            string policyHolderNationalId = "123456";
            IEnumerable<ClaimModel> claims = GetRandomClaimModels(3);

            A.CallTo(() => _mediator.Send(A<GetClaimsQuery>._, A<CancellationToken>._)).Returns(claims);

            // Act
            OkObjectResult? result = await _claimsController.Get(policyHolderNationalId, CancellationToken.None) as OkObjectResult;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);
            result.Value.ShouldSatisfyAllConditions(
            () => result.Value.ShouldBeAssignableTo<ServiceResponse>(),
            () => result.Value.ShouldBeAssignableTo<ServiceResponse>()!.IsSuccess.ShouldBeTrue(),
            () => result.Value.ShouldBeAssignableTo<ServiceResponse>()!.Result.ShouldBe(claims));
        }

        [Test]
        public async Task Post_Should_Return_Ok_With_ClaimModel()
        {
            // Arrange
            CreateClaimModel createClaimModel = GetRandomCreateClaimModel();
            ClaimModel claimModel = GetRandomClaimModel();

            A.CallTo(() => _mediator.Send(A<CreateClaimCommand>._, A<CancellationToken>._)).Returns(claimModel);

            // Act
            OkObjectResult? result = await _claimsController.Post(createClaimModel, CancellationToken.None) as OkObjectResult;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);
            result.Value.ShouldSatisfyAllConditions(
            () => result.Value.ShouldBeAssignableTo<ServiceResponse>(),
            () => result.Value.ShouldBeAssignableTo<ServiceResponse>()!.IsSuccess.ShouldBeTrue(),
            () => result.Value.ShouldBeAssignableTo<ServiceResponse>()!.Result.ShouldBe(claimModel));
        }

        [Test]
        [TestCase(ClaimStatus.Submitted)]
        [TestCase(ClaimStatus.InReview)]
        [TestCase(ClaimStatus.Approved)]
        [TestCase(ClaimStatus.Declined)]
        public async Task ProcessClaim_Should_Return_Ok_With_ClaimModel(ClaimStatus action)
        {
            // Arrange
            Guid claimId = Guid.NewGuid();
            ClaimModel claimModel = GetRandomClaimModel();

            A.CallTo(() => _mediator.Send(A<ProcessClaimCommand>._, A<CancellationToken>._)).Returns(claimModel);

            // Act
            OkObjectResult? result = await _claimsController.ProcessClaim(claimId, action, CancellationToken.None) as OkObjectResult;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);
            result.Value.ShouldSatisfyAllConditions(
            () => result.Value.ShouldBeAssignableTo<ServiceResponse>(),
            () => result.Value.ShouldBeAssignableTo<ServiceResponse>()!.IsSuccess.ShouldBeTrue(),
            () => result.Value.ShouldBeAssignableTo<ServiceResponse>()!.Result.ShouldBe(claimModel));
        }

        private static IEnumerable<ClaimModel> GetRandomClaimModels(int count, string? nationalId = null)
        {
            List<ClaimModel> claims = [];
            for (int i = 0; i < count; i++)
            {
                Guid claimId = Guid.NewGuid();
                claims.Add(GetRandomClaimModel(claimId, nationalId));
            }
            return claims;
        }

        private static ClaimModel GetRandomClaimModel(Guid? id = null, string? nationalId = null)
        {
            Random random = new();
            return new ClaimModel
            {
                ClaimId = id ?? Guid.NewGuid(),
                NationalId = nationalId ?? random.Next(100000, 999999).ToString(),
                ClaimStatus = (ClaimStatus)random.Next(1, 5)
            };
        }

        private static CreateClaimModel GetRandomCreateClaimModel()
        {
            Random random = new();
            return new CreateClaimModel
            {
                ClaimStatus = ClaimStatus.Submitted,
                NationalId = random.Next(100000, 999999).ToString(),
            };
        }
    }
}