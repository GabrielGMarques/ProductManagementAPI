using ProductManagement.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Domain.Dtos.Auth
{
    public class RegisterDto
    {
        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string Username { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        [MinLength(6)] 
        public string Password { get; set; } = string.Empty;
        [Required]
        public UserRole Role { get; set; }
    }
}
