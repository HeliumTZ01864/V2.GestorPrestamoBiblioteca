using BibliotecaAutomatizada.Interfaces;
using BibliotecaAutomatizada.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaAutomatizada.Servicios
{
    internal class MultaService
    {
        private readonly IMultaRepository repo;
        private readonly ICalculadoraMulta calculadora;

        public MultaService(IMultaRepository repo, ICalculadoraMulta calculadora)
        {
            this.repo = repo;
            this.calculadora = calculadora;
        }

        /// <summary>
        /// Genera una multa a partir de un PrestamoDetalle con devolución atrasada.
        /// La fecha límite de devolución es 7 días después del préstamo.
        /// </summary>
        public void GenerarMulta(int prestamoDetalleId, DateTime fechaPrestamo, DateTime fechaDevolucion)
        {
            DateTime fechaLimite = fechaPrestamo.AddDays(7);

            if (fechaDevolucion <= fechaLimite)
                throw new Exception("No corresponde multa: el libro fue devuelto a tiempo.");

            int diasAtraso = (fechaDevolucion - fechaLimite).Days;
            decimal monto = calculadora.Calcular(diasAtraso);

            Multa multa = new Multa
            {
                PrestamoDetalleId = prestamoDetalleId,
                Monto = monto,
                Pagada = false
            };

            repo.Insertar(multa);
        }

        /// <summary>
        /// Registra una multa manualmente con monto indicado.
        /// </summary>
        public void RegistrarMulta(Multa multa)
        {
            if (multa.PrestamoDetalleId <= 0)
                throw new Exception("Debe seleccionar un préstamo detalle válido.");

            if (multa.Monto <= 0)
                throw new Exception("El monto de la multa debe ser mayor a cero.");

            repo.Insertar(multa);
        }

        public void PagarMulta(int id)
        {
            if (id <= 0)
                throw new Exception("ID de multa inválido.");

            repo.MarcarComoPagada(id);
        }

        public void EliminarMulta(int id)
        {
            if (id <= 0)
                throw new Exception("ID de multa inválido.");

            repo.Eliminar(id);
        }

        public DataTable ObtenerMultas()
        {
            return repo.Listar();
        }

        public DataTable ObtenerMultasPorDetalle(int prestamoDetalleId)
        {
            return repo.ListarPorPrestamoDetalle(prestamoDetalleId);
        }
    }
}
