using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Entities;
using Data.RepositoryInterfaces;
using Repositories.Dtos;
using Repositories.ServicesInterfaces;

namespace Services.Services
{
    public class ProviderService:IProviderService
    {

        private readonly IProviderRepository _providerRepository;
        private readonly IProviderIngredientsRepository _providerIngredientsRepository;
        private readonly IProviderIngredientRepository _providerIngredientRepository;

        public ProviderService(IProviderRepository providerRepository, IProviderIngredientsRepository providerIngredientsRepository, IProviderIngredientRepository ingredientRepository)
        {
            _providerRepository = providerRepository;
            _providerIngredientsRepository = providerIngredientsRepository;
            _providerIngredientRepository = ingredientRepository;
        }

        public Provider Create(ProviderDto providerDto)
        {
            var provider = Transform(providerDto);

            _providerRepository.Create(provider);

            return provider;
        }

        public void Update(Provider dbProvider, ProviderDto providerDto)
        {

            dbProvider.Name = providerDto.Name;


            foreach (var ingredient in providerDto.Ingredients)
            {
                var findIngredient= dbProvider.ProviderIngredients.Find(condition => condition.Ingredient.Name.Equals(ingredient.Name));


                if (findIngredient == null)
                {
                    var idIngredientGenerated = Guid.NewGuid();

                    findIngredient = new ProviderIngredients()
                    {

                        Id = Guid.NewGuid(),
                        Ingredient = new ProviderIngredient()
                        {
                            Name = ingredient.Name,
                            Price = ingredient.Price,
                            Quantity = ingredient.Quantity,
                            Id = idIngredientGenerated
                        },
                        Provider = dbProvider,
                        IdProvider = dbProvider.Id,
                        IdIngredient = idIngredientGenerated

                    };
                    dbProvider.ProviderIngredients.Add(findIngredient);
                }
                else
                {
                    findIngredient.Ingredient.Name = ingredient.Name;
                    findIngredient.Ingredient.Price = ingredient.Price;
                    findIngredient.Ingredient.Quantity = ingredient.Quantity;
                }
                
            }

            _providerRepository.Update(dbProvider);
                    
        }

        public void Delete(Provider provider)
        {
            _providerIngredientRepository.Delete(provider);
            _providerIngredientsRepository.Delete(provider);
            _providerRepository.Delete(provider);
        
        }

        public List<Provider> GetAll()
        {
            return _providerRepository.GetAll();
        }

        public List<ProviderDto> GetAllAsDtos()
        {
            var enumerable = new List<ProviderDto>();
            foreach (var provider in GetAll())
            {
                enumerable.Add(Transform(provider));
            }

            return enumerable;
        }

        public Provider GetById(Guid id)
        {
            return _providerRepository.GetById(id);
        }

        public ProviderDto GetByIdDto(Guid id)
        {
            var provider = _providerRepository.GetById(id);
            if (provider == null)
                return null;

            return Transform(provider);
        }


        public ProviderDto Transform(Provider provider)
        {
            ProviderDto providerDto = new ProviderDto {Name = provider.Name, Ingredients = new List<IngredientDto>()};

            foreach (var ingredient in provider.ProviderIngredients)
            {
                providerDto.Ingredients.Add(new IngredientDto(){Name = ingredient.Ingredient.Name,Price = ingredient.Ingredient.Price,Quantity = ingredient.Ingredient.Quantity});
            }

            return providerDto;
        }

        public Provider Transform(ProviderDto providerDto)
        {
            var provider = new Provider()
            {
                Id = Guid.NewGuid(),
                Name = providerDto.Name,
                ProviderIngredients = new List<ProviderIngredients>()
            };

            foreach (var ingredient in providerDto.Ingredients)
            {
                var IdIngredientGenerated = Guid.NewGuid();

                var ProviderIngredient = new ProviderIngredients()
                {

                    Id = Guid.NewGuid(),
                    Ingredient = new ProviderIngredient()
                    {
                        Name = ingredient.Name,
                        Price = ingredient.Price,
                        Quantity = ingredient.Quantity,
                        Id = IdIngredientGenerated
                    },
                    Provider = provider,
                    IdProvider = provider.Id,
                    IdIngredient = IdIngredientGenerated

                };
                provider.ProviderIngredients.Add(ProviderIngredient);
            }

            return provider;
        }

    }
}
