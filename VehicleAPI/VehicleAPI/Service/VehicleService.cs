using Newtonsoft.Json;
using System.Net.Http;
using VehicleAPI.DTOs;
using VehicleAPI.Model;

namespace VehicleAPI.Service
{
    public class VehicleService
    {
        private readonly HttpClient _httpClient;

        public VehicleService(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }
        public async Task<List<VehicleMake>?> GetAllMakes()
        {
            var response = await _httpClient.GetAsync("https://vpic.nhtsa.dot.gov/api/vehicles/getallmakes?format=json");
            if (response.IsSuccessStatusCode)
            {
                string? jsonString = await response.Content.ReadAsStringAsync();
                VehicleMakeDTO? vehicleResponse = JsonConvert.DeserializeObject<VehicleMakeDTO>(jsonString);
                return vehicleResponse?.Results;
            }
            return new List<VehicleMake>();
        }
        public async Task<List<VehicleType>?> GetVehicleTypeByMakeId(int makeId)
        {
            var response = await _httpClient.GetAsync($"https://vpic.nhtsa.dot.gov/api/vehicles/GetVehicleTypesForMakeId/{makeId}?format=json");
            if (response.IsSuccessStatusCode)
            {
                string? jsonString = await response.Content.ReadAsStringAsync();
                VehicleTypeDTO? vehicleResponse = JsonConvert.DeserializeObject<VehicleTypeDTO>(jsonString);
                return vehicleResponse?.Results;
            }
            return new List<VehicleType>();
        }
        public async Task<List<VehicleModel>?> GetModels(int makeId, int modelYear, string? vehicleType = null)
        {
            string url = $"https://vpic.nhtsa.dot.gov/api/vehicles/GetModelsForMakeIdYear/makeId/{makeId}/modelyear/{modelYear}?format=json";

            if (!string.IsNullOrEmpty(vehicleType))
            {
                url += $"&vehicleType={vehicleType}";
            }

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var vehicleModelResponse = JsonConvert.DeserializeObject<VehicleModelDTO>(jsonString);
                return vehicleModelResponse?.Results;
            }
            return new List<VehicleModel>();
        }
    }
}
