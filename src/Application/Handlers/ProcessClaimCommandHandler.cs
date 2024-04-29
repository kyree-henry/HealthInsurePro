using HealthInsurePro.Contract.ClaimContracts;

namespace HealthInsurePro.Application.Handlers
{
    public record ProcessClaimCommandHandler : IRequestHandler<ProcessClaimCommand, ClaimModel>
    {
        private readonly IClaimRepository _claimRepository;

        public ProcessClaimCommandHandler(IClaimRepository claimRepository)
        {
            _claimRepository = claimRepository;
        }

        public async Task<ClaimModel> Handle(ProcessClaimCommand request, CancellationToken cancellationToken)
        {
            return await _claimRepository.ProcessClaimAsync(request.claimId, request.action);
        }
    } 
}