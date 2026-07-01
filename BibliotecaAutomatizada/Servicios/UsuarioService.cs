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

        public Usuario IniciarSesion(string correo, string password)
        {
            return repo.Login(correo, password);
        }
    }
}
