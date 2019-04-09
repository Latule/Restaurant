using System;
using System.Collections.Generic;
using System.Text;
using Data.Entities;
using Data.Entities.Menu;

namespace Data.RepositoryInterfaces
{
    public interface IMenuIngredientsRepository: IBaseRepository<MenuIngredients>
    {
        void Delete(Entities.Menu.Menu menu);
    }
}
