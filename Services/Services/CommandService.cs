using System;
using System.Collections.Generic;
using Data.Entities;
using Data.RepositoryInterfaces;
using Data.RepositoryInterfaces.Command;
using Repositories.Dtos;
using Repositories.ServicesInterfaces;

namespace Services.Services
{
    public class CommandService:ICommandService
    {
        private readonly ICommandRepository _commandRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly IMenuService _menuService;

        public CommandService(ICommandRepository commandRepository, IMenuRepository menuRepository, IMenuService menuService)
        {
            _commandRepository = commandRepository;
            _menuRepository = menuRepository;
            _menuService = menuService;
        }

        public Command Create(CommandDto command)
        {
            if (command==null)
                return null;

            var com = Transform(command);

            if (com.CommandMenus.Count==0)
            {
                return null;
            }
            _commandRepository.Create(com);
            return com;
        }

        public List<Command> GetAll()
        {
            return _commandRepository.GetAll();
        }

        public List<CommandDto> GetAllAsDtos()
        {
            var enumerable = new List<CommandDto>();
            foreach (var command in GetAll())
            {
                enumerable.Add(Transform(command));
            }

            return enumerable;
        }

        public Command GetById(Guid id)
        {
            return _commandRepository.GetById(id);
        }

        public CommandDto GetByIdDto(Guid id)
        {
           var com=  _commandRepository.GetById(id);
           if (com == null)
               return null;
           return Transform(com);
        }

        public CommandDto Transform(Command command)
        {
            var commandDto = new CommandDto()
            {
                Date = command.Date,
                Price = command.Price,
                CommandMenus = new List<string>()
            };

            foreach (var menu in command.CommandMenus)
            {
                commandDto.CommandMenus.Add(menu.Menu.Name);
            }


            return commandDto;
        }


        public Command Transform(CommandDto commandDto)
        {
            var command = new Command()
                {CommandMenus = new List<CommandMenus>(), Date = DateTime.Now, Id = Guid.NewGuid(), Price = 0};

            foreach (var menu in commandDto.CommandMenus)
            {
                var menus = _menuRepository.GetAll().Find(P => P.Name.Equals(menu));
                if (menus != null)
                {
                    if (_menuService.Transform(menus).Coockable)
                    {
                        var commandMenus = new CommandMenus()
                        {
                            Command = command,
                            Id = Guid.NewGuid(),
                            IdCommand = command.Id,
                            IdMenu = menus.Id,
                            Menu = menus
                        };
                        command.CommandMenus.Add(commandMenus);
                        command.Price += menus.Price;
                    }
                }
            }

            return command;
        }

    }
}
