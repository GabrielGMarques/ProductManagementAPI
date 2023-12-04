namespace ProductManagement.Domain.Shared.Dtos
{
    public class CategoryReadDto
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }

    }
}
