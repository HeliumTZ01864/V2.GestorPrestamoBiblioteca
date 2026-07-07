using BibliotecaAutomatizada.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaAutomatizada.Interfaces
{
    public interface ILibroRepository
    {
        // Método de stock que ya tenías
        Task<bool> ActualizarStockAsync(int libroId, int cantidad);

       
        bool Insertar(Libro libro);
        System.Data.DataTable Listar();
        bool Editar(Libro libro);
        bool Eliminar(int id);
    }

}
