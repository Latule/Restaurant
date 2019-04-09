using System;
using System.Collections.Generic;
using System.Text;
using Data.Entities;

namespace Data.RepositoryInterfaces
{
    public interface IProviderIngredientsRepository : IBaseRepository<ProviderIngredients>
    {
        void Delete(Guid idProvider);
        void Delete(Provider provider);
    }
}
