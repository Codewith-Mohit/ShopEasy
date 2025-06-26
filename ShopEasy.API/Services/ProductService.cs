using AutoMapper;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopEasy.API.Data;
using ShopEasy.API.Dto;
using ShopEasy.API.Models;

namespace ShopEasy.API.Services
{
    public interface IProductService
    {
        public Task<List<Product>> GetProductsAsync();
        public Task<Product> GetProductByIdAsync(int? productId);                
        public Task<Product> AddProductAsync(ProductCreateDto product);
        public Task<Product> UpdateProductAsync(ProductUpdateDto product, int? productId);
        public Task DeleteProductAsync(int? productId);
    }

    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Product> AddProductAsync(ProductCreateDto dto)
        {
            var product = _mapper.Map<Product>(dto);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            var result = _mapper.Map<ProductReadDto>(product);

            return new Product { CreatedAt = DateTime.Now, Description = result.Description, Name = result.Description, Price = result.Price, Id= result.Id};
        }

        public async Task DeleteProductAsync(int? productId)
        {
            var product = await _context.Products.FindAsync(productId);            
            if (product != null)
                _context.Products.Remove(product);

            await _context.SaveChangesAsync();            
        }

        public async Task<Product> GetProductByIdAsync(int? productId)
        {
            var product = _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (product == null) { throw new Exception("No Product found with mentioned ID"); }
            else
                return await product;            
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            var products = await _context.Products.ToListAsync();
            if (products is not null || products?.Count() > 0)
                return products;
            else
                throw new InvalidDataException("No Products available..");               
        }

        public async Task<Product> UpdateProductAsync(ProductUpdateDto dto, int? productId)
        {
            if (productId == null)
                throw new InvalidDataException("invalid Product Id.");

            var productU = await _context.Products.FindAsync(productId);

            if (productU == null)
                throw new InvalidDataException("Product not available.");

            productU.Name = dto.Name;
            productU.Description = dto.Description;
            productU.Price = dto.Price;

            await _context.SaveChangesAsync();
            
            return _context.Products.FirstOrDefault(p=>p.Id == productId);
        }
    }


}
