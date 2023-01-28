using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PolicyStoreApplication.Configuration;
using PolicyStoreApplication.Enums;
using PolicyStoreApplication.Factories;
using PolicyStoreApplication.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PolicyStoreApplication.Services
{
    public class PolicyService : DatabaseService<Policy>, IPolicyService
    {
        private readonly ILogger<PolicyService> _logger;

        public PolicyService(ILogger<PolicyService> logger, IOptions<MongoDBConfiguration> configuration, IMongoDbCollectionFactory mongoDbCollectionFactory) : base(mongoDbCollectionFactory, configuration.Value.PolicyCollectionName)
        {
            _logger = logger;
        }

        public async Task<Models.Policy> CreatePolicyAsync(string policyName, string policyDescription, PolicyType policyType, TargetType targetType, Dictionary<string, string> conditions, CancellationToken cancellationToken)
        {
            _logger.LogTrace($"Trying to create policy with name: {policyName}, description: {policyDescription} and type: {policyType}");

            var policy = new Policy(policyName, policyDescription, policyType, targetType, conditions);

            await this.CreateItem(policy, cancellationToken);

            _logger.LogTrace($"Create policy with name: {policyName}, description: {policyDescription} and type: {policyType}");

            return new Models.Policy(policy);
        }

        public async Task DeletePolicyAsync(string id, CancellationToken cancellationToken)
        {
            _logger.LogTrace($"Trying to delete policy with id: {id}");

            await this.DeleteItem(id, cancellationToken);

            _logger.LogTrace($"Successfully deleted policy with id: {id}");
        }


        public async Task<Models.Policy> GetPolicyAsync(string id, CancellationToken cancellationToken)
        {
            _logger.LogTrace($"Retrieving policy with id: {id}");

            var policy = await this.GetItem(id, cancellationToken);

            _logger.LogTrace($"Retrieved policy with id: {id}");

            return new Models.Policy(policy);
        }

        public async Task<IEnumerable<Models.Policy>> GetPoliciesAsync(CancellationToken cancellationToken)
        {
            _logger.LogTrace($"Retrieving policies");

            var policies = await this.GetItems(cancellationToken);

            _logger.LogTrace($"Retrieved policies");

            return policies.Select(policy => new Models.Policy(policy));
        }

        public async Task<Models.Policy> UpdatePolicyAsync(string id, string policyName, string policyDescription, PolicyType policyType, TargetType targetType, Dictionary<string, string> conditions, CancellationToken cancellationToken)
        {
            _logger.LogTrace($"Trying to update policy with id: {id}, name: {policyName}, description: {policyDescription} and type: {policyType}");

            var policy = await this.GetItem(id, cancellationToken);

            if(policy == null)
            {
                _logger.LogWarning($"Item with id: {id} does not exist.");
                await CreatePolicyAsync(policyName, policyDescription, policyType, targetType, conditions, cancellationToken);
            }
            else
            {
                policy.PolicyName = policyName;
                policy.PolicyObjective = policyDescription;
                policy.PolicyType = policyType;
                policy.TargetType = targetType;
                policy.Conditions = conditions;
                policy.ModifiedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

                await this.UpdateItem(id, policy, cancellationToken);
            }

            _logger.LogTrace($"Updated policy with id: {id}, name: {policyName}, description: {policyDescription} and type: {policyType}");

            return new Models.Policy(policy);
        }
    }
}
