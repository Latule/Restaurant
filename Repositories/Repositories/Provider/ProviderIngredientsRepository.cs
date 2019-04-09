using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Entities;
using Data.RepositoryInterfaces;
using Persistence;

namespace Repositories.Repositories
{
    public class ProviderIngredientsRepository : BaseRepository<ProviderIngredients> , IProviderIngredientsRepository
    {
        public readonly Context _Context;
        public ProviderIngredientsRepository(Context context) : base(context)
        {
            _Context = context;
        }

        
        public void Delete(Provider provider)
        {
            _Context.Set<ProviderIngredients>().RemoveRange(_Context.Set<ProviderIngredients>().Where(  id => id.Provider == provider));

            _Context.SaveChanges();
        }

        public void Delete(Guid idProvider)
        {
            _Context.Set<ProviderIngredients>().RemoveRange(_Context.Set<ProviderIngredients>().Where(id => id.IdProvider == idProvider));

            _Context.SaveChanges();
        }

        

    }
}
