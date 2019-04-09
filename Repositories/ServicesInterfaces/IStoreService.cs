using System;
using System.Collections.Generic;
using System.Text;
using Data.Entities;
using Repositories.Dtos;

namespace Repositories.ServicesInterfaces
{
    public interface IStoreService
    {
        StoreIngredient Create(StoreIngredientDto store);

        void Update(StoreIngredient dbStore, StoreIngredient store);

        void Delete(StoreIngredient ingredient);

        List<StoreIngredient> GetAll();

        List<StoreIngredientDto> GetAllAsDtos();

        StoreIngredient GetById(Guid id);

        StoreIngredientDto GetByIdDto(Guid id);
    }
}
