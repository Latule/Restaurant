using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Entities.Invoice
{
    public class InvoiceProvider
    {
        [Key]
        public  Guid Id { get; set; }
        [Required]
        public Guid IdProvider { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        public virtual List<InvoiceProviderIngredients> ProviderIngredients { get; set; }
    }
}
