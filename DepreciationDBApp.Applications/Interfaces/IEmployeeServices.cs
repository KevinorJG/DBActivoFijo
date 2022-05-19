using DepreciationDBApp.Domain.Entities;
using System.Collections.Generic;

namespace DepreciationDBApp.Applications.Interfaces
{
    public interface IEmployeeServices : IService<Empleado>
    {
        Empleado FindByDni(string dni);
        Empleado FindByName(string email);
        IEnumerable<Empleado> FindByLastNames(string lastnames);
        bool SetAssetToEmployee(Empleado employee, ActivoFijo asset);
        bool SetAssetsToEmployee(Empleado employee, List<ActivoFijo> assets);
    }
}

