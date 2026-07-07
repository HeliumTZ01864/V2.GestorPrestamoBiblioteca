using BibliotecaAutomatizada.Interfaces;
using BibliotecaAutomatizada.Modelos;
using BibliotecaAutomatizada.Respositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaAutomatizada.Servicios
{
    public class UsuarioService
    {
        private IUsuarioRepository repo;

        public UsuarioService(IUsuarioRepository repo)
        {
            this.repo = repo;
        }

        public async Task<Usuario> IniciarSesionAsync(string correo, string password)
        {
            return await repo.LoginAsync(correo, password);
        }

        public void EditarUsuario(Usuario usuario)
        {
            repo.Editar(usuario);
        }

        public void EliminarUsuario(int id)
        {
            repo.Eliminar(id);
        }

        public async Task<List<Usuario>> ObtenerUsuariosAsync()
        {
            return await repo.ObtenerUsuariosAsync();
        }

        public async Task<bool> RegistrarUsuario(Usuario usuario)
        {
            return await repo.RegistrarAsync(usuario);
        }


    }
}
