namespace LandManagement.Domain.Entities
{
    public class Crop : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<LandCrop> LandCrops { get; set; }
    }
}
