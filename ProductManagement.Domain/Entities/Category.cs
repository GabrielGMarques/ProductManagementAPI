using System.ComponentModel.DataAnnotations;
using ProductManagement.Domain.Entities.Base;

namespace ProductManagement.Domain.Entities
{
    public sealed class Category : BaseEntity
    {
        [Required]
        [MaxLength(300)]
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public List<Product>? Products { get; set; }

    }
}
