using FakeItEasy;
using HealthInsurePro.Application.Abstracts.Repositories;
using HealthInsurePro.Contract.ClaimContracts;
using HealthInsurePro.Domain;
using Shouldly;

namespace HealthInsurePro.UnitTest.Application
{
    [TestFixture] //TODO: Complete Tests
    internal class IClaimRepositoryTest
    {
        private IClaimRepository _claimRepository;

        [SetUp]
        public void SetUp()
        {
            //Dependencies
            _claimRepository = A.Fake<IClaimRepository>();
        }

        [Test]
        public async Task GetAsync_Should_Return_Claims_For_Valid_NationalId()
        {
            // Arrange
            string nationalId = "123456";
            IEnumerable<ClaimModel> expectedResult = GetRandomClaims(4, nationalId);

            A.CallTo(() => _claimRepository.GetAsync(nationalId)).Returns(expectedResult);

            // Act
            IEnumerable<ClaimModel> result = await _claimRepository.GetAsync(nationalId);

            // Assert
            result.ShouldNotBeNull();
            result.Count().ShouldBe(4);
            result.ShouldBe(expectedResult);
        }

        [Test]
        public async Task GetByIdAsync_Should_Return_Claim_For_Valid_Id()
        {
            // Arrange
            Guid claimId = Guid.NewGuid();
            ClaimModel? expectedClaim = GetRandomClaimModel(claimId);

            A.CallTo(() => _claimRepository.GetByIdAsync(claimId)).Returns(expectedClaim);

            // Act
            ClaimModel result = await _claimRepository.GetByIdAsync(claimId);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBe(expectedClaim);
        }

        [Test]
        public async Task CreateAsync_Should_Return_Created_ClaimModel_For_Valid_Data()
        {
            // Arrange
            CreateClaimModel data = GetRandomCreateClaimModel();
            ClaimModel expectedClaim = GetRandomClaimModel();

            A.CallTo(() => _claimRepository.CreateAsync(data)).Returns(expectedClaim);

            // Act
            ClaimModel result = await _claimRepository.CreateAsync(data);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBe(expectedClaim);
        }

        [Test]
        [TestCase(ClaimStatus.Submitted)]
        [TestCase(ClaimStatus.InReview)]
        [TestCase(ClaimStatus.Approved)]
        [TestCase(ClaimStatus.Declined)]
        public async Task ProcessClaimAsync_Should_Update_Claim_Status(ClaimStatus action)
        {
            // Arrange
            Guid claimId = Guid.NewGuid();
            ClaimModel expectedClaim = GetRandomClaimModel(claimId);

            A.CallTo(() => _claimRepository.ProcessClaimAsync(claimId, action)).Returns(expectedClaim);

            // Act
            ClaimModel result = await _claimRepository.ProcessClaimAsync(claimId, action);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBe(expectedClaim);
        }

        private static IEnumerable<ClaimModel> GetRandomClaims(int count, string? nationalId = null)
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
