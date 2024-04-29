using HealthInsurePro.Contract.ClaimContracts;

namespace HealthInsurePro.Application.Handlers
{
    public record CreateClaimCommandHandler : IRequestHandler<CreateClaimCommand, ClaimModel>
    {
        private readonly IClaimRepository _claimRepository;

        public CreateClaimCommandHandler(IClaimRepository claimRepository)
        {
            _claimRepository = claimRepository;
        }

        public async Task<ClaimModel> Handle(CreateClaimCommand request, CancellationToken cancellationToken) 
        {
            return await _claimRepository.CreateAsync(request.model);
        }
    }
}