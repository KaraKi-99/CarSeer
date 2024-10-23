using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using VehicleAPI.Model;
using VehicleAPI.Service;

namespace VehicleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly VehicleService _vehicleService;
        public VehiclesController(VehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet("GetVehicleTypesForMakeId")]
        public async Task<IActionResult> GetVehicleTypesForMakeId()
        {
            List<VehicleMake>? response = await _vehicleService.GetAllMakes();
            return Ok(response);
        }

        [HttpGet("GetVehicleTypesForMakeId/{makeId}")]
        public async Task<IActionResult> GetVehicleTypesForMakeId(int makeId)
        {
            List<VehicleType>? response = await _vehicleService.GetVehicleTypeByMakeId(makeId);
            return Ok(response);
        }

        [HttpGet("GetModelsForMakeIdYear/{makeId}/{modelYear}")]
        public async Task<IActionResult> GetModelsForMakeIdYear(int makeId, int modelYear, [FromQuery] string? vehicleType = null)
        {
            List<VehicleModel>? response = await _vehicleService.GetModels(makeId, modelYear, vehicleType);
            return Ok(response);
        }
    }
}
