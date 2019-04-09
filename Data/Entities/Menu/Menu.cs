using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Entities.Menu
{
  public class Menu
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; }
        
        [Required]
        [Range(0,double.MaxValue)]
        public  double Price { get; set; }

        public virtual List<MenuIngredients> MenuIngredients { get; set; }
    }
}
