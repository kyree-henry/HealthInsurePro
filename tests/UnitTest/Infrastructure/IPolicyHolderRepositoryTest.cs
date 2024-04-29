using FakeItEasy;
using HealthInsurePro.Application.Abstracts.Repositories;
using HealthInsurePro.Infrastructure.Repositories;

namespace HealthInsurePro.UnitTest.Infrastructure
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


    }
}