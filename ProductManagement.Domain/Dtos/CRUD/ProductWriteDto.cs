using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.Domain.Dtos.CRUD
{
    public class ProductWriteDto
    {
        [Required]
        [MaxLength(300)]
        public string Description { get; set; } = string.Empty;
        [MaxLength(50)]
        public string? ProductCode { get; set; }
        [MaxLength(50)]
        public string? ProductReference { get; set; }
        [Required]
        public int Stock { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}