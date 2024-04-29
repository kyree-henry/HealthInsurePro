using HealthInsurePro.Contract.AccountContracts;

namespace HealthInsurePro.Application.Handlers
{
    public record LoginCommandHandler : IRequestHandler<LoginCommand, TokenResponseModel>
    {
        private readonly IAccountRepository _accountRepository;

        public LoginCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<TokenResponseModel> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            return await _accountRepository.LoginAsync(request.model);
        }
    }
}