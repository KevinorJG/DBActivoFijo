using DepreciationDBApp.Domain.Entities;
using DepreciationDBApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DepreciationDBApp.Infrastructure.Repositories
{
    public class EFEmpleadoRepository : IEmpleadoRepository
    {
        private IDepreciacionDBContext depreciationDBContext;

        public EFEmpleadoRepository(IDepreciacionDBContext depreciationDBContext)
        {
            this.depreciationDBContext = depreciationDBContext;
        }

        public void Create(Empleado t)
        {
            try
            {
                ValidateEmployee(t);
                depreciationDBContext.Employeed.Add(t);
                depreciationDBContext.SaveChanges();
            }
            catch
            {
                throw;
            }
            
        }

        private void ValidateEmployee(Empleado t)
        {
            if (t == null)
            {
                throw new ArgumentException("El objeto employee no puede ser nulo");

            }
            if (string.IsNullOrEmpty(t.Email))
            {
                throw new ArgumentException("El email no puede ser nulo o estar vacio");

            }
        }

        public bool Delete(Empleado t)
        {
            try
            {
                if (t == null)
                {
                    throw new ArgumentNullException("El objeto Activo no puede ser null.");
                }
                Empleado employee = FindByDni(t.Dni);
                if (employee == null)
                {

                    throw new Exception($"El objeto con id{t.Dni} no existe.");
                }

                depreciationDBContext.Employeed.Remove(t);
                int result = depreciationDBContext.SaveChanges();

                return result > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Empleado FindByDni(string dni)
        {
            try
            {
                if (dni.Length <= 0)
                {
                    throw new Exception($"El dni {dni} no puede ser igual a cero.");
                }
                return depreciationDBContext.Employeed.FirstOrDefault(e => e.Dni == dni);
            }
            catch
            {
                throw;
            }
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
            return depreciationDBContext.Employeed.ToList();
        }

        public bool SetAssetsToEmployee(Empleado empleado, List<ActivoFijo> activos)
        {
            throw new NotImplementedException();
        }

        public bool SetAssetToEmployee(Empleado empleado, ActivoFijo activo)
        {
            throw new NotImplementedException();
        }

        public int Update(Empleado t)
        {
            try
            {
                if (t == null)
                {
                    throw new ArgumentNullException("El objeto no puede ser null.");
                }

                Empleado employee = FindByDni(t.Dni);
                if (employee == null)
                {
                    throw new Exception($"El objeto Empleado con {t.Dni}no puede ser null.");
                }

                employee.Nombre = t.Nombre;
                employee.Apellido = t.Apellido;
                employee.Direccion = t.Direccion;
                employee.Email = t.Email;               
                employee.Status = t.Status;
                employee.Telefono = t.Telefono;
                
                depreciationDBContext.Employeed.Update(employee);
                return depreciationDBContext.SaveChanges();
            }
            catch
            {
                throw new ArgumentNullException("El objeto no puede ser null.");
            }
        }
    }
}
