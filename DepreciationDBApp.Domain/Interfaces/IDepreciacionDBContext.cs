using DepreciationDBApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace DepreciationDBApp.Domain.Interfaces
{
    public interface IDepreciacionDBContext
    {
        public DbSet<ActivoFijo> Assets { get; set; }
        public DbSet<Empleado> Employeed { get; set; }
        public DbSet<ActivoEmpleado> AssetEmployeed { get; set; }
        public int SaveChanges();
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
