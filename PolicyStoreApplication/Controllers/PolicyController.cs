using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PolicyStoreApplication.Dtos;
using PolicyStoreApplication.Services;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace PolicyStoreApplication.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PolicyController : ControllerBase
    {
        private readonly ILogger<PolicyController> _logger;
        private readonly IPolicyService _policyService;

        public PolicyController(ILogger<PolicyController> logger, IPolicyService policyService)
        {
            _logger = logger;
            _policyService = policyService;
        }

        /// <summary>
        /// Creates a policy.
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A newly created PolicyObject</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /policy
        ///     {
        ///        "policyName": "Code of Conduct",
        ///        "policyObjective": "Our Employee Code of Conduct company policy outlines our expectations regarding employees’ behaviour towards their colleagues, supervisors and overall organization. ",
        ///        "policyType": 1,
        ///        "targetType": 31,
        ///        "conditions": {
        ///             "Compliance with law": "All employees must protect our company’s legality. They should comply with all environmental, safety and fair dealing laws. We expect employees to be ethical and responsible when dealing with our company’s finances, products, partnerships and public image.",
        ///             "Respect in the workplace": "All employees should respect their colleagues"
        ///        }
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns the newly created Policy object</response>
        /// <response code="500">If an unhandled exception is thrown an internal error is returned</response>
        [HttpPost]
        public async Task<IActionResult> CreatePolicyAsync([FromBody] UpsertPolicyDto dto, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Attempting to create policy with name: {dto.PolicyName} and description {dto.PolicyObjective}");

            try
            {
                var policy = await _policyService.CreatePolicyAsync(dto.PolicyName, dto.PolicyObjective, dto.PolicyType, dto.TargetType, dto.Conditions, cancellationToken);
                return Ok(policy);
            }
            catch(Exception e)
            {
                _logger.LogError($"message: {e.Message}, stack trace: {e.StackTrace}");

                return StatusCode((int)HttpStatusCode.InternalServerError, "Unable to create policy. Please contact support if the problem persists.");
            }
        }

        /// <summary>
        /// Deletes a policy.
        /// </summary>
        /// <param name="policyId"></param>
        /// <param name="cancellationToken"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     Delete /policy/{id}
        ///
        /// </remarks>
        /// <returns>A successful ActionResult</returns>
        /// <response code="200">Returns OkResult object</response>
        /// <response code="500">If an unhandled exception is thrown an internal error is returned</response>
        [HttpDelete("{policyId}")]
        public async Task<IActionResult> DeletePolicyAsync(string policyId, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Attempting to delete policy with id: {policyId}");

            try
            {
                await _policyService.DeletePolicyAsync(policyId, cancellationToken);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"message: {e.Message}, stack trace: {e.StackTrace}");

                return StatusCode((int)HttpStatusCode.InternalServerError, "Unable to delete policy. Please contact support if the problem persists.");
            }
        }

        /// <summary>
        /// Gets a policy.
        /// </summary>
        /// <param name="policyId"></param>
        /// <param name="cancellationToken"></param>
        /// Sample request:
        ///
        ///     Get /policy/{id}
        ///
        /// <returns>The policy matching the id passed in as a parameter</returns>
        /// <response code="200">Returns a Policy object</response>
        /// <response code="500">If an unhandled exception is thrown an internal error is returned</response>
        [HttpGet("{policyId}")]
        public async Task<IActionResult> GetPolicyAsync(string policyId, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Attempting to get policy with id: {policyId}");

            try
            {
                var policy = await _policyService.GetPolicyAsync(policyId, cancellationToken);
                return Ok(policy);
            }
            catch (Exception e)
            {
                _logger.LogError($"message: {e.Message}, stack trace: {e.StackTrace}");

                return StatusCode((int)HttpStatusCode.InternalServerError, "Unable to get policy. Please contact support if the problem persists.");
            }
        }

        /// <summary>
        /// Gets a list of all policies.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// Sample request:
        ///
        ///     Get /policy
        ///
        /// <returns>All company policies</returns>
        /// <response code="200">Returns a list of Policy objects</response>
        /// <response code="500">If an unhandled exception is thrown an internal error is returned</response>
        [HttpGet]
        public async Task<IActionResult> GetPoliciesAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Attempting to get all policies.");

            try
            {
                var policies = await _policyService.GetPoliciesAsync(cancellationToken);
                return Ok(policies);
            }
            catch (Exception e)
            {
                _logger.LogError($"message: {e.Message}, stack trace: {e.StackTrace}");

                return StatusCode((int)HttpStatusCode.InternalServerError, "Unable to get policies. Please contact support if the problem persists.");
            }
        }

        /// <summary>
        /// Updates a policy.
        /// </summary>
        /// <param name="policyId"></param>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A newly updated PolicyObject</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     Put /policy
        ///     {
        ///        "policyName": "Code of Conduct",
        ///        "policyObjective": "Our Employee Code of Conduct company policy outlines our expectations regarding employees’ behaviour towards their colleagues, supervisors and overall organization. ",
        ///        "policyType": 1,
        ///        "targetType": 31,
        ///        "conditions": {
        ///             "Compliance with law": "All employees must protect our company’s legality. They should comply with all environmental, safety and fair dealing laws. We expect employees to be ethical and responsible when dealing with our company’s finances, products, partnerships and public image.",
        ///             "Respect in the workplace": "All employees should respect their colleagues"
        ///        }
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns the newly updated Policy object</response>
        /// <response code="500">If an unhandled exception is thrown an internal error is returned</response>
        [HttpPut("{policyId}")]
        public async Task<IActionResult> UpdatePolicyAsync(string policyId, [FromBody] UpsertPolicyDto dto, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Attempting to update policy with id: {policyId}");

            try
            {
                var policy = await _policyService.UpdatePolicyAsync(policyId, dto.PolicyName, dto.PolicyObjective, dto.PolicyType, dto.TargetType, dto.Conditions, cancellationToken);
                return Ok(policy);
            }
            catch (Exception e)
            {
                _logger.LogError($"message: {e.Message}, stack trace: {e.StackTrace}");

                return StatusCode((int)HttpStatusCode.InternalServerError, "Unable to update policy. Please contact support if the problem persists.");
            }
        }
    }
}
