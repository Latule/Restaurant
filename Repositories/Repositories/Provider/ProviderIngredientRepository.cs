using System.Linq;
using Data.Entities;
using Data.RepositoryInterfaces;
using Persistence;

namespace Repositories.Repositories
{
   public class ProviderIngredientRepository:BaseRepository<ProviderIngredient>,IProviderIngredientRepository
   {
       private readonly Context _context;
        public ProviderIngredientRepository(Context context) : base(context)
        {
            _context = context;
        }


        public void Delete(Provider provider)
        {
            foreach (var providerIngredientForDelete in _context.Set<ProviderIngredients>().Where(id => id.Provider==provider))
            {
                _context.Set<ProviderIngredient>().RemoveRange(_context.Set<ProviderIngredient>().Where(id => id.Id==providerIngredientForDelete.IdIngredient));
            }
            

            _context.SaveChanges();
        }
   }
}
