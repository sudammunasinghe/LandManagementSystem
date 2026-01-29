using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandManagement.Domain.Entities
{
    public class Harvest : BaseEntity
    {
        public int Id { get; set; }
        public int LandCropId { get; set; }
        public double Quantity { get; set; }
        public DateTime HarvestDateTime { get; set; }
        public LandCrop LandCrop { get; set; }
        public ICollection<Sale> Sales { get; set; }
    }
}
