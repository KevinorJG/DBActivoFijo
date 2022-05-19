using System.Collections.Generic;

namespace DepreciationDBApp.Applications.Interfaces
{
    public interface IService<T>
    {
        void Create(T t);
        int Update(T t);
        bool Delete(T t);
        List<T> GetAll();
    }
}
