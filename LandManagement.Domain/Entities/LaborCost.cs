using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandManagement.Domain.Entities
{
    public class LaborCost : BaseEntity
    {
        public int Id { get; set; }
        public int LandCropId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public LandCrop LandCrop { get; set; }
    }
}
