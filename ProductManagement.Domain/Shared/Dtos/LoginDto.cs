using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Domain.Shared.Dtos
{
    public class LoginDto
    {
        [Required]
        [MaxLength(50)]
        public string Username { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string Password { get; set; } = string.Empty;
    }
}
