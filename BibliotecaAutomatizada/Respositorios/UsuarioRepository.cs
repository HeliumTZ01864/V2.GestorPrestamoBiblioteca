using BibliotecaAutomatizada.Database;
using BibliotecaAutomatizada.Interfaces;
using BibliotecaAutomatizada.Modelos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BibliotecaAutomatizada.Respositorios
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private ConexionDB conexion = new ConexionDB();

        private string EncriptarContraseña(string contraseña)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(contraseña));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public async Task<Usuario> LoginAsync(string correo, string password)
        {
            Usuario usuario = null;
            string passwordEncriptada = EncriptarContraseña(password);

            using (SqlConnection con = conexion.ObtenerConexion())
            {
                await con.OpenAsync();

                // Crear administrador si no existe
                string queryCheck = "SELECT COUNT(*) FROM Usuario WHERE Correo='admin@biblioteca.com'";

                using (SqlCommand cmdCheck = new SqlCommand(queryCheck, con))
                {
                    int existe = (int)await cmdCheck.ExecuteScalarAsync();

                    if (existe == 0)
                    {
                        string queryInsert = @"INSERT INTO Usuario
                                            (Nombre, Correo, Contrasena, Rol)
                                            VALUES
                                            ('Administrador','admin@biblioteca.com',@Pass,'Admin')";

                        using (SqlCommand cmdInsert = new SqlCommand(queryInsert, con))
                        {
                            cmdInsert.Parameters.AddWithValue("@Pass", EncriptarContraseña("123456"));
                            await cmdInsert.ExecuteNonQueryAsync();
                        }
                    }
                }

                // Login
                string query = @"SELECT Id, Nombre, Correo, Rol
                         FROM Usuario
                         WHERE Correo=@Correo
                         AND Contrasena=@Password";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Correo", correo);
                    cmd.Parameters.AddWithValue("@Password", passwordEncriptada);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            usuario = new Usuario
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Nombre = reader["Nombre"].ToString(),
                                Correo = reader["Correo"].ToString(),
                                Rol = (TipoRol)Enum.Parse(typeof(TipoRol), reader["Rol"].ToString())
                            };
                        }
                    }
                }
            }

            return usuario;
        }

        public async Task<bool> RegistrarAsync(Usuario usuario)
        {
            // Usamos .Contraseña en lugar de .Password
            string passwordEncriptada = EncriptarContraseña(usuario.Contrasena);

            using (SqlConnection con = conexion.ObtenerConexion())
            {
                await con.OpenAsync();

                string query = "INSERT INTO Usuario (Nombre, Correo, Contrasena, Rol) VALUES (@Nombre, @Correo, @Contrasena, 'Cliente')";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@Correo", usuario.Correo);
                    cmd.Parameters.AddWithValue("@Contrasena", passwordEncriptada);

                    int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                    return filasAfectadas > 0;
                }
            }
        }
        public void Editar(Usuario usuario)
        {
            using (SqlConnection con = conexion.ObtenerConexion())
            {
                con.Open();

                SqlCommand cmd;

                if (!string.IsNullOrWhiteSpace(usuario.Contrasena))
                {
                    // Actualiza incluyendo la contraseña
                    string query = @"UPDATE Usuario
                             SET Nombre = @Nombre,
                                 Correo = @Correo,
                                 Contrasena = @Contrasena,
                                 Rol = @Rol
                             WHERE Id = @Id";

                    cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@Contrasena", EncriptarContraseña(usuario.Contrasena));
                }
                else
                {
                    // Actualiza sin modificar la contraseña
                    string query = @"UPDATE Usuario
                             SET Nombre = @Nombre,
                                 Correo = @Correo,
                                 Rol = @Rol
                             WHERE Id = @Id";

                    cmd = new SqlCommand(query, con);
                }

                cmd.Parameters.AddWithValue("@Id", usuario.Id);
                cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                cmd.Parameters.AddWithValue("@Correo", usuario.Correo);
                cmd.Parameters.AddWithValue("@Rol", usuario.Rol.ToString());

                cmd.ExecuteNonQuery();
            }
        }

        public void Eliminar(int id)
        {
            using (SqlConnection con = conexion.ObtenerConexion())
            {
                con.Open();

                string query = "DELETE FROM Usuario WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public Usuario Login(string correo, string password)
        {
            return LoginAsync(correo, password).GetAwaiter().GetResult();
        }

        public async Task<List<Usuario>> ObtenerUsuariosAsync()
        {
            List<Usuario> lista = new List<Usuario>();

            using (SqlConnection con = conexion.ObtenerConexion())
            {
                await con.OpenAsync();

                string query = @"SELECT Id, Nombre, Correo, Rol
                         FROM Usuario";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            lista.Add(new Usuario
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Nombre = reader["Nombre"].ToString(),
                                Correo = reader["Correo"].ToString(),
                                Rol = (TipoRol)Enum.Parse(
                                    typeof(TipoRol),
                                    reader["Rol"].ToString())
                            });
                        }
                    }
                }
            }


            return lista;
        }
    }
}