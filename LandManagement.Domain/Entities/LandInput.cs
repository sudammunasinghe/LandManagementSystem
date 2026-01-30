namespace LandManagement.Domain.Entities
{
    public class LandInput : BaseEntity
    {
        public int Id { get; set; }
        public int LandCropId { get; set; }
        public int FertilizerId { get; set; }
        public int Quantity { get; set; }
        public DateTime AppliedDate { get; set; }
        public LandCrop LandCrop { get; set; }
        public Fertilizer Fertilizer { get; set; }
    }
}
