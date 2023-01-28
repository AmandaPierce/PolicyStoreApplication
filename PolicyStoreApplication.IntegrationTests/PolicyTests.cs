using Newtonsoft.Json;
using PolicyStoreApplication.IntegrationTests.DataFactories;
using PolicyStoreApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PolicyStoreApplication.IntegrationTests
{
    public class PolicyTests : TestFixture
    {
        private readonly string baseUrl = "http://localhost/api/v1/policy";

        [Fact]
        public async Task Create_Policy_Successfully_Creates_A_Policy()
        {
            //Arrange
            var dto = PolicyDataFactory.CreatePolicyDtoGenerator();

            string strPayload = JsonConvert.SerializeObject(dto);
            HttpContent content = new StringContent(strPayload, Encoding.UTF8, "application/json");

            //Act
            var response = await _httpClient.PostAsync($"{baseUrl}", content);
            var responseContent = await response.Content.ReadAsStringAsync();
            var policy = JsonConvert.DeserializeObject<Policy>(responseContent);

            //Assert
            Assert.NotNull(response);
            Assert.Equal(policy.PolicyType, dto.PolicyType);
            Assert.Equal(policy.PolicyObjective, dto.PolicyObjective);
            Assert.Equal(policy.PolicyType, dto.PolicyType);
            Assert.Equal(policy.TargetType, dto.TargetType);
            Assert.NotNull(policy.Id);
            Assert.NotNull(policy.Conditions);

            //Cleanup
            await DeletePolicy(policy.Id);
        }

        [Fact]
        public async Task Create_Policy_Fails_Create_On_Missing_PolicyName()
        {
            //Arrange
            var dto = PolicyDataFactory.CreatePolicyDtoGenerator();
            dto.PolicyName = "";

            string strPayload = JsonConvert.SerializeObject(dto);
            HttpContent content = new StringContent(strPayload, Encoding.UTF8, "application/json");

            //Act
            var response = await _httpClient.PostAsync($"{baseUrl}", content);

            //Assert
            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Create_Policy_Fails_Create_On_Missing_PolicyObjective()
        {
            //Arrange
            var dto = PolicyDataFactory.CreatePolicyDtoGenerator();
            dto.PolicyObjective = "";

            string strPayload = JsonConvert.SerializeObject(dto);
            HttpContent content = new StringContent(strPayload, Encoding.UTF8, "application/json");

            //Act
            var response = await _httpClient.PostAsync($"{baseUrl}", content);

            //Assert
            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task GetPolicies_Successfully_Gets_All_Policies()
        {
            //Arrange
            var id = await CreatePolicy();

            //Act
            var response = await _httpClient.GetAsync($"{baseUrl}");
            var responseContent = await response.Content.ReadAsStringAsync();
            var policies = JsonConvert.DeserializeObject<List<Policy>>(responseContent);

            //Assert
            Assert.NotNull(response);
            Assert.True(policies.FindIndex(i => i.Id == id) >= 0);

            //Cleanup
            await DeletePolicy(id);
        }

        [Fact]
        public async Task GetPolicy_Successfully_Gets_A_Policy()
        {
            //Arrange
            var id = await CreatePolicy();

            //Act
            var response = await _httpClient.GetAsync($"{baseUrl}/{id}");
            var responseContent = await response.Content.ReadAsStringAsync();
            var policy = JsonConvert.DeserializeObject<Policy>(responseContent);

            //Assert
            Assert.NotNull(response);
            Assert.Equal(id, policy.Id);

            //Cleanup
            await DeletePolicy(id);
        }

        [Fact]
        public async Task UpdatePolicy_Successfully_Updates_A_Policy()
        {
            //Arrange
            var id = await CreatePolicy();
            var dto = PolicyDataFactory.UpdatePolicyDtoGenerator();

            string strPayload = JsonConvert.SerializeObject(dto);
            HttpContent content = new StringContent(strPayload, Encoding.UTF8, "application/json");

            //Act
            var response = await _httpClient.PutAsync($"{baseUrl}/{id}", content);
            var responseContent = await response.Content.ReadAsStringAsync();
            var policy = JsonConvert.DeserializeObject<Policy>(responseContent);

            //Assert
            Assert.NotNull(response);
            Assert.Equal(policy.PolicyType, dto.PolicyType);
            Assert.Equal(policy.PolicyObjective, dto.PolicyObjective);
            Assert.Equal(policy.PolicyType, dto.PolicyType);
            Assert.Equal(policy.TargetType, dto.TargetType);
            Assert.Equal(policy.Id, id);
            Assert.Null(policy.Conditions);

            //Cleanup
            await DeletePolicy(id);
        }

        [Fact]
        public async Task UpdatePolicy_Fails_Update_On_Missing_PolicyName()
        {
            //Arrange
            var id = await CreatePolicy();
            var dto = PolicyDataFactory.UpdatePolicyDtoGenerator();
            dto.PolicyName = "";

            string strPayload = JsonConvert.SerializeObject(dto);
            HttpContent content = new StringContent(strPayload, Encoding.UTF8, "application/json");

            //Act
            var response = await _httpClient.PutAsync($"{baseUrl}/{id}", content);

            //Assert
            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task UpdatePolicy_Fails_Update_On_Missing_PolicyObjective()
        {
            //Arrange
            var id = await CreatePolicy();
            var dto = PolicyDataFactory.UpdatePolicyDtoGenerator();
            dto.PolicyObjective = "";

            string strPayload = JsonConvert.SerializeObject(dto);
            HttpContent content = new StringContent(strPayload, Encoding.UTF8, "application/json");

            //Act
            var response = await _httpClient.PutAsync($"{baseUrl}/{id}", content);

            //Assert
            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task DeletePolicy_Successfully_Deletes_A_Policy()
        {
            //Arrange
            var id = await CreatePolicy();

            //Act
            var response = await _httpClient.DeleteAsync($"{baseUrl}/{id}");

            //Assert
            Assert.NotNull(response);

        }

        private async Task<string> CreatePolicy()
        {
            var dto = PolicyDataFactory.CreatePolicyDtoGenerator();
            string strPayload = JsonConvert.SerializeObject(dto);
            HttpContent content = new StringContent(strPayload, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{baseUrl}", content);
            var responseContent = await response.Content.ReadAsStringAsync();
            var policy = JsonConvert.DeserializeObject<Policy>(responseContent);

            return policy.Id;
        }


        private async Task DeletePolicy(string id)
        {
            await _httpClient.DeleteAsync($"{baseUrl}/{id}");
        }
    }
}
