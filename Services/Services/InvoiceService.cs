using System;
using System.Collections.Generic;
using System.Text;
using Data.Entities.Invoice;
using Data.RepositoryInterfaces;
using Repositories.Repositories;
using Repositories.ServicesInterfaces;

namespace Services.Services
{
    public class InvoiceService : IInvoiceService
    {

        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceService(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public Invoice Create(Invoice invoice)
        {
            _invoiceRepository.Create(invoice);
            return invoice;
        }

        public List<Invoice> GetAll()
        {
           return _invoiceRepository.GetAll();
        }
    }
}
