using BibliotecaAutomatizada.Interfaces;
using BibliotecaAutomatizada.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaAutomatizada.Servicios
{
    public class LibroService
    {
        private readonly ILibroRepository repo;

        public LibroService(ILibroRepository repo)
        {
            this.repo = repo;
        }

        public void RegistrarLibro(Libro libro)
        {
            if (libro.Stock <= 0)
                throw new Exception("Stock inválido");

            repo.Insertar(libro);
        }

        public DataTable ObtenerLibros()
        {
            return repo.Listar();
        }

        public void EditarLibro(Libro libro)
        {
            repo.Editar(libro);
        }

        public void EliminarLibro(int id)
        {
            repo.Eliminar(id);
        }
    }
}
