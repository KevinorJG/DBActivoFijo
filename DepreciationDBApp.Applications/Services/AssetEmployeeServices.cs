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
    public class AssetEmployeeServices : IAssetEmployeeServices
    {
        private IAssetEmployeeRepository assetEmployeeRepository;

        public AssetEmployeeServices(IAssetEmployeeRepository assetEmployeeRepository)
        {
            this.assetEmployeeRepository =  assetEmployeeRepository;
        }

        public void Create(ActivoEmpleado t)
        {
            assetEmployeeRepository.Create(t);
        }

        public bool Delete(ActivoEmpleado t)
        {
            throw new NotImplementedException();
        }

        public List<ActivoEmpleado> FindByAssetId(int id)
        {
            throw new NotImplementedException();
        }

        public List<ActivoEmpleado> FindByEmployeeId(int id)
        {
            throw new NotImplementedException();
        }

        public ActivoEmpleado FindById(int id)
        {
            throw new NotImplementedException();
        }

        public List<ActivoEmpleado> GetAll()
        {
           return assetEmployeeRepository.GetAll();
                 
        }

        public int Update(ActivoEmpleado t)
        {
            throw new NotImplementedException();
        }
    }
}
