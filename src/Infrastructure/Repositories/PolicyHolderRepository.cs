using HealthInsurePro.Contract.PolicyHolderContracts;
using HealthInsurePro.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace HealthInsurePro.Infrastructure.Repositories
{
    internal class PolicyHolderRepository : IPolicyHolderRepository
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PolicyHolderRepository(IMapper mapper, DataContext context, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
        }

        public async Task<IEnumerable<PolicyHolderModel>> GetAsync()
        {
            return await _context.PolicyHolders.ProjectTo<PolicyHolderModel>(_mapper.ConfigurationProvider)
                                        .ToListAsync();
        }

        public async Task<PolicyHolderModel> GetByIdAsync(Guid policyHolderId)
        {
            PolicyHolder? policyHolder = await _context.PolicyHolders.FirstOrDefaultAsync(a => a.PolicyHolderId == policyHolderId);
            return _mapper.Map<PolicyHolderModel>(policyHolder);
        }

        public async Task<PolicyHolderModel> CreateAsync(CreatePolicyHolderModel data)
        {
            ApplicationUser? user = await _userManager.Users.FirstOrDefaultAsync(a => a.Id == data.PolicyHolderId)
                                  ?? throw new Exception("User not found with the given PolicyHolderId.");
            
            PolicyHolder? policyHolder = await _context.PolicyHolders.FindAsync(data.PolicyHolderId);
            if (policyHolder is not null)
            {
                throw new Exception("PolicyHolder with the given PolicyHolderId already exists.");
            }

            _mapper.Map(data, policyHolder);
            policyHolder!.PolicyHolderId = user.Id;
            policyHolder!.FirstName = user.FirstName;
            policyHolder!.SurName = policyHolder.SurName;

            _context.PolicyHolders.Add(policyHolder);

            int result = await _context.SaveChangesAsync();

            return result > 0 ? _mapper.Map<PolicyHolderModel>(policyHolder) : throw new Exception("Error occurred while creating the PolicyHolder.");
        }
    }
}