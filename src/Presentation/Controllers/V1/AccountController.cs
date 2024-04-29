using HealthInsurePro.Application.Commands;
using HealthInsurePro.Contract.AccountContracts;
using HealthInsurePro.Contract.UserContracts;
using Microsoft.AspNetCore.Authorization;

namespace HealthInsurePro.Presentation.Controllers.V1
{
    public class AccountController : BaseController
    {
        [HttpPost]
        [AllowAnonymous]
        [Route(nameof(Register))]
        public async Task<IActionResult> Register([FromBody] RegisterModel data, CancellationToken cancellationToken)
        {
            return await HandleExceptionAsync(async () =>
            {
                UserModel user = await _mediator.Send(new RegisterCommand(data), cancellationToken);
                _response.IsSuccess = true;
                _response.Result = user;
                _response.Message = $"User '{user.FullNames}' Registered.";

                Log.Information($"User '{user.Email}' Registered");
                return Ok(_response);
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [Route(nameof(Login))]
        public async Task<IActionResult> Login(TokenRequestModel data, CancellationToken cancellationToken)
        {
            return await HandleExceptionAsync(async () =>
            {
                TokenResponseModel result = await _mediator.Send(new LoginCommand(data), cancellationToken);
                _response.IsSuccess = true;
                _response.Result = result;

                return Ok(_response);
            });
        }


    }
}
