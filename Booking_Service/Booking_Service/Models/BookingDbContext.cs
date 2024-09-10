using Microsoft.EntityFrameworkCore;

namespace Booking_Service.Models
{
    public partial class BookingDbContext : DbContext
    {
        public BookingDbContext()
        {
        }

        public BookingDbContext(DbContextOptions<BookingDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Reserva> Reservas { get; set; }
        public virtual DbSet<Evento> Eventos { get; set; } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("server=CACERES; database=bookingDB; integrated security=true; TrustServerCertificate=Yes");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
      
            modelBuilder.Entity<Reserva>(entity =>
            {
                entity.HasKey(e => e.ReservaID); 
                entity.ToTable("reservas"); 
                entity.Property(e => e.ReservaID).HasColumnName("reservaID"); 
                entity.Property(e => e.UsuarioID).HasColumnName("usuarioID"); 
                entity.Property(e => e.EventoID).HasColumnName("eventoID"); 
                entity.Property(e => e.CantidadEntradas).HasColumnName("cantidadEntradas"); 
                entity.Property(e => e.FechaReserva).HasColumnName("fechaReserva"); 
                entity.Property(e => e.EstaCancelada).HasColumnName("estaCancelada").HasDefaultValue(false); 
            });

           
            modelBuilder.Entity<Evento>(entity =>
            {
                entity.HasKey(e => e.EventoID); 
                entity.ToTable("eventos"); 
                entity.Property(e => e.EventoID).HasColumnName("eventoID"); 
                entity.Property(e => e.NombreEvento).HasColumnName("nombreEvento"); 
                entity.Property(e => e.EntradasDisponibles).HasColumnName("entradasDisponibles"); 
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
