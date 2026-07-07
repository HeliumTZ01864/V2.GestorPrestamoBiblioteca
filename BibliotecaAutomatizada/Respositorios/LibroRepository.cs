using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using BibliotecaAutomatizada.Database;
using BibliotecaAutomatizada.Interfaces;
using BibliotecaAutomatizada.Modelos;

namespace BibliotecaAutomatizada.Respositorios
{
    public class LibroRepository : ILibroRepository
    {
        private ConexionDB conexion = new ConexionDB();

        // 1. El método que ya tenías para el Módulo de Préstamos
        public async Task<bool> ActualizarStockAsync(int libroId, int cantidad)
        {
            using (SqlConnection con = conexion.ObtenerConexion())
            {
                await con.OpenAsync();
                string query = "UPDATE Libro SET Stock = Stock + @Cantidad WHERE Id = @LibroId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Cantidad", cantidad);
                    cmd.Parameters.AddWithValue("@LibroId", libroId);
                    int filas = await cmd.ExecuteNonQueryAsync();
                    return filas > 0;
                }
            }
        }

        // ==========================================
        // SOLUCIÓN A LOS 4 ERRORES DE COMPILACIÓN:
        // ==========================================

        // 2. Implementación de Insertar Libro (Síncrono para compatibilidad)
        public bool Insertar(Libro libro)
        {
            using (SqlConnection con = conexion.ObtenerConexion())
            {
                con.Open();
                string query = "INSERT INTO Libro (Titulo, Autor, Stock, CategoriaId) VALUES (@Titulo, @Autor, @Stock, @CategoriaId)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Titulo", libro.Titulo);
                    cmd.Parameters.AddWithValue("@Autor", libro.Autor);
                    cmd.Parameters.AddWithValue("@Stock", libro.Stock);
                    cmd.Parameters.AddWithValue("@CategoriaId", libro.CategoriaId);

                    int filas = cmd.ExecuteNonQuery();
                    return filas > 0;
                }
            }
        }

        // 3. Implementación de Listar Libros
        public DataTable Listar()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = conexion.ObtenerConexion())
            {
                con.Open();

                string query = @"
            SELECT
                l.Id,
                l.Titulo,
                l.Autor,
                l.Stock,
                l.CategoriaId,
                c.Nombre AS Categoria
            FROM Libro l
            INNER JOIN Categoria c
                ON l.CategoriaId = c.Id";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.Fill(dt);
            }

            return dt;
        }

        // 4. Implementación de Editar Libro
        public bool Editar(Libro libro)
        {
            using (SqlConnection con = conexion.ObtenerConexion())
            {
                con.Open();
                string query = "UPDATE Libro SET Titulo = @Titulo, Autor = @Autor, Stock = @Stock, CategoriaId = @CategoriaId WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Titulo", libro.Titulo);
                    cmd.Parameters.AddWithValue("@Autor", libro.Autor);
                    cmd.Parameters.AddWithValue("@Stock", libro.Stock);
                    cmd.Parameters.AddWithValue("@CategoriaId", libro.CategoriaId);
                    cmd.Parameters.AddWithValue("@Id", libro.Id);

                    int filas = cmd.ExecuteNonQuery();
                    return filas > 0;
                }
            }
        }

        // 5. Implementación de Eliminar Libro
        public bool Eliminar(int id)
        {
            using (SqlConnection con = conexion.ObtenerConexion())
            {
                con.Open();
                string query = "DELETE FROM Libro WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    int filas = cmd.ExecuteNonQuery();
                    return filas > 0;
                }
            }
        }
    }
}