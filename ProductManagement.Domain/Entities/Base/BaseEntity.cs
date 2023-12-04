using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Domain.Entities.Base
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
