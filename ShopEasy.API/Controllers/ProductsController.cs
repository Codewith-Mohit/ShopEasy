using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopEasy.API.Dto;
using ShopEasy.API.Models;
using ShopEasy.API.Services;
using ShopEasy.API.Services.Notifications;

namespace ShopEasy.API.Controllers
{
    [ApiController] //<-- 	Enables model validation, automatic responses, etc.
    [Route("api/[controller]")] // <-- Automatically maps to /api/auth
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;  
        private readonly NotificationService _notificationService;    

        public ProductsController(IProductService productService, NotificationService notificationService)
        {            
            _productService = productService;
            _notificationService = notificationService;
        }

        // GET: Products
        [HttpGet]
        public async Task<List<Product>> GetAll()
        {
            return await _productService.GetProductsAsync();            
        }

        [HttpGet("{id}")]        
        public async Task<IActionResult> GetByID(int? id)
        {
            return Ok(_productService.GetProductByIdAsync(id));
        }

        // POST: Products/Create
        [HttpPost]       
        public async Task<IActionResult> Create(ProductCreateDto dto)
        {
            var product = await _productService.AddProductAsync(dto);
            CreatedAtActionResult obj = CreatedAtAction(nameof(GetByID), new { id = product.Id }, product);

            if (obj.StatusCode == 201) {
                _notificationService.Notify("Product created", "Email");
                return obj;
            }
            else
                return BadRequest("Product creation not succeded.");

        }        
        
        [Authorize]
        [HttpPut("{id}")]
        public async Task<Product> Update(int? id, ProductUpdateDto dto)
        {
            return await _productService.UpdateProductAsync(dto, id);
        }
        
        // POST: Products/Delete/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {           
            await _productService.DeleteProductAsync(id);
            return Ok();
        }
    }
}
