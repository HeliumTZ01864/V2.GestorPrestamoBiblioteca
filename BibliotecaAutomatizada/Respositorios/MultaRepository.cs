using BibliotecaAutomatizada.Database;
using BibliotecaAutomatizada.Interfaces;
using BibliotecaAutomatizada.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaAutomatizada.Respositorios
{
    public class MultaRepository : IMultaRepository
    {
        private readonly ConexionDB conexion = new ConexionDB();

        public void Eliminar(int id)
        {
            using (SqlConnection con = conexion.ObtenerConexion())
            {
                con.Open();

                string query = "DELETE FROM Multa WHERE Id = @Id";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", id);

                cmd.ExecuteNonQuery();
            }
        }

        public void Insertar(Multa multa)
        {
            using (SqlConnection con = conexion.ObtenerConexion())
            {
                con.Open();

                string query = "INSERT INTO Multa (PrestamoDetalleId, Monto, Pagada) VALUES (@PrestamoDetalleId, @Monto, @Pagada)";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@PrestamoDetalleId", multa.PrestamoDetalleId);
                cmd.Parameters.AddWithValue("@Monto", multa.Monto);
                cmd.Parameters.AddWithValue("@Pagada", multa.Pagada);

                cmd.ExecuteNonQuery();
            }
        }

        public DataTable Listar()
        {
            using (SqlConnection con = conexion.ObtenerConexion())
            {
                con.Open();

                string query = @"
                    SELECT 
                        M.Id,
                        M.PrestamoDetalleId,
                        U.Nombre AS Usuario,
                        L.Titulo AS Libro,
                        M.Monto,
                        CASE WHEN M.Pagada = 1 THEN 'Sí' ELSE 'No' END AS Pagada
                    FROM Multa M
                    INNER JOIN PrestamoDetalle PD ON M.PrestamoDetalleId = PD.Id
                    INNER JOIN Prestamo P ON PD.PrestamoId = P.Id
                    INNER JOIN Usuario U ON P.UsuarioId = U.Id
                    INNER JOIN Libro L ON PD.LibroId = L.Id";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
        }

        public DataTable ListarPorPrestamoDetalle(int prestamoDetalleId)
        {
            using (SqlConnection con = conexion.ObtenerConexion())
            {
                con.Open();

                string query = @"
                    SELECT 
                        M.Id,
                        M.PrestamoDetalleId,
                        U.Nombre AS Usuario,
                        L.Titulo AS Libro,
                        M.Monto,
                        CASE WHEN M.Pagada = 1 THEN 'Sí' ELSE 'No' END AS Pagada
                    FROM Multa M
                    INNER JOIN PrestamoDetalle PD ON M.PrestamoDetalleId = PD.Id
                    INNER JOIN Prestamo P ON PD.PrestamoId = P.Id
                    INNER JOIN Usuario U ON P.UsuarioId = U.Id
                    INNER JOIN Libro L ON PD.LibroId = L.Id
                    WHERE M.PrestamoDetalleId = @PrestamoDetalleId";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@PrestamoDetalleId", prestamoDetalleId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
        }

        public void MarcarComoPagada(int id)
        {
            using (SqlConnection con = conexion.ObtenerConexion())
            {
                con.Open();

                string query = "UPDATE Multa SET Pagada = 1 WHERE Id = @Id";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", id);

                cmd.ExecuteNonQuery();
            }
        }
    }
}

