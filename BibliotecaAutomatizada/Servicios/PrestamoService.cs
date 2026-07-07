using BibliotecaAutomatizada.Interfaces;
using BibliotecaAutomatizada.Modelos;
using System;
using System.Data;

namespace BibliotecaAutomatizada.Servicios
{
    public class PrestamoService
    {

        private readonly IPrestamoRepository repo;


        public PrestamoService(IPrestamoRepository repo)
        {
            this.repo = repo;
        }



        public void RegistrarPrestamo(Prestamo prestamo)
        {

            if (prestamo.UsuarioId <= 0)
                throw new Exception(
                "Seleccione un usuario");


            if (prestamo.Detalles.Count == 0)
                throw new Exception(
                "Debe agregar libros");


            prestamo.FechaPrestamo = DateTime.Now;


            prestamo.CodigoBoleta =
            "BOL-" +
            DateTime.Now.Year +
            "-" +
            Guid.NewGuid()
            .ToString()
            .Substring(0, 4)
            .ToUpper();



            prestamo.EstadoPrestamo =
            "PendienteRecojo";


            bool ok =
            repo.Registrar(prestamo);



            if (!ok)
                throw new Exception(
                "No se pudo registrar el préstamo");

        }



        public DataTable ObtenerPrestamos()
        {
            return repo.Listar();
        }



        public void ActualizarPrestamo(Prestamo prestamo)
        {
            repo.Editar(prestamo);
        }



        public void EliminarPrestamo(int id)
        {
            repo.Eliminar(id);
        }

    }
}