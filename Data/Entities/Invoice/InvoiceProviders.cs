using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities.Invoice
{
    public class InvoiceProviders
    {
        [Key]
        public Guid Id { get; set; }

        public Guid IdInvoice { get; set; }
        public virtual Invoice Invoice { get; set; }

        public Guid IdProvider { get; set; }
        public virtual InvoiceProvider Provider { get; set; }


    }
}
