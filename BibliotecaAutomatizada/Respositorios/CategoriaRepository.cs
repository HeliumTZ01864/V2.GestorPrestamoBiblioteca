using BibliotecaAutomatizada.Database;
using BibliotecaAutomatizada.Interfaces;
using BibliotecaAutomatizada.Modelos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaAutomatizada.Respositorios
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private ConexionDB conexion = new ConexionDB();

        public List<Categoria> Listar()
        {
            List<Categoria> lista = new List<Categoria>();

            using (SqlConnection con = conexion.ObtenerConexion())
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM Categoria", con);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Categoria()
                    {
                        Id = (int)dr["Id"],
                        Nombre = dr["Nombre"].ToString(),
                        Descricao = dr["Descricao"].ToString()
                    });
                }
            }

            return lista;
        }

        public void Insertar(Categoria categoria)
        {
            using (SqlConnection con = conexion.ObtenerConexion())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Categoria (Nombre, Descricao) VALUES (@Nombre, @Descricao)", con);
                cmd.Parameters.AddWithValue("@Nombre", categoria.Nombre);
                cmd.Parameters.AddWithValue("@Descricao", categoria.Descricao ?? "");
                cmd.ExecuteNonQuery();
            }
        }

        public void Modificar(Categoria categoria)
        {
            using (SqlConnection con = conexion.ObtenerConexion())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Categoria SET Nombre = @Nombre, Descricao = @Descricao WHERE Id = @Id", con);
                cmd.Parameters.AddWithValue("@Nombre", categoria.Nombre);
                cmd.Parameters.AddWithValue("@Descricao", categoria.Descricao ?? "");
                cmd.Parameters.AddWithValue("@Id", categoria.Id);
                cmd.ExecuteNonQuery();
            }
        }

        public void Eliminar(int id)
        {
            using (SqlConnection con = conexion.ObtenerConexion())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Categoria WHERE Id = @Id", con);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
