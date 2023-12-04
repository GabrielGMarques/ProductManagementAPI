namespace ProductManagement.Domain.Shared.Dtos
{
    public class CategoryWriteDto
    {
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }

    }
}
