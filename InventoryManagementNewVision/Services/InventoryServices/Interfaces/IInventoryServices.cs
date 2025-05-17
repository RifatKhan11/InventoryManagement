using InventoryManagementNewVision.Areas.InventoryInfo.Models;
using InventoryManagementNewVision.Data.Entity;

namespace InventoryManagementNewVision.Services.InventoryServices.Interfaces
{
    public interface IInventoryServices
    {
        #region Category
        public Task<IEnumerable<Catagory>> GetAllItemCategory();
        Task<Catagory> GetCategoryById(int id);
        public Task<int> SaveCategory(Catagory catagory);
        Task<int> DeleteCat(int id);
        #endregion


        #region  Product
        Task<List<ProductInfo>> GetAllAsync();
        Task<ProductInfo> GetByIdAsync(int id);
        Task<ProductInfo> SaveAsync(ProductInfo product);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<SPProductInfoModel>> GetProductSummery();
        #endregion

    }
}
