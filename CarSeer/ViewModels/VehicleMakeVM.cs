using CarSeer.Models;

namespace CarSeer.ViewModels
{
    public class VehicleMakeVM
    {
        public int PageSize { get; set; } = 10;
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public List<VehicleMake>? Results { get; set; }
    }
}
