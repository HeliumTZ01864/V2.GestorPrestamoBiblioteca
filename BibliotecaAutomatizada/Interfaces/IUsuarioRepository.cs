using BibliotecaAutomatizada.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaAutomatizada.Interfaces
{
    public interface IUsuarioRepository
    {
        // Métodos Asíncronos (Los que tú optimizaste)
        Task<Usuario> LoginAsync(string correo, string password);
        Task<bool> RegistrarAsync(Usuario usuario);
        Usuario Login(string correo, string password);

        void Editar(Usuario usuario);

        void Eliminar(int id);

        Task<List<Usuario>> ObtenerUsuariosAsync();
    }
}
