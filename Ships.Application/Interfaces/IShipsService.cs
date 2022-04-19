using Ships.DTO;

namespace Ships.Application.Interfaces
{
    public interface IShipsService
    {
        Task<ShipResponse> AddShip(ShipRequest req);
        Task<ShipResponse> UpdateShip(ShipRequest req, string code);
        Task<IEnumerable<ShipResponse>> GetShips();
        Task<ShipResponse> GetShip(string code);
        Task DeleteShip(string code);
    }
}
