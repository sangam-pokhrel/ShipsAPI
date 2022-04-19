using Microsoft.EntityFrameworkCore;
using Ships.Domain;
using Ships.Domain.Entities;
using Ships.DTO;

namespace Ships.UnitTests
{
    public class UnitTestFixture
    {
        protected readonly Ship ship;
        protected readonly ShipResponse shipResponse;
        protected readonly string shipCode;

        protected UnitTestFixture()
        {
            shipCode = "ABCD-1234-A1";
            shipResponse = new ShipResponse
            {
                Code = shipCode,
                Name = "Test Ship",
                LengthInMeters = 100,
                WidthInMeters = 20
            };

            ship = new Ship
            {
                Name = shipResponse.Name,
                LengthInMeters = shipResponse.LengthInMeters,
                WidthInMeters = shipResponse.WidthInMeters,
                Code = shipCode
            };
        }

        protected async Task<ShipsDbContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ShipsDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var dbContext = new ShipsDbContext(options);
            dbContext.Database.EnsureCreated();

            dbContext.Ships.Add(ship);    //Add default Ship

            await dbContext.SaveChangesAsync();
            dbContext.Entry(ship).State = EntityState.Detached;
            return dbContext;
        }
    }
}
