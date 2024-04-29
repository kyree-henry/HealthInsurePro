using Asp.Versioning;
using HealthInsurePro.Presentation.Data;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace HealthInsurePro.Presentation.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private IMediator? _mediatorInstance;
        // private IHostEnvironment? _envInstance;

        public ServiceResponse _response { get; set; } = new();
        private IAuthorizationService? _authorizationServiceInstance;

        protected IMediator _mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>()!;
        // protected IHostEnvironment _env => _envInstance ??= HttpContext.RequestServices.GetService<IHostEnvironment>()!;
        protected IAuthorizationService _authorizationService => _authorizationServiceInstance ??= HttpContext.RequestServices.GetService<IAuthorizationService>()!;

        protected async Task<IActionResult> HandleExceptionAsync(Func<Task<IActionResult>> action, string? customErrorMessage = null)
        {
            try
            {
                return await action();
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                _response.Message = customErrorMessage ?? "An error occurred.";
                _response.IsSuccess = false;
                _response.ErrorMessages.Add(ex.Message);
                return StatusCode(500, _response);
            }
        }
    }
}