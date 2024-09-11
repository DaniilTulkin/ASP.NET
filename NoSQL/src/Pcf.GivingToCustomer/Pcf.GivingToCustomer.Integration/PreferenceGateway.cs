using Pcf.GivingToCustomer.Core.Abstractions.Gateways;
using Pcf.GivingToCustomer.Core.Domain;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pcf.GivingToCustomer.Integration
{
    public class PreferenceGateway
       : IPreferenceGateway
    {
        private readonly HttpClient _httpClient;

        public PreferenceGateway(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Preference> GetPreferenceByIdAsync(Guid preferenceId)
        {
            var response = await _httpClient.GetAsync($"api/v1/preferences/{preferenceId}");
            return await response.Content.ReadFromJsonAsync<Preference>();
        }

        public async Task<List<Preference>> GetPreferenceRangeByIdAsync(List<Guid> preferenceIds)
        {
            var preferenceIdsJson = JsonSerializer.Serialize(preferenceIds);
            var requestContent = new StringContent(preferenceIdsJson, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"api/v1/preferences/range", requestContent);
            return await response.Content.ReadFromJsonAsync<List<Preference>>();
        }

        public async Task<List<Preference>> GetPreferences()
        {
            var response = await _httpClient.GetAsync($"api/v1/preferences");
            return await response.Content.ReadFromJsonAsync<List<Preference>>();

        }
    }
}
