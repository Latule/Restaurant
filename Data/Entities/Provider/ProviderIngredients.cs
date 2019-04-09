using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
   public class ProviderIngredients
    {
        [Key]
        public Guid Id { get; set; }

        public Guid IdProvider { get; set; }
        public virtual Provider Provider { get; set; }

        public Guid IdIngredient { get; set; }
        public virtual ProviderIngredient Ingredient { get; set; }

    }
}
