using PolicyStoreApplication.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PolicyStoreApplication.Dtos
{
    public class UpsertPolicyDto
    {
        [Required]
        [MaxLength(25)]
        public string PolicyName { get; set; }
        [Required]
        [MaxLength(500)]
        public string PolicyObjective { get; set; }
        [Required]
        public PolicyType PolicyType { get; set; }

        [Required]
        public TargetType TargetType { get; set; }

        public Dictionary<string, string> Conditions { get; set; }

    }
}
