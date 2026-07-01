using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaAutomatizada.Modelos
{
    public class PrestamoDetalle
    {
        public int Id { get; set; }

        public int PrestamoId { get; set; }

        public int LibroId { get; set; }

        public DateTime? FechaDevolucion { get; set; }
    }
}
