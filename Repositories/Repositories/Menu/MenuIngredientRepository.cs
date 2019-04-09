using System;
using System.Collections.Generic;
using System.Text;
using Data.Entities;
using Data.Entities.Menu;
using Data.RepositoryInterfaces.Menu;
using Persistence;

namespace Repositories.Repositories.Menu
{
    public class MenuIngredientRepository:BaseRepository<MenuIngredient>,IMenuIngredientRepository
    {
        private readonly Context _context;
        public MenuIngredientRepository(Context context) : base(context)
        {
            _context = context;
        }
    }
}
