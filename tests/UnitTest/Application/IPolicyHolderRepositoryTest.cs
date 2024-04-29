using FakeItEasy;
using HealthInsurePro.Application.Abstracts.Repositories;
using HealthInsurePro.Contract.PolicyHolderContracts;
using Shouldly;

namespace HealthInsurePro.UnitTest.Application
{
    internal class IPolicyHolderRepositoryTest
    {
        private IPolicyHolderRepository _policyHolderRepository;

        [SetUp]
        public void SetUp()
        {
            //Dependencies
            _policyHolderRepository = A.Fake<IPolicyHolderRepository>();
        }

        [Test]
        public async Task GetAsync_Should_Return_PolicyHolders()
        {
            // Arrange
            IEnumerable<PolicyHolderModel> expectedPolicyHolders = GetRandomPolicyHolders(4);

            A.CallTo(() => _policyHolderRepository.GetAsync()).Returns(expectedPolicyHolders);

            // Act
            IEnumerable<PolicyHolderModel> result = await _policyHolderRepository.GetAsync();

            // Assert
            result.ShouldNotBeNull();
            result.Count().ShouldBe(4);
            result.ShouldBe(expectedPolicyHolders);
        }

        [Test]
        public async Task GetByIdAsync_Should_Return_PolicyHolder_For_Valid_Id()
        {
            // Arrange
            Guid policyHolderId = Guid.NewGuid();
            PolicyHolderModel expectedPolicyHolder = GetRandomPolicyHolder(policyHolderId);

            A.CallTo(() => _policyHolderRepository.GetByIdAsync(policyHolderId)).Returns(expectedPolicyHolder);

            // Act
            PolicyHolderModel result = await _policyHolderRepository.GetByIdAsync(policyHolderId);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBe(expectedPolicyHolder);
        }

        [Test]
        public async Task CreateAsync_Should_Return_Created_PolicyHolder()
        {
            // Arrange
            CreatePolicyHolderModel newPolicyHolder = GetRandomCreatePolicyHolder();
            PolicyHolderModel expectedPolicyHolder = GetRandomPolicyHolder(newPolicyHolder.PolicyHolderId);

            A.CallTo(() => _policyHolderRepository.CreateAsync(newPolicyHolder)).Returns(expectedPolicyHolder);

            // Act
            PolicyHolderModel result = await _policyHolderRepository.CreateAsync(newPolicyHolder);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBe(expectedPolicyHolder);
        }

        private IEnumerable<PolicyHolderModel> GetRandomPolicyHolders(int count)
        {
            List<PolicyHolderModel> policyHolders = [];
            for (int i = 0; i < count; i++)
            {
                Guid policyHolderId = Guid.NewGuid();
                policyHolders.Add(GetRandomPolicyHolder(policyHolderId));
            }
            return policyHolders;
        }

        private static PolicyHolderModel GetRandomPolicyHolder(Guid policyHolderId)
        {
            Random random = new ();
            return new PolicyHolderModel
            {
                PolicyHolderId = policyHolderId,
                PolicyNumber = $"POL-{random.Next(1000)}",
                NationalId = $"ID-{random.Next(1000000)}",
                DateOfBirth = DateTime.Now.AddYears(-random.Next(18, 80)),
                Age = random.Next(18, 80),
                FullNames = $"Policy Holder {policyHolderId}"
            };
        }

        private static CreatePolicyHolderModel GetRandomCreatePolicyHolder()
        {
            Random random = new ();
            return new CreatePolicyHolderModel
            {
                PolicyHolderId = Guid.NewGuid(),
                PolicyNumber = $"POL-{random.Next(1000)}",
                NationalId = $"ID-{random.Next(1000000)}",
                DateOfBirth = DateTime.Now.AddYears(-random.Next(18, 80))
            };
        }
    }
}