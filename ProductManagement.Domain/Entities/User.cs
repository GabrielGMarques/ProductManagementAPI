using ProductManagement.Domain.Entities.Base;
using ProductManagement.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Domain.Entities
{
    public sealed class User : BaseEntity
    {
        [MaxLength(50)]
        public string Username { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = new byte[0];
        public byte[] PasswordSalt { get; set; } = new byte[0];

        public UserRole Role { get; set; }

    }
}
