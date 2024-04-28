using HealthInsurePro.Contract.ClaimContracts;

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



    }
}