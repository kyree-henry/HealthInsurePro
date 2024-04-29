using Asp.Versioning;
using HealthInsurePro.Presentation.Data;

namespace HealthInsurePro.Presentation.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public ServiceResponse _response { get; set; } = new();
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