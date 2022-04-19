namespace Ships.Domain.Entities
{
    public class Ship
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal LengthInMeters { get; set; }
        public decimal WidthInMeters { get; set; }
    }
}
