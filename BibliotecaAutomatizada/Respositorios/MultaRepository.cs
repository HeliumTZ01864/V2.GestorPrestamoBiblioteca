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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public DataTable ListarPorPrestamoDetalle(int prestamoDetalleId)
        {
            throw new NotImplementedException();
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

