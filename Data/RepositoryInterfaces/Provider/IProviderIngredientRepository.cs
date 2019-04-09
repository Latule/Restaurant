using System;
using System.Collections.Generic;
using System.Text;
using Data.Entities;

namespace Data.RepositoryInterfaces
{
  public  interface IProviderIngredientRepository:IBaseRepository<ProviderIngredient>
  {
     void Delete(Provider provider);
  }
}
