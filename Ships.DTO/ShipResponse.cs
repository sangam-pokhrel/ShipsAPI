namespace Ships.DTO
{
    /// <summary>
    /// The class that contains the Ship Add/Update response data
    /// </summary>
    public class ShipResponse : ShipRequest
    {
        /// <summary>
        /// The System Generated Code of the Ship
        /// </summary>
        public string Code { get; set; }
    }
}
