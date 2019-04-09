using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Data.Entities;
using Data.RepositoryInterfaces;
using Persistence;
using Repositories.Dtos;
using Repositories.Repositories;
using Repositories.ServicesInterfaces;

namespace Services.Services
{
    public class StoreService :IStoreService
    {

        private readonly IStoreRepository _storeRepository;

        public StoreService(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }


        public StoreIngredient Create(StoreIngredientDto store)
        {
            var creat = Transform(store);
            _storeRepository.Create(creat);
            return creat;
        }

        public void Update(StoreIngredient dbStore, StoreIngredient store)
        {
            dbStore.Name = store.Name;
 
            dbStore.Quantity = store.Quantity;
            _storeRepository.Update(dbStore);
        }

        public void Delete(StoreIngredient ingredient)
        {
            _storeRepository.Delete(ingredient);
        }

        List<StoreIngredient> IStoreService.GetAll()
        {
            return _storeRepository.GetAll();
        }

        List<StoreIngredientDto> IStoreService.GetAllAsDtos()
        {

                var enumerable = new List<StoreIngredientDto>();
                foreach (var store in _storeRepository.GetAll())
                {
                    enumerable.Add(Transform(store));
                }
            
                return enumerable;
            
        }

        StoreIngredient IStoreService.GetById(Guid id)
        {
            return _storeRepository.GetById(id);
        }

        StoreIngredientDto IStoreService.GetByIdDto(Guid id)
        {

            var store = _storeRepository.GetById(id);
            if (store == null)
                return null;
            return Transform(store);
        }


        public StoreIngredient Transform(StoreIngredientDto ingredientDto)
        {
            return new StoreIngredient()
            {
                Id = Guid.NewGuid(),
                Name = ingredientDto.Name,
                Quantity = ingredientDto.Quantity
            };
        }

        public StoreIngredientDto Transform(StoreIngredient ingredient)
        {
            return new StoreIngredientDto()
            {
                Name = ingredient.Name,
                Quantity = ingredient.Quantity
            };
        }

    }
}
