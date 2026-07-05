using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaAutomatizada.Modelos
{
    public enum TipoRol
    {
        Admin,
        Cliente
    }

    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Contraseña { get; set; } = string.Empty; // Recuerda encriptarla en producción
        public TipoRol Rol { get; set; } = TipoRol.Cliente; // Por defecto es Cliente
    }
}
