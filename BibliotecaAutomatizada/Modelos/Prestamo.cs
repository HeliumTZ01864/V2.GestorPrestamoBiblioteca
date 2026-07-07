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

        // NUEVOS CAMPOS PARA LA AUTOMATIZACIÓN
        public string CodigoBoleta { get; set; } // Ejemplo: "BOL-2026-XXXX"
        public string EstadoPrestamo { get; set; } // "PendienteRecojo", "Activo", "Finalizado"

        public List<PrestamoDetalle> Detalles { get; set; } = new List<PrestamoDetalle>();

    }
}
