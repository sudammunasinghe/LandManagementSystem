using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandManagement.Domain.Entities
{
    public class Land : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string DeedNumber { get; set; }
        public int OwnerId { get; set; }
        public double SizeInAcres { get; set; }
        public Owner Owner { get; set; }
        public ICollection<LandCrop> LandCrops { get; set; }
    }
}
