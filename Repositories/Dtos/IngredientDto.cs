using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Repositories.Dtos
{
    public class IngredientDto
    {
        [Key]
        [StringLength(500)]
        public string Name { get; set; }
        [Required]
        [Range(0,int.MaxValue)]
        public int Quantity { get; set; }
        [Required]
        [Range(0,double.MaxValue)]
        public double Price { get; set; }
    }
}
