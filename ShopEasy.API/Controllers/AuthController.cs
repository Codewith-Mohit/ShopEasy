using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopEasy.API.Data;
using ShopEasy.API.Dto;
using ShopEasy.API.Helpers;
using ShopEasy.API.Models;

namespace ShopEasy.API.Controllers
{
    [ApiController] //<-- 	Enables model validation, automatic responses, etc.
    [Route("api/[controller]")] // <-- Automatically maps to /api/auth
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext? _context;
        private readonly JwtTokenGenerator _jwt;

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signManager;


        public AuthController(AppDbContext context, SignInManager<IdentityUser> signManager, UserManager<IdentityUser> userManager,JwtTokenGenerator jwt )
        {
            _context = context;
            _jwt = jwt;
            _signManager = signManager;
            _userManager = userManager;
        }

        [HttpPost("register")]      
        public async Task<IActionResult> Register(UserRegisterDto dto) //<--Accepts a username + password, hashes it with Bcrypt, and stores in DB
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Check if the username already exists
            //if (await _context!.Users.AnyAsync(u => u.Username == dto.Username))            
            //    return BadRequest("Username already exists.");            

            if ( await _userManager.Users.AnyAsync(u => u.UserName == dto.Username))            
                return BadRequest("Username already exists.");            

            // Create new user with hashed password
            var user = new IdentityUser
            {
                UserName = dto.Username,              
                Email = dto.Email                
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            
            //_context. Users.Add(user);
            //await _context.SaveChangesAsync();

            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(UserLoginDto dto) //<-- Verifies password using Bcrypt and returns a signed JWT token
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _signManager.PasswordSignInAsync
                (dto.Username, dto.Password, false, false);


            //var user = _context!.Users.FirstOrDefault(u => u.Username == dto.Username);
            //if (user is null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            //    return Unauthorized("Invalid username or password...");
            //var token = _jwt.GenerateToken(user); //<--	Uses the helper to build the token using user claims
            //return Ok(new { token });

            if (!result.Succeeded)
                return Unauthorized("Invalid user..");

            return Ok("Logged In.");

        }
    }
}
