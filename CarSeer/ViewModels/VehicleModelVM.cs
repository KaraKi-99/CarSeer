using CarSeer.Models;

namespace CarSeer.ViewModels
{
    public class VehicleModelVM
    {
        public int? MakeId { get; set; }
        public int? ModelYear { get; set; }
        public int PageSize { get; set; } = 10;
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public List<VehicleModel>? Results { get; set; }
    }
}
