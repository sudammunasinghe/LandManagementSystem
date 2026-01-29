using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandManagement.Domain.Entities
{
    public class Fertilizer : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Unit { get; set; }
        public decimal CostPerUnit { get; set; }
        public ICollection<LandInput> LandInputs { get; set; }
    }
}
