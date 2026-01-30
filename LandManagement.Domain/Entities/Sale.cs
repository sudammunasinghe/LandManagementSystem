namespace LandManagement.Domain.Entities
{
    public class Sale : BaseEntity
    {
        public int Id { get; set; }
        public int HarvestId { get; set; }
        public double QuantitySold { get; set; }
        public decimal PricePerUnit { get; set; }
        public DateTime SaleDate { get; set; }
        public Harvest Harvest { get; set; }
    }
}
