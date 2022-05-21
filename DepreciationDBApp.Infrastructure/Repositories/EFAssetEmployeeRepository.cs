using DepreciationDBApp.Domain.Entities;
using DepreciationDBApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepreciationDBApp.Infrastructure.Repositories
{
    public class EFAssetEmployeeRepository : IAssetEmployeeRepository
    {
        private IDepreciacionDBContext depreciacionDBContext;

        public EFAssetEmployeeRepository(IDepreciacionDBContext depreciacionDBContext)
        {
            this.depreciacionDBContext = depreciacionDBContext;
        }

        public void Create(ActivoEmpleado t)
        {
            try
            {
                if (t == null)
                {
                    throw new ArgumentNullException("El objeto no puede ser nulo");
                }
                depreciacionDBContext.AssetEmployeed.AddAsync(t);
                depreciacionDBContext.SaveChanges();
            }
            catch
            {
                throw;
            }
            
        }

        public bool Delete(ActivoEmpleado t)
        {
            try
            {
                if(t == null)
                {
                    throw new ArgumentNullException("El objeto no puede ser nulo");
                }
                ActivoEmpleado activoEmpleado = FindById(t.Id);
                depreciacionDBContext.AssetEmployeed.Remove(activoEmpleado);
                int result = depreciacionDBContext.SaveChanges();
                return result > 0;
            }
            catch
            {
                throw;
            }
        }

        public List<ActivoEmpleado> FindByAssetId(int id)
        {
            return depreciacionDBContext.AssetEmployeed.Where(x => x.Id == id).ToList();
        }

        public List<ActivoEmpleado> FindByEmployeeId(int id)
        {
            return depreciacionDBContext.AssetEmployeed.Where(x => x.Id == id).ToList();
        }

        public ActivoEmpleado FindById(int id)
        {
            return depreciacionDBContext.AssetEmployeed.FirstOrDefault(a => a.Id == id);
        }

        public List<ActivoEmpleado> GetAll()
        {
            try
            {
                return depreciacionDBContext.AssetEmployeed.ToList();
            }
            catch
            {
                throw;
            }
            
        }

        public int Update(ActivoEmpleado t)
        {
            throw new NotImplementedException();
        }
    }
}
