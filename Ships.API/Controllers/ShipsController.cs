using FluentValidation;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Ships.API.Helpers;
using Ships.Application.Interfaces;
using Ships.DTO;
using System.Net;

namespace Ships.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [EnableCors("CorsPolicy")]
    [ApiController]
    [ApiVersion("1")]
    public class ShipsController : ControllerBase
    {
        private readonly IShipsService _shipsService;
        private readonly IValidator<ShipRequest> _shipRequestValidator;

        public ShipsController(IShipsService shipsService, IValidator<ShipRequest> shipRequestValidator)
        {
            _shipsService = shipsService;
            _shipRequestValidator = shipRequestValidator;
        }

        /// <summary>
        /// Get List of Ships
        /// </summary>
        /// <returns>List of Ships</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ShipResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> List()
        {
            return Ok(await _shipsService.GetShips());
        }

        /// <summary>
        /// Get Ship Details
        /// </summary>
        /// <param name="code">The Code of the Ship, of which, the details are to be fetched</param>
        /// <returns>Detail of the specific Ship</returns>
        [HttpGet("{code}")]
        [ProducesResponseType(typeof(ShipResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Details(string code)
        {
            return Ok(await _shipsService.GetShip(code));
        }

        /// <summary>
        /// Adds a new Ship
        /// </summary>
        /// <param name="request">The List containing the Ship details</param>
        /// <returns>The Ship response with newly created code</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ShipResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ValidationErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Add(ShipRequest request)
        {
            var validationResults = await _shipRequestValidator.ValidateAsync(request);
            if (!validationResults.IsValid)
            {
                return BadRequest(validationResults.Errors.ToValidationMessage());
            }

            return Ok(await _shipsService.AddShip(request));
        }

        /// <summary>
        /// Updates an existing Ship
        /// </summary>
        /// <param name="code">The code of the Ship to be updated</param>
        /// <param name="request">The object containing the Ship details</param>
        /// <returns>The Ship response with the updated Ship data</returns>
        [HttpPut("{code}")]
        [ProducesResponseType(typeof(ShipResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ValidationErrorResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update(ShipRequest request, string code)
        {
            var validationResults = await _shipRequestValidator.ValidateAsync(request);
            if (!validationResults.IsValid)
            {
                return BadRequest(validationResults.Errors.ToValidationMessage());
            }

            return Ok(await _shipsService.UpdateShip(request, code));
        }

        /// <summary>
        /// Deletes an existing Ship
        /// </summary>
        /// <param name="code">The Code of the Ship to be deleted</param>
        [HttpDelete("{code}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(string code)
        {
            await _shipsService.DeleteShip(code);
            return Ok();
        }
    }
}
