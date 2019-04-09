using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities.Invoice
{
    public class Invoice
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public  DateTime Date { get; set; }


        [Required]
        public double Price { get; set; }

        public  virtual  List<InvoiceProviders> InvoiceProviders { get; set; }


    }
}
