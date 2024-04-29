using HealthInsurePro.Contract.ClaimContracts;
using HealthInsurePro.Domain;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using System;

namespace HealthInsurePro.Infrastructure.Repositories
{
    internal class ClaimRepository : IClaimRepository
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public ClaimRepository(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<ClaimModel>> GetAsync(string policyHolderNationalId)
        {
            return await _context.Claims.Where(a => a.NationalId == policyHolderNationalId)
                                        .ProjectTo<ClaimModel>(_mapper.ConfigurationProvider)
                                        .ToListAsync();
        }

        public async Task<ClaimModel> GetByIdAsync(Guid id)
        {
            Claim? claim = await _context.Claims.FirstOrDefaultAsync(a => a.ClaimId == id);
            return _mapper.Map<ClaimModel>(claim);
        }

        public async Task<ClaimModel> CreateAsync(CreateClaimModel data)
        {
            Claim? claim = _mapper.Map<Claim>(data);

            _context.Claims.Add(claim);
            int result = await _context.SaveChangesAsync();

            return result > 0 ? _mapper.Map<ClaimModel>(claim) 
                              : throw new Exception("Error occured while processing submitted claim.");
        }

        public async Task<ClaimModel> ProcessClaimAsync(Guid claimId, ClaimStatus action)
        {
            Claim? claim = await _context.Claims.FindAsync(claimId) ?? throw new ArgumentException("Claim not found.");

            switch (action)
            {
                case ClaimStatus.InReview:
                    claim.ClaimStatus = ClaimStatus.InReview;
                    break;
                case ClaimStatus.Approved:
                    claim.ClaimStatus = ClaimStatus.Approved;
                    break;
                case ClaimStatus.Declined:
                    claim.ClaimStatus = ClaimStatus.Declined;
                    break;
                default:
                    throw new ArgumentException("Invalid claim action.");
            }

            int result = await _context.SaveChangesAsync();

            return result > 0 ? _mapper.Map<ClaimModel>(claim)
                              : throw new Exception("An error occurred while processing the claim.");
        }

    }
}