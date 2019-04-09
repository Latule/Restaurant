using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Repositories.Dtos
{
    public class StoreIngredient
    {

        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
