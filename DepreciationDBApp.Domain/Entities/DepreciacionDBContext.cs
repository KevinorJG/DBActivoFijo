using DepreciationDBApp.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DepreciationDBApp.Domain.Entities
{



    public partial class DepreciacionDBContext : DbContext, IDepreciacionDBContext

    {
        public DepreciacionDBContext()
        {
        }

        public DepreciacionDBContext(DbContextOptions<DepreciacionDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ActivoFijo> ActivoFijos { get; set; }

        public DbSet<ActivoFijo> Assets { get; set; }
        public DbSet<Empleado> Employeed { get; set; }
        public DbSet<ActivoEmpleado> AssetEmployeed { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source=JADPA28\\SQLSERVER2019;Initial Catalog=DepreciacionDB;user=sa;password=123456");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<ActivoEmpleado>(entity =>
            {
                entity.ToTable("ActivoEmpleado");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ActivoId).HasColumnName("activoID");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.EmpleadoId).HasColumnName("empleadoID");

                entity.HasOne(d => d.Activo)
                    .WithMany(p => p.ActivoEmpleados)
                    .HasForeignKey(d => d.ActivoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Activo");

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.ActivoEmpleados)
                    .HasForeignKey(d => d.EmpleadoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Empleado");
            });


            modelBuilder.Entity<ActivoFijo>(entity =>
            {
                entity.ToTable("ActivoFijo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("codigo");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.ValorActivo)
                    .HasColumnType("decimal(9, 2)")
                    .HasColumnName("valorActivo");

                entity.Property(e => e.ValorResidual)
                    .HasColumnType("decimal(9, 2)")
                    .HasColumnName("valorResidual");

                entity.Property(e => e.VidaUtil).HasColumnName("vidaUtil");
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.ToTable("Empleado");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("apellido");

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("direccion");

                entity.Property(e => e.Dni)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("dni");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("telefono");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
