using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaAutomatizada.Modelos
{
    public class Multa
    {
        public int Id { get; set; }

        public int PrestamoDetalleId { get; set; }

        public decimal Monto { get; set; }

        public bool Pagada { get; set; }
    }
}
