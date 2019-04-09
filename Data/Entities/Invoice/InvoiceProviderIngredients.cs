using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Entities.Invoice
{
    public class InvoiceProviderIngredients
    {
        [Key]
        public Guid Id { get; set; }

        public Guid IdProvider { get; set; }
        public virtual InvoiceProvider Provider { get; set; }

        public Guid IdIngredient { get; set; }
        public virtual InvoiceProviderIngredient Ingredient { get; set; }
    }
}
