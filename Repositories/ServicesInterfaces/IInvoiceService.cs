using System;
using System.Collections.Generic;
using System.Text;
using Data.Entities.Invoice;

namespace Repositories.ServicesInterfaces
{
    public interface IInvoiceService
    {
        Invoice Create(Invoice invoice);

        List<Invoice> GetAll();

    }
}
