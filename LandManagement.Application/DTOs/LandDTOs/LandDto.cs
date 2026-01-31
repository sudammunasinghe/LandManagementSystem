using LandManagement.Application.DTOs.OwnerDTOs;

namespace LandManagement.Application.DTOs.LandDTOs
{
    public class LandDto
    {
        public int LandId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string DeedNumber { get; set; }
        public double SizeInAcres { get; set; }
        public OwnerDto? OwnerDetails { get; set; }

    }
}
