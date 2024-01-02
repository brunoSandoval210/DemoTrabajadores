using DemoTrabajadoresBackend.Entitdades;
using Microsoft.EntityFrameworkCore;

namespace DemoTrabajadoresBackend
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Departamento>(entity =>
            {
                entity.HasKey(e => e.IdDepartamento);

                entity.ToTable("Departamento");

                entity.Property(e => e.IdDepartamento)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.NombreDepartamento)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                // Relación uno a muchos con Provincia
                entity.HasMany(e => e.Provincias)
                    .WithOne(e => e.Departamento)
                    .HasForeignKey(e => e.IdDepartamento);
            });

            modelBuilder.Entity<Distrito>(entity =>
            {
                entity.HasKey(e => new { e.IdDistrito });

                entity.ToTable("Distrito");

                entity.Property(e => e.IdDistrito)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.NombreDistrito)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                // Relación muchos a uno con Provincia
                entity.HasOne(e => e.Provincia)
                    .WithMany(e => e.Distritos)
                    .HasForeignKey(e => e.IdProvincia);

                // Relación uno a muchos con Trabajador
                entity.HasMany(d => d.Trabajadores)
                    .WithOne(e => e.Distrito)
                    .HasForeignKey(t => t.IdDistrito);
            });

            modelBuilder.Entity<Provincia>(entity =>
            {
                entity.HasKey(e => e.IdProvincia);
                entity.ToTable("Provincia");
                entity.Property(e => e.NombreProvincia)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                // Relación muchos a uno con Departamento
                entity.HasOne(e => e.Departamento)
                    .WithMany(e => e.Provincias)
                    .HasForeignKey(e => e.IdDepartamento);

                // Relación uno a muchos con Distrito
                entity.HasMany(e => e.Distritos)
                    .WithOne(e => e.Provincia)
                    .HasForeignKey(e => e.IdProvincia);
            });

            modelBuilder.Entity<Trabajador>(entity =>
            {
                entity.HasKey(e => e.IdTrabajador);

                entity.ToTable("Trabajador");

                entity.Property(e => e.IdTrabajador)
                    .ValueGeneratedOnAdd();
                entity.Property(e => e.Nombres)
                    .HasMaxLength(250)
                    .IsUnicode(false);
                entity.Property(e => e.NroDocumento)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Sexo)
                    .HasMaxLength(1)
                    .IsUnicode(false);
                entity.Property(e => e.TipoDocumento)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(e => e.Distrito)
                    .WithMany(e => e.Trabajadores)
                    .HasForeignKey(e => new { e.IdDistrito});
            });


            base.OnModelCreating(modelBuilder);

        }
        public virtual DbSet<Departamento> Departamentos { get; set; }

        public virtual DbSet<Distrito> Distritos { get; set; }

        public virtual DbSet<Provincia> Provincia { get; set; }

        public virtual DbSet<Trabajador> Trabajadors { get; set; }


    }
}
