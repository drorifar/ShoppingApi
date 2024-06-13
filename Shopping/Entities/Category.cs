using System.ComponentModel.DataAnnotations;

namespace Shopping.Entities
{
    public class Category
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        public List<Product> Products { get; set; }
    }

}
