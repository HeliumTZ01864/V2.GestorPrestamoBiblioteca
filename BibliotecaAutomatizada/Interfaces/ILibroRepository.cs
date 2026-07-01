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
        void Insertar(Libro libro);
        void Editar(Libro libro);
        void Eliminar(int id);
        DataTable Listar();
    }

}
