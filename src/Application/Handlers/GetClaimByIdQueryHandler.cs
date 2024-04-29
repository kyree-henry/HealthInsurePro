using HealthInsurePro.Contract.ClaimContracts;

namespace HealthInsurePro.Application.Handlers
{
    public record GetClaimByIdQueryHandler : IRequestHandler<GetClaimByIdQuery, ClaimModel>
    {
        private readonly IClaimRepository _claimRepository;

        public GetClaimByIdQueryHandler(IClaimRepository claimRepository)
        {
            _claimRepository = claimRepository;
        }

        public async Task<ClaimModel> Handle(GetClaimByIdQuery request, CancellationToken cancellationToken)
        {
            return await _claimRepository.GetByIdAsync(request.claimId);
        }
    }
}