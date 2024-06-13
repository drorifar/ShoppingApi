using System.ComponentModel.DataAnnotations;

namespace Shopping.Models
{
    public class ProductForCreationDTO
    {
        [Required]
        [MaxLength(10)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string? Description { get; set; }
    }
}