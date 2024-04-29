using FakeItEasy;
using HealthInsurePro.Application.Abstracts.Repositories;

namespace HealthInsurePro.UnitTest.Infrastructure
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


    }
}
