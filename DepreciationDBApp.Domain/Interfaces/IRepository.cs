using System.Collections.Generic;

namespace DepreciationDBApp.Domain.Interfaces
{
    public interface IRepository<T>
    {
        void Create(T t);
        int Update(T t);
        bool Delete(T t);
        List<T> GetAll();

    }
}
