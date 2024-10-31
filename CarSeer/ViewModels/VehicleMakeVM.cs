using CarSeer.Models;
namespace CarSeer.ViewModels
{
    public class VehicleMakeVM
    {
        public int ModelYear { get; set; }
        public List<string>? Years;
        public List<VehicleMake>? Results { get; set; }
    }
}
