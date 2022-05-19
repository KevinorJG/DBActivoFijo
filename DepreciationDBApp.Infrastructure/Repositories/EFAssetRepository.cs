using DepreciationDBApp.Domain.Entities;
using DepreciationDBApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DepreciationDBApp.Infrastructure.Repositories
{
    public class EFAssetRepository : IAssetRepository
    {
        public IDepreciacionDBContext depreciationDbContext;

        public EFAssetRepository(IDepreciacionDBContext depreciationDbContext)
        {
            this.depreciationDbContext = depreciationDbContext;
        }
        public void Create(ActivoFijo t)
        {
            try
            {
                depreciationDbContext.Assets.Add(t);
                depreciationDbContext.SaveChanges();
            }
            catch
            {

            }

        }

        public bool Delete(ActivoFijo t)
        {
            try
            {
                if (t == null)
                {
                    throw new ArgumentNullException("El objeto Activo no puede ser null.");
                }
                ActivoFijo activo = FindById(t.Id);
                if (activo == null)
                {

                    throw new Exception($"El objeto con id{t.Id} no existe.");
                }

                depreciationDbContext.Assets.Remove(t);
                int result = depreciationDbContext.SaveChanges();

                return result > 0;
            }
            catch (Exception)
            {
                throw;
            }


        }

        public ActivoFijo FindByCode(string code)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(code))
                {
                    throw new Exception($"El parametro {code} no tiene el formato correcto.");
                }
                return depreciationDbContext.Assets.FirstOrDefault(e => e.Codigo.Equals(code));
            }
            catch
            {
                throw;
            }

        }

        public ActivoFijo FindById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new Exception($"El id {id} no puede ser igual a cero.");
                }
                return depreciationDbContext.Assets.FirstOrDefault(e => e.Id == id);
            }
            catch
            {
                throw;
            }

        }

        public List<ActivoFijo> FindByName(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    throw new Exception($"El parametro {name} no tiene el formato correcto.");
                }
                return depreciationDbContext.Assets.Where(e => e.Nombre.Equals(name, StringComparison.CurrentCultureIgnoreCase)).ToList();
            }
            catch
            {
                throw;
            }

        }

        public List<ActivoFijo> GetAll()
        {
            try
            {
                return depreciationDbContext.Assets.ToList();
            }
            catch
            {
                throw;
            }

        }

        public int Update(ActivoFijo t)
        {
            try
            {
                if (t == null)
                {
                    throw new ArgumentNullException("El objeto no puede ser null.");
                }

                ActivoFijo activo = FindById(t.Id);
                if (activo == null)
                {
                    throw new Exception($"El objeto Activo con {t.Id}no puede ser null.");
                }

                activo.Nombre = t.Nombre;
                activo.Descripcion = t.Descripcion;
                //activo.ValorActivo = t.ValorActivo;
                activo.ValorResidual = t.ValorResidual;
                // activo.VidaUtil = t.VidaUtil;
                activo.Estado = t.Estado;
                activo.EsActivo = t.EsActivo;
                //activo.Codigo = t.Codigo;

                depreciationDbContext.Assets.Update(activo);
                return depreciationDbContext.SaveChanges();
            }
            catch
            {
                throw new ArgumentNullException("El objeto no puede ser null.");
            }
        }
    }
}
