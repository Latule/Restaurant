using System;
using System.Collections.Generic;
using System.Text;
using Data.Entities;
using Data.RepositoryInterfaces;
using Persistence;
using Repositories.Dtos;

namespace Repositories.Repositories
{
    public class StoreRepository:BaseRepository<StoreIngredient>,  IStoreRepository
    {

        public readonly Context Context;

        public StoreRepository(Context context) : base(context)
        {
            Context = context;
        }
    }
}
