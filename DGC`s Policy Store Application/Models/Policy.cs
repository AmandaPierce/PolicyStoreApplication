using PolicyStoreApplication.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolicyStoreApplication.Models
{
    public class Policy
    {
        public string Id { get; set; }
        public string PolicyName { get; set; }
        public string PolicyDescription { get; set; }
        public PolicyType PolicyType { get; set; }
        public TargetType TargetType { get; set; }
        public Dictionary<string, string> Conditions { get; set; }

        public Policy(Data.Policy policy)
        {
            Id = policy.Id;
            PolicyName = policy.PolicyName;
            PolicyDescription = policy.PolicyDescription;
            PolicyType = policy.PolicyType;
            TargetType = policy.TargetType;
            Conditions = policy.Conditions;
        }
    }
}
