namespace Booking_Service.Models
{
    public class Reserva
    {
        public int ReservaID { get; set; }
        public int UsuarioID { get; set; }
        public int EventoID { get; set; }
        public int CantidadEntradas { get; set; }
        public DateTime FechaReserva { get; set; } 
        public bool EstaCancelada { get; set; } 
    }
}

