using System;
using System.Collections.Generic;
using System.Text;
using Data.Entities;
using Data.RepositoryInterfaces;
using Persistence;

namespace Repositories.Repositories
{
    public class ProviderRepository:BaseRepository<Provider>,IProviderRepository
    {
        private readonly Context _context;
        public ProviderRepository(Context context) : base(context)
        {
            _context = context;
        }
    }
}
