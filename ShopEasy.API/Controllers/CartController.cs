using AutoMapper;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using ShopEasy.API.Data;
using ShopEasy.API.Dto;
using ShopEasy.API.Models;
using ShopEasy.API.Services;
using System.Security.Claims;

namespace ShopEasy.API.Controllers
{
    [ApiController]
    public class CartController : Controller 
    {

        public readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICustomLoggerProvider _loggerProvider;

        public CartController(AppDbContext context,  IMapper mapper, ICustomLoggerProvider myLoggerProvider)
        {
            _context = context;
            _mapper = mapper;
            _loggerProvider = myLoggerProvider; 
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartRequest request)
        {
            int? userId = null;
            string? guestId = null;

            _loggerProvider.writemsg("started : AddtocardRequest controller.");
            //Fetching userid if user is authenticated. else getting a guestId from cookies, if already present else generate a new guestId 
            if (request.UserId == Convert.ToString(0) || string.IsNullOrWhiteSpace(request.UserId.ToString()))
                guestId = request.GuestId.ToString(); 
            else if (User.Identity.IsAuthenticated)
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (int.TryParse(userIdClaim, out int parsedUserId))
                    userId = parsedUserId;
            }
            else
                return BadRequest("Unable to find the user/guest...");
            

            if (userId == null && string.IsNullOrEmpty(guestId))
                return BadRequest("Unable to find the user/guest...");

            Cart cart = null;
            // 2. Find existing cart (or create new one if not exists)
            if (userId == 0 || string.IsNullOrEmpty(userId.ToString()))            
                cart = _context.Carts.FirstOrDefault(c => c.GuestId == guestId);            
            else            
                cart = _context.Carts.FirstOrDefault(c => c.UserId == userId.ToString());

            if (cart == null)
            {
                cart = new Cart
                {
                    GuestId = request.GuestId.ToString(),
                    UserId = request.UserId,
                    Items = new List<CartItem>() {
                        new CartItem {
                            ProductId = request.ProductId,
                            Price = _context.Products?.FirstOrDefault(p => p.Id == request.ProductId).Price,
                            Quantity = request.Quantity
                        }
                    }
                };
                _context.Carts.Add(cart);
            }
            else
            {
                // 3. Check if item already exists in cart

                var existingCartItem = _context.cartItems.FirstOrDefault(i => i.CartId == cart.CartId && i.ProductId == request.ProductId);
                if (existingCartItem == null)
                {
                    _context.cartItems.Add(new CartItem {
                        ProductId = request.ProductId,
                        Price = _context.Products?.FirstOrDefault(p => p.Id == request.ProductId).Price,
                        Quantity = request.Quantity, 
                        CartId = cart.CartId
                     });
                }
                else                
                    existingCartItem.Quantity += request.Quantity;                                 
            }
            _loggerProvider.writemsg("Ended : AddtocardRequest controller.");
            await _context.SaveChangesAsync();
            return Ok(cart);
        }
    }
}
