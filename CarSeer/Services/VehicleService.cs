using CarSeer.ViewModels;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace CarSeer.Services
{
    public interface IVehicleService
    {
        Task<VehicleMakeVM?> GetAllMakes();
        Task<VehicleTypeVM?> GetVehicleTypeByMakeId(VehicleTypeVM model);
        Task<VehicleModelVM?> GetModels(VehicleModelVM model);
    }
    public class VehicleService : IVehicleService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMemoryCache _cache;
        public VehicleService(HttpClient httpClient, IHttpClientFactory httpClientFactory, IMemoryCache cache)
        {
            _httpClient = httpClient;
            _httpClientFactory = httpClientFactory;
            _cache = cache;
        }

        public async Task<VehicleMakeVM?> GetAllMakes()
        {
            var cacheKey = "allMakes";
            if (!_cache.TryGetValue(cacheKey, out VehicleMakeVM? cachedData))
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync("https://vpic.nhtsa.dot.gov/api/vehicles/getallmakes?format=json");

                if (!response.IsSuccessStatusCode)
                {
                    return new VehicleMakeVM();
                }
                string jsonString = await response.Content.ReadAsStringAsync();
                cachedData = JsonConvert.DeserializeObject<VehicleMakeVM>(jsonString);

                _cache.Set(cacheKey, cachedData, TimeSpan.FromHours(1));
            }

            if (cachedData?.Results != null && cachedData.Results.Count > 0)
            {
                return cachedData;
            }

            return new VehicleMakeVM();
        }

        public async Task<VehicleTypeVM?> GetVehicleTypeByMakeId(VehicleTypeVM model)
        {
            var response = await _httpClient.GetAsync($"https://vpic.nhtsa.dot.gov/api/vehicles/GetVehicleTypesForMakeId/{model.MakeId}?format=json");

            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                VehicleTypeVM? vehicleTypeResponse = JsonConvert.DeserializeObject<VehicleTypeVM>(jsonString);

                int totalItems = vehicleTypeResponse?.Results?.Count ?? 0;

                var totalPages = (int)Math.Ceiling(totalItems / (double)model.PageSize);

                var pagedResults = vehicleTypeResponse?.Results?
                                            .Skip((model.CurrentPage - 1) * model.PageSize)
                                            .Take(model.PageSize)
                                            .ToList();

                return new VehicleTypeVM
                {
                    Results = pagedResults,
                    TotalCount = totalItems,
                    CurrentPage = model.CurrentPage,
                    TotalPages = totalPages,
                    PageSize = model.PageSize,
                    MakeId = model.MakeId
                };
            }
            return new VehicleTypeVM();
        }
        public async Task<VehicleModelVM?> GetModels(VehicleModelVM model)
        {
            var response = await _httpClient.GetAsync($"https://vpic.nhtsa.dot.gov/api/vehicles/GetModelsForMakeIdYear/makeId/{model.MakeId}/modelyear/{model.ModelYear}?format=json");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var vehicleModelResponse = JsonConvert.DeserializeObject<VehicleModelVM>(jsonString);

                int totalItems = vehicleModelResponse?.Results?.Count ?? 0;

                var totalPages = (int)Math.Ceiling(totalItems / (double)model.PageSize);

                var pagedResults = vehicleModelResponse?.Results?
                                             .Skip((model.CurrentPage - 1) * model.PageSize)
                                             .Take(model.PageSize)
                                             .ToList();

                return new VehicleModelVM
                {
                    Results = pagedResults,
                    TotalCount = totalItems,
                    CurrentPage = model.CurrentPage,
                    TotalPages = totalPages,
                    PageSize = model.PageSize,
                    MakeId = model.MakeId,
                    ModelYear = model.ModelYear
                };
            }
            return new VehicleModelVM();
        }
    }
}
