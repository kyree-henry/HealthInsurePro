using HealthInsurePro.Application.Abstracts.Services;
using HealthInsurePro.Contract.AccountContracts;
using HealthInsurePro.Contract.UserContracts;
using HealthInsurePro.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace HealthInsurePro.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private const string InvalidLoginMessage = "Invalid login attempt.";

        public AccountRepository(SignInManager<ApplicationUser> signInManager,
                                 UserManager<ApplicationUser> userManager,
                                 IMapper mapper,
                                 ITokenService tokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<TokenResponseModel> LoginAsync(TokenRequestModel model)
        {
            TokenResponseModel response = new();

            ApplicationUser? user = await _userManager.FindByEmailAsync(model.Email!);

            if (user is null)
            {
                response.Message = InvalidLoginMessage;
                return response;
            }

            SignInResult? result = await _signInManager.PasswordSignInAsync(user, model.Password!, false, false);
            if (!result.Succeeded)
            {
                response.Message = "Account locked contact support.";
                response.IsLockedOut = result.IsLockedOut;
                return response;
            }

            IList<string>? roles = await _userManager.GetRolesAsync(user);
            string token = await _tokenService.CreateToken(user, roles);
            response.Token = token;
            response.Succeeded = true;
            response.Message = "Login successful.";

            return response;
        }

        public async Task<UserModel> RegisterAsync(RegisterModel data)
        {
            if (await IsEmailInUse(data.Email))
            {
                throw new Exception($"Account with Email {data.Email} is already registered.");
            }

            ApplicationUser? user = _mapper.Map<ApplicationUser>(data);
            user.UserName = data.Email;

            IdentityResult? result = await _userManager.CreateAsync(user, data.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, RoleConstants.UserRole);

                user = await _userManager.FindByEmailAsync(user.Email!);
                return _mapper.Map<UserModel>(user);
            }
            else
            {
                throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(a => a.Description)));
            }
        }

        public async Task<bool> IsEmailInUse(string email)
        {
            return (await _userManager.FindByEmailAsync(email)) is not null;
        }

    }
}