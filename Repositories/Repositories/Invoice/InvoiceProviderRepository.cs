using System;
using System.Collections.Generic;
using System.Text;
using Data.Entities.Invoice;
using Data.RepositoryInterfaces.Invoice;
using Persistence;

namespace Repositories.Repositories.Invoice
{
    public class InvoiceProviderRepository: BaseRepository<InvoiceProvider>,IInvoiceProviderRepository
    {
        private readonly Context _context;
        public InvoiceProviderRepository(Context context) : base(context)
        {
            _context = context;
        }
    }
}
