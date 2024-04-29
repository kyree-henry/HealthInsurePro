using HealthInsurePro.Contract.PolicyHolderContracts;

namespace HealthInsurePro.Application.Handlers
{
    public record CreatePolicyHolderCommandHandler : IRequestHandler<CreatePolicyHolderCommand, PolicyHolderModel>
    {
        private readonly IPolicyHolderRepository _policyHolderRepository;

        public CreatePolicyHolderCommandHandler(IPolicyHolderRepository policyHolderRepository)
        {
            _policyHolderRepository = policyHolderRepository;
        }

        public async Task<PolicyHolderModel> Handle(CreatePolicyHolderCommand request, CancellationToken cancellationToken)
        {
            return await _policyHolderRepository.CreateAsync(request.model);
        }
    }
}