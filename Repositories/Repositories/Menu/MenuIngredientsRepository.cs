using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Entities;
using Data.Entities.Menu;
using Data.RepositoryInterfaces;
using Persistence;

namespace Repositories.Repositories
{
    public class MenuIngredientsRepository:BaseRepository<MenuIngredients>,IMenuIngredientsRepository
    {
        public readonly Context Context;
        public MenuIngredientsRepository(Context context) : base(context)
        {
            Context = context;
        }

        public void Delete(Data.Entities.Menu.Menu menu)
        {
            Context.Set<MenuIngredients>().RemoveRange(Context.Set<MenuIngredients>().Where(id => id.Menu == menu));

            Context.SaveChanges();
        }
    }
}
