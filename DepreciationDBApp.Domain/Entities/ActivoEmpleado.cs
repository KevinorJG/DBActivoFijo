using System;

#nullable disable

namespace DepreciationDBApp.Domain.Entities
{
    public partial class ActivoEmpleado
    {
        public int Id { get; set; }
        public int ActivoId { get; set; }
        public int EmpleadoId { get; set; }
        public DateTime Date { get; set; }
        public bool IsActive { get; set; }

        public virtual ActivoFijo Activo { get; set; }
        public virtual Empleado Empleado { get; set; }
    }
}
