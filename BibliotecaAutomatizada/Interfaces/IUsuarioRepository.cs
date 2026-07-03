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
        Task<bool> RegistrarClienteAsync(Usuario usuario);

        // ¡NUEVO! Soporte síncrono para quitar el error del formulario antiguo
        Usuario Login(string correo, string password);
    }
}
