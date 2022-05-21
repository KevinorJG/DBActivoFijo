using DepreciationDBApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepreciationDBApp.Applications.Interfaces
{
    public interface IAssetEmployeeServices : IService<ActivoEmpleado>
    {
        List<ActivoEmpleado> FindByAssetId(int id);
        List<ActivoEmpleado> FindByEmployeeId(int id);
        ActivoEmpleado FindById(int id);
    }
}
