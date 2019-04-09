using System;
using System.Collections.Generic;
using System.Text;
using Data.Entities;
using Data.RepositoryInterfaces;
using Persistence;

namespace Repositories.Repositories
{
   public class CommandRepository :BaseRepository<Data.Entities.Command>,ICommandRepository
   {
       private readonly Context _context;
        public CommandRepository(Context context) : base(context)
        {
            _context = context;
        }
    }
}
