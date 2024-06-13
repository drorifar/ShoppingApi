﻿using System.ComponentModel.DataAnnotations;

namespace Shopping.Models
{
    public class ProductForCreationDTO
    {
        [Required(ErrorMessage = "Name is required for creation")]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }
    }
}