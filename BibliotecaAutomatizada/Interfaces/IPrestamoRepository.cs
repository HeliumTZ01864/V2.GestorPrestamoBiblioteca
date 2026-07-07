using BibliotecaAutomatizada.Modelos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BibliotecaAutomatizada.Interfaces
{
    public interface IPrestamoRepository
    {
        bool Registrar(Prestamo prestamo);

        System.Data.DataTable Listar();

        bool Editar(Prestamo prestamo);

        bool Eliminar(int id);
    }
}