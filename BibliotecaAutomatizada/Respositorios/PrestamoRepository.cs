using System;
using BibliotecaAutomatizada.Database;
using BibliotecaAutomatizada.Interfaces;
using BibliotecaAutomatizada.Modelos;
using System.Data;
using System.Data.SqlClient;

namespace BibliotecaAutomatizada.Respositorios
{
    public class PrestamoRepository : IPrestamoRepository
    {
        private ConexionDB conexion = new ConexionDB();


        public bool Registrar(Prestamo prestamo)
        {
            using (SqlConnection con = conexion.ObtenerConexion())
            {
                con.Open();

                SqlTransaction trans = con.BeginTransaction();

                try
                {

                    // 1. Insertar préstamo
                    string sqlPrestamo = @"
                    INSERT INTO Prestamo
                    (
                        UsuarioId,
                        FechaPrestamo,
                        CodigoBoleta,
                        EstadoPrestamo
                    )
                    VALUES
                    (
                        @UsuarioId,
                        @FechaPrestamo,
                        @CodigoBoleta,
                        @EstadoPrestamo
                    );

                    SELECT SCOPE_IDENTITY();";


                    int prestamoId;


                    using (SqlCommand cmd = new SqlCommand(sqlPrestamo, con, trans))
                    {

                        cmd.Parameters.AddWithValue("@UsuarioId", prestamo.UsuarioId);
                        cmd.Parameters.AddWithValue("@FechaPrestamo", prestamo.FechaPrestamo);
                        cmd.Parameters.AddWithValue("@CodigoBoleta", prestamo.CodigoBoleta);
                        cmd.Parameters.AddWithValue("@EstadoPrestamo", prestamo.EstadoPrestamo);


                        prestamoId = Convert.ToInt32(cmd.ExecuteScalar());

                    }



                    // 2. Insertar detalles y descontar stock

                    foreach (var detalle in prestamo.Detalles)
                    {


                        string sqlDetalle = @"
                        INSERT INTO PrestamoDetalle
                        (
                            PrestamoId,
                            LibroId
                        )
                        VALUES
                        (
                            @PrestamoId,
                            @LibroId
                        )";


                        using (SqlCommand cmd = new SqlCommand(sqlDetalle, con, trans))
                        {
                            cmd.Parameters.AddWithValue("@PrestamoId", prestamoId);
                            cmd.Parameters.AddWithValue("@LibroId", detalle.LibroId);

                            cmd.ExecuteNonQuery();
                        }



                        // Descontar stock

                        string sqlStock = @"
                        UPDATE Libro
                        SET Stock = Stock - 1
                        WHERE Id=@LibroId
                        AND Stock > 0";


                        using (SqlCommand cmd = new SqlCommand(sqlStock, con, trans))
                        {

                            cmd.Parameters.AddWithValue("@LibroId", detalle.LibroId);


                            int filas = cmd.ExecuteNonQuery();


                            if (filas == 0)
                            {
                                throw new Exception(
                                "No hay stock disponible del libro");
                            }

                        }


                    }


                    trans.Commit();

                    return true;

                }
                catch
                {

                    trans.Rollback();

                    return false;

                }

            }
        }



        public DataTable Listar()
        {

            DataTable dt = new DataTable();


            using (SqlConnection con = conexion.ObtenerConexion())
            {

                con.Open();


                string sql = @"
                SELECT
                    p.Id,
                    p.CodigoBoleta,
                    p.UsuarioId,
                    p.FechaPrestamo,
                    p.EstadoPrestamo
                FROM Prestamo p";


                SqlDataAdapter da =
                new SqlDataAdapter(sql, con);


                da.Fill(dt);

            }


            return dt;

        }



        public bool Editar(Prestamo prestamo)
        {

            using (SqlConnection con = conexion.ObtenerConexion())
            {

                con.Open();


                string sql = @"
                UPDATE Prestamo
                SET
                    EstadoPrestamo=@EstadoPrestamo
                WHERE Id=@Id";


                using (SqlCommand cmd =
                new SqlCommand(sql, con))
                {

                    cmd.Parameters.AddWithValue("@Id", prestamo.Id);

                    cmd.Parameters.AddWithValue(
                    "@EstadoPrestamo",
                    prestamo.EstadoPrestamo);


                    return cmd.ExecuteNonQuery() > 0;

                }

            }

        }



        public bool Eliminar(int id)
        {

            using (SqlConnection con = conexion.ObtenerConexion())
            {

                con.Open();


                string sqlDetalle =
                "DELETE FROM PrestamoDetalle WHERE PrestamoId=@Id";


                using (SqlCommand cmd =
                new SqlCommand(sqlDetalle, con))
                {

                    cmd.Parameters.AddWithValue("@Id", id);

                    cmd.ExecuteNonQuery();

                }



                string sqlPrestamo =
                "DELETE FROM Prestamo WHERE Id=@Id";



                using (SqlCommand cmd =
                new SqlCommand(sqlPrestamo, con))
                {

                    cmd.Parameters.AddWithValue("@Id", id);


                    return cmd.ExecuteNonQuery() > 0;

                }


            }

        }

    }
}