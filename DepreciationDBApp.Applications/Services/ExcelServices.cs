using DepreciationDBApp.Applications.Interfaces;
using DepreciationDBApp.Domain.Entities;
using DepreciationDBApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepreciationDBApp.Applications.Services
{
    public class ExcelServices : IExcelServices
    {
        private IExcelRepository excelRepository;

        public ExcelServices(IExcelRepository excelRepository)
        {
            this.excelRepository = excelRepository;
        }

        public void Create(ActivoEmpleado t)
        {
            excelRepository.Create(t);
        }

        public void CreateExcel(ActivoEmpleado activoEmpleado)
        {
            excelRepository.CreateExcel(activoEmpleado);
        }

        public bool Delete(ActivoEmpleado t)
        {
            throw new NotImplementedException();
        }

        public List<ActivoEmpleado> GetAll()
        {
            throw new NotImplementedException();
        }

        public int Update(ActivoEmpleado t)
        {
            throw new NotImplementedException();
        }
    }
}
