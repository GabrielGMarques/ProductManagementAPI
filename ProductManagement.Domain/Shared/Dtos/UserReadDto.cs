using ProductManagement.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Domain.Shared.Dtos
{
    public class UserReadDto
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public UserRole Role { get; set; }
    }
}
