using System;
using System.Collections.Generic;
using System.Text;
using Data.Entities;
using Repositories.Dtos;

namespace Repositories.ServicesInterfaces
{
    public interface IProviderService
    {
        Provider Create(ProviderDto provider);

        void Update(Provider dbProvider, ProviderDto provider);

        void Delete(Provider provider);

        List<Provider> GetAll();

        List<ProviderDto> GetAllAsDtos();

        Provider GetById(Guid id);

        ProviderDto GetByIdDto(Guid id);

        Provider Transform(ProviderDto providerDto);

        ProviderDto Transform(Provider provider);

    }
}
