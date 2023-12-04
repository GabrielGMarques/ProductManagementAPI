using ProductManagement.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.Domain.Entities
{
    public sealed class Product : BaseEntity
    {
        [Required]
        [MaxLength(300)]
        public string Description { get; set; } = string.Empty;
        
        [MaxLength(50)]
        public string? ProductCode { get; set; }
        
        [MaxLength(50)]
        public string? ProductReference { get; set; }
        
        public int Stock { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(12,4)")]
        public decimal Width { get; set; }

        [Column(TypeName = "decimal(12,4)")]
        public decimal Height { get; set; }
        
        [Column(TypeName = "decimal(12,4)")]
        public decimal Weight { get; set; }
        
        public bool IsActive { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }

    }
}