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
        public string PolicyObjective { get; set; }
        public PolicyType PolicyType { get; set; }
        public TargetType TargetType { get; set; }
        public Dictionary<string, string> Conditions { get; set; }
        public long CreatedAt { get; set; }
        public long ModifiedAt { get; set; }

        public Policy() { }

        public Policy(string policyName, string policyDescription, PolicyType policyType, TargetType targetType, Dictionary<string, string> conditions = null)
        {
            Id = Guid.NewGuid().ToString();
            PolicyName = policyName;
            PolicyObjective = policyDescription;
            PolicyType = policyType;
            TargetType = targetType;
            Conditions = conditions;
            CreatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            ModifiedAt = CreatedAt;
        }
    }
}
