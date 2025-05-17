using InventoryManagementNewVision.Areas.InventoryInfo.Models;
using InventoryManagementNewVision.Data.Entity;
using InventoryManagementNewVision.Services.InventoryServices.Interfaces;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementNewVision.Areas.InventoryInfo.Controllers
{
    [Area("InventoryInfo")]
    public class ProductController : Controller
    {
        private readonly IInventoryServices _iInventoryServices;
        public ProductController(IInventoryServices inventoryServices)
        {
            _iInventoryServices = inventoryServices;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region Category
        [HttpGet]
        public async Task<IActionResult> ProductCategory() 
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetCatagories()
        {
            var list = await _iInventoryServices.GetAllItemCategory();
            return Json(list);
        }

        [HttpPost]
        public async Task<IActionResult> SaveCatagory([FromBody] Catagory catagory)
        {
            if (string.IsNullOrWhiteSpace(catagory.Name))
                return Json("Name is required");
            Thread.Sleep(5000);
            var data = new Catagory
                {
                    Name = catagory.Name,
                    Id = catagory.Id
                };
            await _iInventoryServices.SaveCategory(catagory);
            return Json(data.Id);
        }

        [HttpGet]
        public async Task<IActionResult> GetCatagory(int id)
        {
            var cat = await _iInventoryServices.GetCategoryById(id);
            if (cat == null) return NotFound();
            return Json(cat);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCatagory(int id)
        {
            var data =await _iInventoryServices.DeleteCat(id);
            return Json(data);
        }
        #endregion

        #region Product

        [HttpGet]
        public async Task<IActionResult> ProductInfo()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _iInventoryServices.GetAllAsync();
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] ProductInfo product)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data");
            Thread.Sleep(5000);

            var savedProduct = await _iInventoryServices.SaveAsync(product);
            return Ok(savedProduct);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _iInventoryServices.GetByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            Thread.Sleep(5000);
            var result = await _iInventoryServices.DeleteAsync(id);
            if (!result) return NotFound();
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetProductSummery()
        {
            var data = await _iInventoryServices.GetProductSummery();
            var model = new ProductInfoModel
            {
                sPProductInfoModels = data
            };
            return View(model);
        }
        #endregion
    }
}
