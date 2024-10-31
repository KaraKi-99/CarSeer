using CarSeer.Services;
using CarSeer.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CarSeer.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly IVehicleService _vehicleService;
        public VehiclesController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task<IActionResult> Index()
        {
            VehicleMakeVM? result = await _vehicleService.GetAllMakes();

            int startYear = 1950;
            int currentYear = DateTime.Now.Year;

            result.Years = Enumerable.Range(startYear, currentYear - startYear + 1)
                            .Select(year => year.ToString())
                            .ToList();
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetVehicleTypeGrid(VehicleTypeVM model)
        {
            VehicleTypeVM? result = await _vehicleService.GetVehicleTypeByMakeId(model);
            return PartialView("_VehicleType", result);
        }
        [HttpPost]
        public async Task<IActionResult> GetVehicleModelsGrid(VehicleModelVM model)
        {
            VehicleModelVM? result = await _vehicleService.GetModels(model);
            return PartialView("_VehicleModel", result);
        }
    }
}
