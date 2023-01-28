using PolicyStoreApplication.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PolicyStoreApplication.Dtos
{
    public class CreatePolicyDto
    {
        [Required]
        public string PolicyName { get; set; }
        [Required]
        public string PolicyDescription { get; set; }
        [Required]
        public PolicyType PolicyType { get; set; }

        [Required]
        public TargetType TargetType { get; set; }

        public Dictionary<string, string> Conditions { get; set; }

    }
}
