using DepreciationDBApp.Domain.Entities;
using DepreciationDBApp.Domain.Enums;
using DepreciationDBApp.Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
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
            try
            {
                List<Empleado> employees = GetAll();

                return (List<Empleado>)employees.Where(x => x.Apellido.Equals(lastnames, StringComparison.CurrentCultureIgnoreCase)).ToList();
            }
            catch
            {
                throw;
            }
        }

        public Empleado FindByEmail(string email)
        {
            try
            {
                List<Empleado> employees = GetAll();
                return employees.First(x => x.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase));
            }
            catch
            {
                throw;
            }
        }

        public List<Empleado> GetAll()
        {
            return depreciationDBContext.Employeed.ToList();
        }

        public bool SetAssetsToEmployee(Empleado empleado, List<ActivoFijo> activos)
        {
            if (empleado == null && activos == null)
            {
                throw new ArgumentException("Los objetos no pueden ser nulos");
            }
            Empleado Valideemployee = FindByDni(empleado.Dni);

            if (Valideemployee == null)
            {
                throw new ArgumentException($"El objeto con {empleado.Dni} no se encuentra en la base de datos.");
            }
            foreach (ActivoFijo asset in activos)
            {
                if (asset == null)
                {
                    throw new ArgumentException("El asset no puede ser nulo");
                }
                if (asset.Estado == AssetStatus.Asignado.ToString())
                {
                    throw new ArgumentException($"El asset {asset.Nombre} ya esta asignado");
                }
                if (asset.EsActivo == false)
                {
                    throw new ArgumentException($"El asset {asset.Nombre} no se encuentra activo");
                }
            }

            foreach (ActivoFijo asset in activos)
            {
                ActivoEmpleado assetEmployee = new ActivoEmpleado
                {
                    Activo = asset,
                    Empleado = empleado,
                    ActivoId = asset.Id,
                    IsActive = asset.EsActivo.Value,
                    Date = DateTime.Now,
                    EmpleadoId = empleado.Id,

                };
                depreciationDBContext.AssetEmployeed.Add(assetEmployee);
            }
            return true;
        }

        public bool SetAssetToEmployee(Empleado empleado, ActivoFijo activo)
        {
            if (empleado == null && activo == null)
            {
                throw new ArgumentException("Los objetos no pueden ser nulos");
            }
            Empleado valideEmployee = FindByDni(empleado.Dni);
            if (valideEmployee == null)
            {
                throw new ArgumentException($"El empleado {empleado.Nombre} no se encuentra en la base de datos");
            }
            if (activo.Estado == AssetStatus.Asignado.ToString())
            {
                throw new ArgumentException($"El asset {activo.Nombre} se encuentra ya asignado");
            }
            if (activo.EsActivo == false)
            {
                throw new ArgumentException($"El asset {activo.Nombre} no se encuentra activo");
            }
            ActivoEmpleado assetEmployee = new ActivoEmpleado
            {
                Activo = activo,
                ActivoId = activo.Id,
                Date = DateTime.Now,
                IsActive = true,
                Empleado = empleado,
                EmpleadoId = empleado.Id,
            };
            depreciationDBContext.AssetEmployeed.Add(assetEmployee);
            depreciationDBContext.SaveChanges();
            return true;
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

        public IDbContextTransaction GetTransaction()
        {
            return ((DepreciacionDBContext)depreciationDBContext).Database.BeginTransaction();
        }
    }
}
