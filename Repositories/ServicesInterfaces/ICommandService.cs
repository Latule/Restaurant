using System;
using System.Collections.Generic;
using System.Text;
using Data.Entities;
using Repositories.Dtos;

namespace Repositories.ServicesInterfaces
{
   public interface ICommandService
    {
        Command Create(CommandDto command);

        List<Command> GetAll();

        List<CommandDto> GetAllAsDtos();

        Command GetById(Guid id);
        CommandDto GetByIdDto(Guid id);

        CommandDto Transform(Command command);

    }
}
