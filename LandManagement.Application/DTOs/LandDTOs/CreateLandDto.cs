namespace LandManagement.Application.DTOs.LandDTOs
{
    public class CreateLandDto
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string DeedNumber { get; set; }
        public int OwnerId { get; set; }
        public double SizeInAcres { get; set; }

    }
}
