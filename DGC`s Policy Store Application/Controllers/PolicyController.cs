using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PolicyStoreApplication.Dtos;
using PolicyStoreApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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
        ///        "policyDescription": "Users will bla bla"
        ///        "policyType": 0
        ///        "targetType": 1,
        ///        "conditions" : {
        ///             "Sick Leave": "AAAAAAAAAAA"
        ///        }
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="500">If an internal error occurred</response>
        [HttpPost]
        public async Task<IActionResult> CreatePolicyAsync([FromBody] CreatePolicyDto dto, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Attempting to create policy with name: {dto.PolicyName} and description {dto.PolicyDescription}");

            try
            {
                var policy = await _policyService.CreatePolicyAsync(dto.PolicyName, dto.PolicyDescription, dto.PolicyType, dto.TargetType, dto.Conditions, cancellationToken);
                return Ok(policy);
            }
            catch(Exception e)
            {
                _logger.LogError($"message: {e.Message}, stack trace: {e.StackTrace}");

                return StatusCode((int)HttpStatusCode.InternalServerError, "Unable to create policy. Please contact support if the problem persists.");
            }
        }

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


        [HttpPut("{policyId}")]
        public async Task<IActionResult> UpdatePolicyAsync(string policyId, [FromBody] UpdatePolicyDto dto, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Attempting to update policy with id: {policyId}");

            try
            {
                var policy = await _policyService.UpdatePolicyAsync(policyId, dto.PolicyName, dto.PolicyDescription, dto.PolicyType, dto.TargetType, dto.Conditions, cancellationToken);
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
