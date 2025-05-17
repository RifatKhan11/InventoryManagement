using InventoryManagementNewVision.Areas.InventoryInfo.Models;
using InventoryManagementNewVision.Data;
using InventoryManagementNewVision.Data.Entity;
using InventoryManagementNewVision.Models;
using InventoryManagementNewVision.Services.Dapper.IInterfaces;
using InventoryManagementNewVision.Services.InventoryServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementNewVision.Services.InventoryServices
{
    public class InventoryServices:IInventoryServices
    {
        private readonly AppDbContext _context;
        private readonly IDapper _dapper;

        public InventoryServices(AppDbContext context, IDapper dapper)
        {
            _context = context;
            this._dapper = dapper;
        }

        #region Category
        public async Task<IEnumerable<Catagory>> GetAllItemCategory()
        {
            var list =await _context.catagories.ToListAsync();
            return list;
        }
        public async Task<Catagory> GetCategoryById(int id)
        {
            return await _context.catagories.FindAsync(id);
        }
        public async Task<int> SaveCategory(Catagory catagory)
        {
            if (catagory.Id == 0)
               _context.catagories.Add(catagory);
            else
                _context.catagories.Update(catagory);

             await _context.SaveChangesAsync();

            return catagory.Id;
        }
        public async Task<int> DeleteCat(int id)
        {
            var cat = _context.catagories.Find(id);
            if (cat == null) return 0;

            _context.catagories.Remove(cat);
            await _context.SaveChangesAsync();
            return 1;
        }


        #endregion
        #region Product
        public async Task<List<ProductInfo>> GetAllAsync()
        {
            return await _context.productInfos.Include(p => p.catagory).ToListAsync();
        }

        public async Task<ProductInfo> GetByIdAsync(int id)
        {
            return await _context.productInfos.FindAsync(id);
        }

        public async Task<ProductInfo> SaveAsync(ProductInfo product)
        {
            if (product.Id == 0)
            {
                product.LastUpdated = DateTime.Now;
                _context.productInfos.Add(product);
            }
            else
            {
                product.LastUpdated = DateTime.Now;
                _context.productInfos.Update(product);
            }

            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.productInfos.FindAsync(id);
            if (entity == null) return false;

            _context.productInfos.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<SPProductInfoModel>> GetProductSummery()
        {
            var data = await _dapper.FromSqlAsync<SPProductInfoModel>($"GetAllProductInfo");
            return data;
        }
        #endregion

    }
}
