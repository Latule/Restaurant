using System;
using System.Collections.Generic;
using System.Text;
using Data.Entities;
using Data.Entities.Menu;
using Data.RepositoryInterfaces;
using Repositories.Dtos;
using Repositories.Repositories;
using Repositories.ServicesInterfaces;

namespace Services.Services
{
   public class MenuService :IMenuService
   {
       private readonly IMenuRepository _menuRepository;
       private readonly IMenuIngredientsRepository _menuIngredientsRepository;
       private readonly IStoreRepository _storeRepository;

       public MenuService(IMenuRepository menuRepository, IMenuIngredientsRepository menuIngredientsRepository, IStoreRepository storeRepository)
       {
           _menuRepository = menuRepository;
           _menuIngredientsRepository = menuIngredientsRepository;
           _storeRepository = storeRepository;
       }

       public Menu Create(MenuDto menuDto)
       {
           Menu menu = Transform(menuDto);
           _menuRepository.Create(menu);
           return menu;
       }

        public void Update(Menu dbMenu, MenuDto menuDto)
        {
            dbMenu.Name = menuDto.Name;
            dbMenu.Price = menuDto.Price;
            

            foreach (var ingredient in menuDto.Ingredients)
            {
                var findIngredient = dbMenu.MenuIngredients.Find(condition => condition.Ingredient.Name.Equals(ingredient.Name));


                if (findIngredient == null)
                {
                    var idIngredientGenerated = Guid.NewGuid();

                    findIngredient = new MenuIngredients
                    {
                        Id = Guid.NewGuid(),
                        Ingredient = new  MenuIngredient()
                        {
                            Name = ingredient.Name,
                            Quantity = ingredient.Quantity,
                            Id = idIngredientGenerated
                        },
                        Menu = dbMenu,


                    };
                    dbMenu.MenuIngredients.Add(findIngredient);
                   
                }
                else
                {
                    findIngredient.Ingredient.Name = ingredient.Name;
                    findIngredient.Ingredient.Quantity = ingredient.Quantity;
                }

            }
            _menuRepository.Update(dbMenu);
        }

        public void Delete(Menu menu)
        {
           _menuIngredientsRepository.Delete(menu);
           _menuRepository.Delete(menu);
        }

        public List<Menu> GetAll()
        {
            return _menuRepository.GetAll();
        }

        public List<MenuDto> GetAllAsDtos()
        {
            var enumerable = new List<MenuDto>();
            foreach (var menu in GetAll())
            {
                enumerable.Add(Transform(menu));
            }

            return enumerable;
        }

        public Menu GetById(Guid id)
        {
            return _menuRepository.GetById(id);
        }

        public MenuDto GetByIdDto(Guid id)
        {
            var menu = _menuRepository.GetById(id);
            if (menu == null)
                return null;    

            return Transform(menu);
        }


        public MenuDto Transform(Menu menu)
        {
            MenuDto menuDto = new MenuDto
            {
                Name = menu.Name,
                Ingredients = new List<StoreIngredientDto>(),
                Price = menu.Price,
                Coockable = true
            };

            foreach (var ingredient in menu.MenuIngredients)
            {
                var storeIngredientDto= new StoreIngredientDto()
                {
                    Name = ingredient.Ingredient.Name,
                    Quantity = ingredient.Ingredient.Quantity

                };

                menuDto.Ingredients.Add(storeIngredientDto);

                if (menuDto.Coockable)
                {
                    var store = _storeRepository.GetAll().Find(p => p.Name.Equals(storeIngredientDto.Name));
                    if (store == null)
                    {
                        menuDto.Coockable = false;
                    }
                    else
                    {
                        if (store.Quantity < storeIngredientDto.Quantity)
                            menuDto.Coockable = false;
                    }
                }
            }

            return menuDto;
        }

        public Menu Transform(MenuDto menuDto)
        {
            var menu = new Menu()
            {
                Id = Guid.NewGuid(),
                Name = menuDto.Name,
                MenuIngredients = new List<MenuIngredients>(),
                Price = menuDto.Price
            };

            foreach (var ingredient in menuDto.Ingredients)
            {
                var idIngredientGenerated = Guid.NewGuid();

                var menuIngredient = new MenuIngredients()
                {

                    Id = Guid.NewGuid(),
                    Ingredient = new MenuIngredient()
                    {
                        Name = ingredient.Name,
                        Quantity = ingredient.Quantity,
                        Id = idIngredientGenerated
                    },
                    Menu = menu,
                    IdIngredient = idIngredientGenerated,
                    IdMenu = menu.Id

                };
                menu.MenuIngredients.Add(menuIngredient);
            }

            return menu;
        }


    }
}
