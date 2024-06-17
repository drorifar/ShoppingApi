using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopping.Entities
{
    public class Product
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
            
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }

        //[ForeignKey("CategoryID")] // we dont need to add it because E.FW doing it automaticlly
        public Category? Category { get; set; }

        public int CategoryID { get; set; }

        public float Price { get; set; }
    }

}
