using HealthInsurePro.Contract.PolicyHolderContracts;

namespace HealthInsurePro.Application.Handlers
{
    public record GetPolicyHoldersQueryHandler : IRequestHandler<GetPolicyHoldersQuery, IEnumerable<PolicyHolderModel>>
    {
        private readonly IPolicyHolderRepository _policyHolderRepository;

        public GetPolicyHoldersQueryHandler(IPolicyHolderRepository policyHolderRepository)
        {
            _policyHolderRepository = policyHolderRepository;
        }

        public async Task<IEnumerable<PolicyHolderModel>> Handle(GetPolicyHoldersQuery request, CancellationToken cancellationToken)
        {
            return await _policyHolderRepository.GetAsync();
        }
    }
}