using BibliotecaAutomatizada.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaAutomatizada.Interfaces
{
    public interface IMultaRepository
    {
        void Insertar(Multa multa);
        void MarcarComoPagada(int id);
        void Eliminar(int id);
        DataTable Listar();
        DataTable ListarPorPrestamoDetalle(int prestamoDetalleId);
    }
}
