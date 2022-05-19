using System.Collections.Generic;

#nullable disable

namespace DepreciationDBApp.Domain.Entities
{
    public partial class Empleado
    {
        public Empleado()
        {
            ActivoEmpleados = new HashSet<ActivoEmpleado>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Dni { get; set; }
        public string Status { get; set; }

        public virtual ICollection<ActivoEmpleado> ActivoEmpleados { get; set; }
    }
}
