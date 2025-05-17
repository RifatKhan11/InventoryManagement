namespace InventoryManagementNewVision.Areas.InventoryInfo.Models
{
    public class SPProductInfoModel
    {
        public string categoryName { get; set; }
        public string productName { get; set; }
        public int StockQuantity { get; set; }
        public decimal? totalPrice { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}
