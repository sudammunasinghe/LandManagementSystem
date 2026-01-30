namespace LandManagement.Domain.Entities
{
    public class LandCrop : BaseEntity
    {
        public int Id { get; set; }
        public int LandId { get; set; }
        public int CropId { get; set; }
        public DateTime PlantingDate { get; set; }
        public double ExpectedYield { get; set; }
        public Land Land { get; set; }
        public Crop Crop { get; set; }
        public ICollection<LandInput> LandInputs { get; set; }
        public ICollection<LaborCost> LaborCosts { get; set; }
        public ICollection<Harvest> Harvests { get; set; }
    }
}
