using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Repositories.Dtos
{
    public class MenuDto
    {
        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double Price { get; set; }


        public List<StoreIngredientDto> Ingredients { get; set; }

        public bool Coockable { get; set; }

    }
}
