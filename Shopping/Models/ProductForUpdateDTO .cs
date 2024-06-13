using System.ComponentModel.DataAnnotations;

namespace Shopping.Models
{
    public class ProductForUpdateDTO
    {
        [Required (ErrorMessage ="Name is required for update")]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }
    }
}