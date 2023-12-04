using System.ComponentModel.DataAnnotations;
namespace ProductManagement.Domain.Shared.Dtos
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
        public decimal Width { get; set; }
        [Required]
        public decimal Height { get; set; }
        [Required]
        public decimal Weight { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}