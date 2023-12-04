namespace ProductManagement.Domain.Dtos.CRUD
{
    public class CategoryWriteDto
    {
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }

    }
}
