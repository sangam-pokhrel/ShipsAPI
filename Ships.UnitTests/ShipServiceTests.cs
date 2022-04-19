using FluentAssertions;
using Moq;
using Ships.Application.Interfaces;
using Ships.Application.Services;
using Ships.Common.Constants;
using Ships.DTO;
using Xunit;

namespace Ships.UnitTests
{
    public class ShipServiceTests : UnitTestFixture
    {
        private IShipsService _shipsService;
        private readonly Mock<ICodeGeneratorService> _randomCodeServiceMock;

        public ShipServiceTests()
        {
            _randomCodeServiceMock = new Mock<ICodeGeneratorService>();
        }

        #region Tests

        [Fact]
        public async Task GetShips_Should_Return_ShipsList()
        {
            _shipsService = new ShipsService(await GetDbContext(), _randomCodeServiceMock.Object);
            var shipsResult = await _shipsService.GetShips();
            shipsResult.Should().ContainEquivalentOf(shipResponse);
        }

        [Fact]
        public async Task GetShip_With_CorrectCode_Should_Return_Ship()
        {
            _shipsService = new ShipsService(await GetDbContext(), _randomCodeServiceMock.Object);
            var shipResult = await _shipsService.GetShip(shipCode);
            shipResult.Should().BeEquivalentTo(shipResponse);
        }

        [Fact]
        public async Task GetShip_With_Incorrect_ShipCode_Should_Throw_Http_Exception_With_NotFound_Msg()
        {
            var incorrectShipCode = "AAAA-1234-A4";
            _shipsService = new ShipsService(await GetDbContext(), _randomCodeServiceMock.Object);
            var shipsRequest = async () => await _shipsService.GetShip(incorrectShipCode);
            await shipsRequest.Should().ThrowAsync<HttpRequestException>().WithMessage(ErrorConstants.ShipNotFoundMsg);
        }

        [Fact]
        public async Task AddShip_Should_Add_NewShip()
        {
            var newShipCode = "AABB-1189-B2";
            var shipRequest = new ShipRequest
            {
                Name = "Test Ship 2",
                LengthInMeters = 200,
                WidthInMeters = 40
            };

            var shipResponse = new ShipResponse
            {
                Code = newShipCode,
                Name = shipRequest.Name,
                LengthInMeters = shipRequest.LengthInMeters,
                WidthInMeters = shipRequest.WidthInMeters
            };

            _randomCodeServiceMock.Setup(x => x.GenerateShipCode()).Returns(newShipCode).Verifiable();
            _shipsService = new ShipsService(await GetDbContext(), _randomCodeServiceMock.Object);
            var result = await _shipsService.AddShip(shipRequest);
            result.Should().BeEquivalentTo(shipResponse);
            _randomCodeServiceMock.Verify();

            //check if data was saved properly
            var getResult = await _shipsService.GetShip(newShipCode);
            getResult.Should().BeEquivalentTo(shipResponse);
        }

        [Fact]
        public async Task UpdateShip_With_Correct_ShipCode_Should_Update_ExistingShip()
        {
            var shipRequest = new ShipRequest
            {
                Name = "Test Ship Update",
                LengthInMeters = 210,
                WidthInMeters = 41
            };

            var shipResponse = new ShipResponse
            {
                Code = shipCode,
                Name = shipRequest.Name,
                LengthInMeters = shipRequest.LengthInMeters,
                WidthInMeters = shipRequest.WidthInMeters
            };

            _shipsService = new ShipsService(await GetDbContext(), _randomCodeServiceMock.Object);
            var result = await _shipsService.UpdateShip(shipRequest, shipCode);
            result.Should().BeEquivalentTo(shipResponse);

            //check if data was saved properly
            var getResult = await _shipsService.GetShip(shipCode);
            getResult.Should().BeEquivalentTo(shipResponse);
        }

        [Fact]
        public async Task UpdateShip_With_Incorrect_ShipCode_Should_Throw_Http_Exception_With_NotFound_Msg()
        {
            var incorrectShipCode = "AAAA-1234-A4";
            var shipRequest = new ShipRequest
            {
                Name = "Test Ship Update",
                LengthInMeters = 210,
                WidthInMeters = 41
            };

            _shipsService = new ShipsService(await GetDbContext(), _randomCodeServiceMock.Object);
            var request = async () => await _shipsService.UpdateShip(shipRequest, incorrectShipCode);
            await request.Should().ThrowAsync<HttpRequestException>().WithMessage(ErrorConstants.ShipNotFoundMsg);
        }

        [Fact]
        public async Task DeleteShip_With_Correct_ShipCode_Should_Delete_Ship()
        {
            _shipsService = new ShipsService(await GetDbContext(), _randomCodeServiceMock.Object);
            await _shipsService.DeleteShip(shipCode);

            //check if data was deleted
            var getRequest = async () => await _shipsService.GetShip(shipCode);
            await getRequest.Should().ThrowAsync<HttpRequestException>().WithMessage(ErrorConstants.ShipNotFoundMsg);
        }

        [Fact]
        public async Task DeleteShip_With_Incorrect_ShipCode_Should_Throw_Http_Exception_With_NotFound_Msg()
        {
            var incorrectShipCode = "AAAA-1234-A4";
            _shipsService = new ShipsService(await GetDbContext(), _randomCodeServiceMock.Object);
            var shipsRequest = async () => await _shipsService.DeleteShip(incorrectShipCode);
            await shipsRequest.Should().ThrowAsync<HttpRequestException>().WithMessage(ErrorConstants.ShipNotFoundMsg);
        }

        #endregion Tests
    }
}
