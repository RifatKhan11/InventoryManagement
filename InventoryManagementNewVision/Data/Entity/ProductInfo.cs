namespace InventoryManagementNewVision.Data.Entity
{
    public class ProductInfo:Base
    {
        public string Name { get; set; } 
        public int? catagoryId { get; set; }
        public Catagory catagory { get; set; } 
        public decimal? Price { get; set; }
        public int? StockQuantity { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}
