using CarSeer.Models;
using CarSeer.Services;
using CarSeer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CarSeer.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly IVehicleService _vehicleService;
        public VehiclesController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task<IActionResult> GetAllMakes(VehicleMakeVM model)
        {
            var result = await _vehicleService.GetAllMakes(model);
            return View(result);
        }

        public async Task<IActionResult> GetVehicleType(VehicleTypeVM model)
        {
            VehicleTypeVM? result = await _vehicleService.GetVehicleTypeByMakeId(model);
            return View(result);
        }

        public async Task<IActionResult> GetVehicleModels(VehicleModelVM model)
        {
            VehicleModelVM? result = await _vehicleService.GetModels(model);
            return View(result);
        }
    }
}
