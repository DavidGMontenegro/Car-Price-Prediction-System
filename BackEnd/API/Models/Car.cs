namespace FinalAPI.Models
{
    public class Car
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public int Year { get; set; }
        public int Kilometer { get; set; }
        public string FuelType { get; set; }
        public string Transmission { get; set; }
        public string Location { get; set; }
        public string Color { get; set; }
        public string Owner { get; set; }
        public string SellerType { get; set; }
        public string Engine { get; set; }
        public string MaxPower { get; set; }
        public string MaxTorque { get; set; }
        public string Drivetrain { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public decimal SeatingCapacity { get; set; }
        public decimal FuelTankCapacity { get; set; }
    }

    public class CarParameters
    {
        public int Year { get; set; }
        public int Kilometer { get; set; }
        public string FuelType { get; set; }
        public string Transmission { get; set; }
        public string Owner { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string SellerType { get; set; }
        public string Engine { get; set; }
        public string Drivetrain { get; set; }
    }
}
