using HealthInsurePro.Application.Commands;
using HealthInsurePro.Application.Queries;
using HealthInsurePro.Contract.ClaimContracts;
using HealthInsurePro.Domain;
using HealthInsurePro.Domain.Identity;
using Microsoft.AspNetCore.Authorization;

namespace HealthInsurePro.Presentation.Controllers.V1
{
    public class ClaimsController : BaseController
    {
        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            return await HandleExceptionAsync(async () =>
            {
                ClaimModel claim = await _mediator.Send(new GetClaimByIdQuery(id), cancellationToken);
                if (claim is null)
                {
                    _response.Message = $"Claim with id {id} could not be found";
                    return NotFound(_response);
                }

                _response.IsSuccess = true;
                _response.Result = claim;

                return Ok(_response);
            });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get(string policyHolderNationalId, CancellationToken cancellationToken)
        {
            return await HandleExceptionAsync(async () =>
            {
                IEnumerable<ClaimModel> claims = await _mediator.Send(new GetClaimsQuery(policyHolderNationalId), cancellationToken);
                _response.IsSuccess = true;
                _response.Result = claims;

                return Ok(_response);
            });
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] CreateClaimModel data, CancellationToken cancellationToken)
        {
            return await HandleExceptionAsync(async () =>
            {
                ClaimModel claim = await _mediator.Send(new CreateClaimCommand(data), cancellationToken);
                _response.IsSuccess = true;
                _response.Result = claim;
                _response.Message = $"Claim created successfully.";

                Log.Information($"Claim created successfully.");
                return Ok(_response);
            });
        }

        [HttpPut]
        [Authorize(Roles = RoleConstants.AdminRole)]
        [Route(nameof(ProcessClaim))]
        public async Task<IActionResult> ProcessClaim(Guid claimId, ClaimStatus action, CancellationToken cancellationToken)
        {
            return await HandleExceptionAsync(async () =>
            {
                ClaimModel claim = await _mediator.Send(new ProcessClaimCommand(claimId, action), cancellationToken);
                _response.IsSuccess = true;
                _response.Result = claim;
                _response.Message = "Claim processed successfully.";

                Log.Information($"Claim processed successfully. ClaimId: {claimId}, Action: {action}");
                return Ok(_response);
            });
        }
    }
}