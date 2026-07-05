using BibliotecaAutomatizada.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BibliotecaAutomatizada.Interfaces
{
    public interface IPrestamoRepository
    {
        Task<string> RegistrarReservaClienteAsync(int usuarioId, List<int> libroIds);
        Task<Prestamo> ObtenerPorCodigoBoletaAsync(string codigoBoleta);
        Task<bool> ActualizarEstadoPrestamoAsync(int prestamoId, string nuevoEstado);
        Task<int> CancelarReservasExpiradasAsync();

        // ¡NUEVO! Método para que el cliente revise sus reservas generadas
        Task<List<Prestamo>> ObtenerHistorialClienteAsync(int usuarioId);
    }
}