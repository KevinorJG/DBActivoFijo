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
    public class EmployeeServices : IEmployeeServices
    {
        private IEmpleadoRepository empleadoRepository;

        public EmployeeServices(IEmpleadoRepository empleadoRepository)
        {
            this.empleadoRepository = empleadoRepository;
        }

        public void Create(Empleado t)
        {
            empleadoRepository.Create(t);
                    
        }

        public bool Delete(Empleado t)
        {
            throw new NotImplementedException();
        }

        public Empleado FindByDni(string dni)
        {
            return empleadoRepository.FindByDni(dni);
        }

        public IEnumerable<Empleado> FindByLastNames(string lastnames)
        {
            throw new NotImplementedException();
        }

        public Empleado FindByName(string email)
        {
            throw new NotImplementedException();
        }

        public List<Empleado> GetAll()
        {
            return empleadoRepository.GetAll();
        }

        public bool SetAssetsToEmployee(Empleado employee, List<ActivoFijo> assets)
        {
            throw new NotImplementedException();
        }

        public bool SetAssetToEmployee(Empleado employee, ActivoFijo asset)
        {
            throw new NotImplementedException();
        }

        public int Update(Empleado t)
        {
            throw new NotImplementedException();
        }
    }
}
