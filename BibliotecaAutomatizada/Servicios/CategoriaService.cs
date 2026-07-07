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
    public class CategoriaService
    {
        private readonly ICategoriaRepository repo;

        public CategoriaService(ICategoriaRepository repo)
        {
            this.repo = repo;
        }

        public List<Categoria> ObtenerCategorias()
        {
            return repo.Listar();
        }

        public void RegistrarCategoria(Categoria categoria)
        {
            if (string.IsNullOrWhiteSpace(categoria.Nombre))
                throw new ArgumentException("El nombre de la categoría no puede estar vacío.");

            repo.Insertar(categoria);
        }

        public void EditarCategoria(Categoria categoria)
        {
            if (categoria.Id <= 0)
                throw new ArgumentException("Id de categoría inválido.");

            repo.Modificar(categoria);
        }

        public void EliminarCategoria(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id de categoría inválido.");

            repo.Eliminar(id);
        }
    }
}
