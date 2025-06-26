using System.ComponentModel.DataAnnotations;
namespace ShopEasy.API.Dto
{
    public class UserLoginDto
    {
        
        [RegularExpression("^[a-zA-Z0-9]{4,20}$", ErrorMessage = "Username must be 4-20 characters and alphanumeric.")]
        public required string Username { get; set; }
                
        public required string Password { get; set; }
    }

    public class UserRegisterDto
    {

        [RegularExpression("^[a-zA-Z0-9]{4,20}$", ErrorMessage = "Username must be 4-20 characters and alphanumeric.")]
        public required string Username { get; set; }

        
        public required string Password { get; set; }

        public string? Email { get; set; }
    }
}
