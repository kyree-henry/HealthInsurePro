using HealthInsurePro.Application.Commands;
using HealthInsurePro.Application.Queries;
using HealthInsurePro.Contract.PolicyHolderContracts;
using Microsoft.AspNetCore.Authorization;

namespace HealthInsurePro.Presentation.Controllers.V1
{
    public class PolicyHolderController : BaseController
    {
        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            return await HandleExceptionAsync(async () =>
            {
                PolicyHolderModel policyHolder = await _mediator.Send(new GetPolicyHolderByIdQuery(id), cancellationToken);
                if (policyHolder is null)
                {
                    _response.Message = $"Policy Holder with id {id} could not be found";
                    return NotFound(_response);
                }

                _response.IsSuccess = true;
                _response.Result = policyHolder;

                return Ok(_response);
            });
        }
        
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            return await HandleExceptionAsync(async () =>
            {
                IEnumerable<PolicyHolderModel> policyHolders = await _mediator.Send(new GetPolicyHoldersQuery(), cancellationToken);
                _response.IsSuccess = true;
                _response.Result = policyHolders;

                return Ok(_response);
            });
        }
        
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] CreatePolicyHolderModel data, CancellationToken cancellationToken)
        {
            return await HandleExceptionAsync(async () =>
            {
                PolicyHolderModel policyHolder = await _mediator.Send(new CreatePolicyHolderCommand(data), cancellationToken);
                _response.IsSuccess = true;
                _response.Result = policyHolder;
                _response.Message = $"Policy holder created successfully.";

                Log.Information($"Policy holder created: {policyHolder.FullNames} ID: {policyHolder.PolicyHolderId}");
                return Ok(_response);
            });
        }


    }
}