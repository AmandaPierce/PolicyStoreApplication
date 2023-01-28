using PolicyStoreApplication.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolicyStoreApplication.Models.Data
{
    public class Policy : IDocument
    {
        public string PolicyName { get; set; }
        public string PolicyDescription { get; set; }
        public PolicyType PolicyType { get; set; }
        public TargetType TargetType { get; set; }
        public Dictionary<string, string> Conditions { get; set; }
        DateTimeOffset CreatedAt { get; set; }
        DateTimeOffset ModifiedAt { get; set; }

        public Policy(string policyName, string policyDescription, PolicyType policyType, TargetType targetType, Dictionary<string, string> conditions)
        {
            Id = Guid.NewGuid().ToString();
            PolicyName = policyName;
            PolicyDescription = policyDescription;
            PolicyType = policyType;
            TargetType = targetType;
            Conditions = conditions;
            CreatedAt = DateTimeOffset.UtcNow;
            ModifiedAt = CreatedAt;
        }
    }
}
