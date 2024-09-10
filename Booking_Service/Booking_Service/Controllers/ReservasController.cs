using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Booking_Service.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Booking_Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservasController : ControllerBase
    {
        private readonly BookingDbContext _context;

        public ReservasController(BookingDbContext context)
        {
            _context = context;
        }

      
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reserva>>> GetReservas()
        {
            return await _context.Reservas.Where(r => !r.EstaCancelada).ToListAsync();
        }

      
        [HttpGet("{id}")]
        public async Task<ActionResult<Reserva>> GetReserva(int id)
        {
            var reserva = await _context.Reservas
                .FirstOrDefaultAsync(m => m.ReservaID == id && !m.EstaCancelada);
            if (reserva == null)
            {
                return NotFound();
            }

            return reserva;
        }

    
        [HttpPost]
        public async Task<ActionResult<Reserva>> CreateOrUpdateReserva([FromBody] Reserva reserva)
        {
            if (reserva == null)
            {
                return BadRequest("Reserva cannot be null.");
            }

            var evento = await _context.Eventos.FindAsync(reserva.EventoID);
            if (evento == null)
            {
                return BadRequest("El evento no existe.");
            }

          
            var reservaExistente = await _context.Reservas
                .FirstOrDefaultAsync(r => r.UsuarioID == reserva.UsuarioID && r.EventoID == reserva.EventoID && !r.EstaCancelada);

            if (reservaExistente != null)
            {
                if (reserva.CantidadEntradas != reservaExistente.CantidadEntradas)
                {
                    
                    evento.EntradasDisponibles += reservaExistente.CantidadEntradas; 
                    evento.EntradasDisponibles -= reserva.CantidadEntradas; 

                 
                    reservaExistente.CantidadEntradas = reserva.CantidadEntradas;
                    reservaExistente.FechaReserva = DateTime.Now;

                   
                    _context.Update(reservaExistente);
                }
                else
                {
            
                    reservaExistente.FechaReserva = DateTime.Now;
                    _context.Update(reservaExistente);
                }
            }
            else
            {
             
                reserva.FechaReserva = DateTime.Now;
                evento.EntradasDisponibles -= reserva.CantidadEntradas;

                _context.Add(reserva);
            }

        
            _context.Update(evento);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReserva), new { id = reserva.ReservaID }, reserva);
        }

   
        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> CancelReserva(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null || reserva.EstaCancelada)
            {
                return NotFound("La reserva no existe o ya está cancelada.");
            }

            reserva.EstaCancelada = true;
            var evento = await _context.Eventos.FindAsync(reserva.EventoID);
            if (evento != null)
            {
                evento.EntradasDisponibles += reserva.CantidadEntradas;
                _context.Update(evento);
            }

            _context.Update(reserva);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete("{id}/delete")]
        public async Task<IActionResult> DeleteReserva(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos.FindAsync(reserva.EventoID);
            if (evento != null)
            {
                evento.EntradasDisponibles += reserva.CantidadEntradas;
                _context.Update(evento);
            }

            _context.Reservas.Remove(reserva);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
