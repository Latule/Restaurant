using System;
using System.Collections.Generic;
using System.Text;
using Data.Entities;
using Data.Entities.Menu;
using Repositories.Dtos;

namespace Repositories.ServicesInterfaces
{
    public interface IMenuService
    {
        Menu Create(MenuDto menu);

        List<Menu> GetAll();

        void Update(Menu dbMenu, MenuDto menu);

        void Delete(Menu menu);

        List<MenuDto> GetAllAsDtos();

        Menu GetById(Guid id);

        MenuDto GetByIdDto(Guid id);
        Menu Transform(MenuDto menuDto);
        MenuDto Transform(Menu menu);
    }
}
