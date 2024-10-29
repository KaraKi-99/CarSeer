using CarSeer.Models;
using Newtonsoft.Json;

namespace CarSeer.ViewModels
{
    public class VehicleTypeVM
    {
        public int? MakeId { get; set; }
        public int PageSize { get; set; } = 10;
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public List<VehicleType>? Results { get; set; }
    }
}
