using Microsoft.EntityFrameworkCore;
using Ships.Application.Interfaces;
using Ships.Common.Constants;
using Ships.Domain;
using Ships.Domain.Entities;
using Ships.DTO;
using System.Net;

namespace Ships.Application.Services
{
    public class ShipsService : IShipsService
    {
        private readonly ShipsDbContext _shipsDbContext;
        private readonly ICodeGeneratorService _randomCodeService;

        public ShipsService(ShipsDbContext shipsDbContext, ICodeGeneratorService codeGeneratorService)
        {
            _shipsDbContext = shipsDbContext;
            _randomCodeService = codeGeneratorService;
        }

        public async Task<ShipResponse> AddShip(ShipRequest req)
        {
            var ship = new Ship
            {
                Code = _randomCodeService.GenerateShipCode(),
                Name = req.Name,
                LengthInMeters = req.LengthInMeters,
                WidthInMeters = req.WidthInMeters
            };

            _shipsDbContext.Add(ship);
            await _shipsDbContext.SaveChangesAsync();

            return new ShipResponse {
                Code = ship.Code,
                Name = ship.Name,
                LengthInMeters = ship.LengthInMeters,
                WidthInMeters = ship.WidthInMeters
            };
        }

        public async Task<ShipResponse> UpdateShip(ShipRequest req, string code)
        {
            var ship = await _shipsDbContext.Ships.Where(x => x.Code == code).FirstOrDefaultAsync();
            if (ship == null)
                throw new HttpRequestException(ErrorConstants.ShipNotFoundMsg, null, statusCode: HttpStatusCode.NotFound);

            ship.Name = req.Name;
            ship.LengthInMeters = req.LengthInMeters;
            ship.WidthInMeters = req.WidthInMeters;

            _shipsDbContext.Update(ship);
            await _shipsDbContext.SaveChangesAsync();

            return new ShipResponse
            {
                Code = code,
                Name = req.Name,
                LengthInMeters = req.LengthInMeters,
                WidthInMeters = req.WidthInMeters
            };
        }

        public async Task<ShipResponse> GetShip(string code)
        {
            var ship = await _shipsDbContext.Ships.Where(x => x.Code == code)
                        .Select(x => new ShipResponse {
                            Code = x.Code,
                            Name = x.Name,
                            LengthInMeters = x.LengthInMeters,
                            WidthInMeters = x.WidthInMeters
                        })
                        .FirstOrDefaultAsync();

            if (ship == null)
                throw new HttpRequestException(ErrorConstants.ShipNotFoundMsg, null, statusCode: HttpStatusCode.NotFound);

            return ship;
        }

        public async Task<IEnumerable<ShipResponse>> GetShips()
        {
            return await _shipsDbContext.Ships
                        .Select(x => new ShipResponse
                        {
                            Code = x.Code,
                            Name = x.Name,
                            LengthInMeters = x.LengthInMeters,
                            WidthInMeters = x.WidthInMeters
                        })
                        .ToListAsync();
        }

        public async Task DeleteShip(string code)
        {
            if (!await _shipsDbContext.Ships.Where(x => x.Code == code).AnyAsync())
                throw new HttpRequestException(ErrorConstants.ShipNotFoundMsg, null, statusCode: HttpStatusCode.NotFound);

            _shipsDbContext.Remove(new Ship { Code = code });
            await _shipsDbContext.SaveChangesAsync();
        }
    }
}
