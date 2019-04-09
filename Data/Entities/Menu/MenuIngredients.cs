using System;
using System.ComponentModel.DataAnnotations;
using Repositories.Dtos;

namespace Data.Entities.Menu
{
   public class MenuIngredients
   {
       [Key]
       public Guid Id { get; set; }

       public  Guid IdMenu { get; set; }
       public virtual Menu Menu { get; set; }

       public  Guid IdIngredient { get; set; }
       public virtual MenuIngredient Ingredient { get; set; }
   }
}
