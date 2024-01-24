using ELCLite.SharedKernel.SharedObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ELCLite.ApiGateway.Api.Features.Vehicles.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        /// <summary>
        /// Get a vehicle
        /// </summary>
        /// <param name="id">Unique identifier</param>
        /// <param name="cancellationToken"></param>
        [HttpGet("{id}")]
        [Authorize(Policy = nameof(PermissionKey.VehicleRead))]
        public IActionResult GetAsync(string id, CancellationToken cancellationToken = default)
        {
            if (id == string.Empty)
            {
                return BadRequest();
            }

            return Ok($"You requested vehicle id {id}");
        }
    }
}
