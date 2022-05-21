using DepreciationDBApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepreciationDBApp.Domain.Interfaces
{
    public interface IExcelRepository : IRepository<ActivoEmpleado>
    {

        void CreateExcel(ActivoEmpleado activoEmpleado);
    }
}
