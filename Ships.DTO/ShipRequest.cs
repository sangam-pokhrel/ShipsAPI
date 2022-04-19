namespace Ships.DTO
{
    /// <summary>
    /// The class used to make Add/Update Ship Request
    /// </summary>
    public class ShipRequest
    {
        /// <summary>
        /// The Name of the Ship
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The Length of the Ship in Meters
        /// </summary>
        public decimal LengthInMeters { get; set; }

        /// <summary>
        /// The Width of the Ship in Meters
        /// </summary>
        public decimal WidthInMeters { get; set; }
    }
}
