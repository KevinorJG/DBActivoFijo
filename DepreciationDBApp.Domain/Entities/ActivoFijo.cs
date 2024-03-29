﻿using System.Collections.Generic;

#nullable disable

namespace DepreciationDBApp.Domain.Entities
{
    public partial class ActivoFijo
    {

        public ActivoFijo()
        {
            ActivoEmpleados = new HashSet<ActivoEmpleado>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal ValorActivo { get; set; }
        public decimal ValorResidual { get; set; }
        public int VidaUtil { get; set; }
        public string Codigo { get; set; }
        public string Estado { get; set; }
        public bool? EsActivo { get; set; }
        public virtual ICollection<ActivoEmpleado> ActivoEmpleados { get; set; }
    }
}
