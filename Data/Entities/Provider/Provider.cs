using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text;

namespace Data.Entities
{
   public class Provider
   {
       
       [Key]
       public Guid Id { get; set; }

       [Required]
       [StringLength(500)]
       public string Name { get; set; }

       public virtual List<ProviderIngredients> ProviderIngredients { get; set; }


    }
}
