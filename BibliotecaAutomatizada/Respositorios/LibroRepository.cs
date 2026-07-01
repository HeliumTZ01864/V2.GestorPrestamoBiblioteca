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
    public class LibroRepository : ILibroRepository
    {
        private readonly ConexionDB conexion = new ConexionDB();


        public void Insertar(Libro libro)
        {
            using (SqlConnection con = conexion.ObtenerConexion())
            {
                con.Open();

                string query = "INSERT INTO Libro (Titulo, Autor, Stock, CategoriaId) VALUES (@Titulo, @Autor, @Stock, @CategoriaId)";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@Titulo", libro.Titulo);
                cmd.Parameters.AddWithValue("@Autor", libro.Autor);
                cmd.Parameters.AddWithValue("@Stock", libro.Stock);
                cmd.Parameters.AddWithValue("@CategoriaId", libro.CategoriaId);

                cmd.ExecuteNonQuery();
            }
        }

        public void Eliminar(int id)
        {
            using (SqlConnection con = conexion.ObtenerConexion())
            {
                con.Open();

                string query = "DELETE FROM Libro WHERE Id=@Id";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", id);

                cmd.ExecuteNonQuery();
            }
        }

        public void Editar(Libro libro)
        {
            using (SqlConnection con = conexion.ObtenerConexion())
            {
                con.Open();

                string query = @"UPDATE Libro 
                         SET Titulo=@Titulo, 
                             Autor=@Autor, 
                             Stock=@Stock, 
                             CategoriaId=@CategoriaId 
                         WHERE Id=@Id";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@Titulo", libro.Titulo);
                cmd.Parameters.AddWithValue("@Autor", libro.Autor);
                cmd.Parameters.AddWithValue("@Stock", libro.Stock);
                cmd.Parameters.AddWithValue("@CategoriaId", libro.CategoriaId);
                cmd.Parameters.AddWithValue("@Id", libro.Id);

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
            L.Id,
            L.Titulo,
            L.Autor,
            L.Stock,
            C.Nombre AS Categoria
        FROM Libro L
        INNER JOIN Categoria C ON L.CategoriaId = C.Id";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
        }


    }
}
