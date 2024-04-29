using HealthInsurePro.Contract.PolicyHolderContracts;

namespace HealthInsurePro.Application.Handlers
{
    public record GetPolicyHolderByIdQueryHandler : IRequestHandler<GetPolicyHolderByIdQuery, PolicyHolderModel>
    {
        private readonly IPolicyHolderRepository _policyHolderRepository;

        public GetPolicyHolderByIdQueryHandler(IPolicyHolderRepository policyHolderRepository)
        {
            _policyHolderRepository = policyHolderRepository;
        }

        public async Task<PolicyHolderModel> Handle(GetPolicyHolderByIdQuery request, CancellationToken cancellationToken)
        {
            return await _policyHolderRepository.GetByIdAsync(request.policyHolderId);
        }
    }
}