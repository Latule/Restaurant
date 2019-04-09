using System;
using System.Collections.Generic;
using System.Text;
using Data.Entities;
using Data.RepositoryInterfaces.Command;
using Persistence;

namespace Repositories.Repositories.Command
{
    public class CommandMenusRepository:BaseRepository<CommandMenus>,ICommandMenusRepository
    {
        private readonly Context _context;
        public CommandMenusRepository(Context context) : base(context)
        {
            _context = context;
        }
    }
}
