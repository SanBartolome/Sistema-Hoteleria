using Microsoft.EntityFrameworkCore;
using HotelBahia.BussinesLogic.Domain;

namespace HotelBahia.DataAccess.Context
{
    public partial class HoteleriaContext : DbContext
    {
        public HoteleriaContext()
        {
        }

        public HoteleriaContext(DbContextOptions<HoteleriaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actividad> Actividad { get; set; }
        public virtual DbSet<AsignacionHabitacion> AsignacionHabitacion { get; set; }
        public virtual DbSet<Empleado> Empleado { get; set; }
        public virtual DbSet<EstadoHabitacion> EstadoHabitacion { get; set; }
        public virtual DbSet<Habitacion> Habitacion { get; set; }
        public virtual DbSet<HabitacionActividad> HabitacionActividad { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<TipoActividad> TipoActividad { get; set; }
        public virtual DbSet<TipoHabitacion> TipoHabitacion { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Incidencia> Incidencia { get; set; }
        public virtual DbSet<ObjetoPerdido> ObjetoPerdido { get; set; }
        public virtual DbSet<ResultadoEvaluacion> ResultadoEvaluacion { get; set; }
        public virtual DbSet<EvaluacionSupervisor> EvaluacionSupervisor { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=DESKTOP-HB1UI73;Database=Hoteleria;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Actividad>(entity =>
            {
                entity.Property(e => e.ActividadId).HasColumnName("ActividadID");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TipoActividadId).HasColumnName("TipoActividadID");

                entity.HasOne(d => d.TipoActividad)
                    .WithMany(p => p.Actividad)
                    .HasForeignKey(d => d.TipoActividadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Actividad_TipoActividad");

               
            });

            modelBuilder.Entity<AsignacionHabitacion>(entity =>
            {
                entity.Property(e => e.AsignacionHabitacionId).HasColumnName("AsignacionHabitacionID");

                entity.Property(e => e.EmpleadoId).HasColumnName("EmpleadoID");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.HabitacionId).HasColumnName("HabitacionID");

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.AsignacionHabitacion)
                    .HasForeignKey(d => d.EmpleadoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AsignacionHabitacion_Empleado");

                entity.HasOne(d => d.Habitacion)
                    .WithMany(p => p.AsignacionHabitacion)
                    .HasForeignKey(d => d.HabitacionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AsignacionHabitacion_Habitacion");
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.HasIndex(e => e.UsuarioNombre)
                    .HasName("IX_Empleado")
                    .IsUnique();

                entity.Property(e => e.EmpleadoId).HasColumnName("EmpleadoID");

                entity.Property(e => e.Apellidos)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Correo)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.Property(e => e.Nombres)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sexo)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.UsuarioNombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EstadoHabitacion>(entity =>
            {
                entity.Property(e => e.EstadoHabitacionId).HasColumnName("EstadoHabitacionID");

                entity.Property(e => e.EstadoNombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Habitacion>(entity =>
            {
                entity.Property(e => e.HabitacionId).HasColumnName("HabitacionID");

                entity.Property(e => e.EstadoHabitacionId).HasColumnName("EstadoHabitacionID");

                entity.Property(e => e.TipoHabitacionId).HasColumnName("TipoHabitacionID");

                entity.HasOne(d => d.EstadoHabitacion)
                    .WithMany(p => p.Habitacion)
                    .HasForeignKey(d => d.EstadoHabitacionId)
                    .HasConstraintName("FK_Habitacion_EstadoHabitacion");

                entity.HasOne(d => d.TipoHabitacion)
                    .WithMany(p => p.Habitacion)
                    .HasForeignKey(d => d.TipoHabitacionId)
                    .HasConstraintName("FK_Habitacion_TipoHabitacion");

                entity.HasQueryFilter(x => !x.IsDelete);
            });

            modelBuilder.Entity<HabitacionActividad>(entity =>
            {
                entity.Property(e => e.HabitacionActividadId).HasColumnName("HabitacionActividadID");

                entity.Property(e => e.ActividadId).HasColumnName("ActividadID");

                entity.Property(e => e.HabitacionId).HasColumnName("HabitacionID");

                entity.HasOne(d => d.Actividad)
                    .WithMany(p => p.HabitacionActividad)
                    .HasForeignKey(d => d.ActividadId)
                    .HasConstraintName("FK_HabitacionActividad_Actividad");

                entity.HasOne(d => d.Habitacion)
                    .WithMany(p => p.HabitacionActividad)
                    .HasForeignKey(d => d.HabitacionId)
                    .HasConstraintName("FK_HabitacionActividad_Habitacion");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.Property(e => e.RolId).HasColumnName("RolID");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TipoActividad>(entity =>
            {
                entity.Property(e => e.TipoActividadId).HasColumnName("TipoActividadID");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TipoHabitacion>(entity =>
            {
                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.UsuarioNombre);

                entity.Property(e => e.UsuarioNombre)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Password)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.RolId).HasColumnName("RolID");

                entity.HasOne(d => d.Rol)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.RolId)
                    .HasConstraintName("FK_Usuario_Rol");
            });

            modelBuilder.Entity<Incidencia>(entity =>
            {
                entity.Property(e => e.IncidenciaID).HasColumnName("IncidenciaID");

                entity.Property(e => e.EmpleadoId).HasColumnName("EmpleadoID");

                entity.Property(e => e.Prioridad)
                .HasMaxLength(10)
                .IsUnicode(false);

                entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);

                entity.Property(e => e.Encargado)
                .HasMaxLength(50)
                .IsUnicode(false);

                entity.Property(e => e.FechaAbierto).HasColumnType("datetime");

                entity.Property(e => e.FechaCerrado).HasColumnType("datetime");

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.Incidencia)
                    .HasForeignKey(d => d.EmpleadoId)
                    .HasConstraintName("FK_Incidencia_Empleado");
            });

            modelBuilder.Entity<ObjetoPerdido>(entity =>
            {
                entity.Property(e => e.ObjetoPerdidoId).HasColumnName("ObjetoPerdidoID");

                entity.Property(e => e.EmpleadoId).HasColumnName("EmpleadoID");

                entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false);

                entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.ObjetoPerdido)
                    .HasForeignKey(d => d.EmpleadoId)
                    .HasConstraintName("FK_ObjetoPerdido_Empleado");
            });

            modelBuilder.Entity<ResultadoEvaluacion>(entity =>
            {
                entity.Property(e => e.ResultadoEvaluacionId).HasColumnName("ResultadoEvaluacionID");

                entity.Property(e => e.Comentarios)
                .HasMaxLength(100)
                .IsUnicode(false);
            });

            modelBuilder.Entity<EvaluacionSupervisor>(entity =>
            {
                entity.Property(e => e.EvaluacionSupervisorId).HasColumnName("EvaluacionSupervisorID");

                entity.Property(e => e.EmpleadoId).HasColumnName("EmpleadoID");

                entity.Property(e => e.ResultadoEvaluacionId).HasColumnName("ResultadoEvaluacionID");

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.EvaluacionSupervisor)
                    .HasForeignKey(d => d.EmpleadoId)
                    .HasConstraintName("FK_EvaluacionSupervisor_Empleado");

                entity.HasOne(d => d.ResultadoEvaluacion)
                    .WithMany(p => p.EvaluacionSupervisor)
                    .HasForeignKey(d => d.ResultadoEvaluacionId)
                    .HasConstraintName("FK_EvaluacionSupervisor_ResultadoEvaluacion");
            });
        }
    }
}
