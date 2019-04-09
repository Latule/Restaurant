using System;
using System.Collections.Generic;
using System.Text;

namespace Data.RepositoryInterfaces
{
   public interface IBaseRepository<T> 
    {
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        List<T> GetAll();
        T GetById(Guid id);
    }
}
