using PolicyStoreApplication.Dtos;
using PolicyStoreApplication.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PolicyStoreApplication.IntegrationTests.DataFactories
{
    public static class PolicyDataFactory
    {
        public static UpsertPolicyDto CreatePolicyDtoGenerator()
        {
            var conditions = new Dictionary<string, string>();
            conditions.Add(RandomString(10), RandomString(60));

            UpsertPolicyDto dto = new UpsertPolicyDto()
            {
                PolicyName = RandomString(10),
                PolicyObjective = RandomString(50),
                PolicyType = PolicyType.Distributive,
                TargetType = TargetType.FullTimeEmployees,
                Conditions = conditions
            };

            return dto;
        }

        public static UpsertPolicyDto UpdatePolicyDtoGenerator()
        {
            UpsertPolicyDto dto = new UpsertPolicyDto()
            {
                PolicyName = RandomString(10),
                PolicyObjective = RandomString(50),
                PolicyType = PolicyType.Distributive,
                TargetType = TargetType.FullTimeEmployees,
                Conditions = null
            };

            return dto;
        }

        private static Random random = new Random();

        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
