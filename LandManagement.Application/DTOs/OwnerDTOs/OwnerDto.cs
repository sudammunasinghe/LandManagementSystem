using LandManagement.Application.DTOs.LandDTOs;

namespace LandManagement.Application.DTOs.OwnerDTOs
{
    public class OwnerDto
    {
        public int OwnerId { get; set; }
        public string FullName { get; set; }
        public string NIC { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public ICollection<LandDto> LandDetails { get; set; }
    }
}
