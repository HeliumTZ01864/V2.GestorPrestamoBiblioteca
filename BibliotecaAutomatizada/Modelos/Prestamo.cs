using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaAutomatizada.Modelos
{
    public class Prestamo
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }

        public DateTime FechaPrestamo { get; set; }

        public DateTime FechaLimite { get; set; }
    }
}
