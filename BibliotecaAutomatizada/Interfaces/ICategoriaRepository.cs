using BibliotecaAutomatizada.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaAutomatizada.Interfaces
{
    public interface ICategoriaRepository
    {
        List<Categoria> Listar();
        void Insertar(Categoria categoria);
        void Modificar(Categoria categoria);
        void Eliminar(int id);
    }
}
