using DepreciationDBApp.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;

namespace DepreciationDBApp.Domain.Interfaces
{
    public interface IEmpleadoRepository : IRepository<Empleado>
    {
        Empleado FindByDni(string dni);
        Empleado FindByEmail(string email);
        IEnumerable<Empleado> FindByLastNames(string lastnames);
        bool SetAssetToEmployee(Empleado empleado, ActivoFijo activo);
        bool SetAssetsToEmployee(Empleado empleado, List<ActivoFijo> activos);
        IDbContextTransaction GetTransaction();
    }
}
