using PolicyStoreApplication.Enums;
using PolicyStoreApplication.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PolicyStoreApplication.Services
{
    public interface IPolicyService
    {
        Task<Policy> CreatePolicyAsync(string policyName, string policyDescription, PolicyType policyType, TargetType targetType, Dictionary<string, string> conditions, CancellationToken cancellationToken);
        Task DeletePolicyAsync(string id, CancellationToken cancellationToken);
        Task<Policy> GetPolicyAsync(string id, CancellationToken cancellationToken);
        Task<IEnumerable<Models.Policy>> GetPoliciesAsync(CancellationToken cancellationToken);
        Task<Models.Policy> UpdatePolicyAsync(string id, string policyName, string policyDescription, PolicyType policyType, TargetType targetType, Dictionary<string, string> conditions, CancellationToken cancellationToken);
    }
}
