using HealthInsurePro.Application.Abstracts.Repositories;
using HealthInsurePro.Contract.ClaimContracts;

namespace HealthInsurePro.Application.Handlers
{
    public record GetClaimsQueryHandler : IRequestHandler<GetClaimsQuery, IEnumerable<ClaimModel>>
    {
        private readonly IClaimRepository _claimRepository;

        public GetClaimsQueryHandler(IClaimRepository claimRepository)
        {
            _claimRepository = claimRepository;
        }

        public async Task<IEnumerable<ClaimModel>> Handle(GetClaimsQuery request, CancellationToken cancellationToken)
        {
            return await _claimRepository.GetAsync(request.policyHolderNationalId);
        }
    }
}