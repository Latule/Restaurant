using System;
using System.Collections.Generic;
using System.Text;
using Data.Entities.Invoice;
using Data.RepositoryInterfaces.Invoice;
using Persistence;

namespace Repositories.Repositories.Invoice
{
 
        public class InvoiceProvidersRepository : BaseRepository<InvoiceProviders>,IInvoiceProvidersRepository
        {
            private readonly Context _context;
            public InvoiceProvidersRepository(Context context) : base(context)
            {
                _context = context;
            }
        }
    
}
