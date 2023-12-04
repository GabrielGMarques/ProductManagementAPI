using ProductManagement.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Domain.Dtos.Auth
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public UserRole Role { get; set; }
    }
}
