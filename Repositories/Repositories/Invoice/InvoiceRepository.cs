using System;
using System.Collections.Generic;
using System.Text;
using Data.Entities;
using Data.Entities.Invoice;
using Data.RepositoryInterfaces;
using Persistence;

namespace Repositories.Repositories
{
   public class InvoiceRepository :BaseRepository<Data.Entities.Invoice.Invoice>,IInvoiceRepository

   {
       private readonly Context _context;
        public InvoiceRepository(Context context) : base(context)
        {
            _context = context;
        }
    }
}
