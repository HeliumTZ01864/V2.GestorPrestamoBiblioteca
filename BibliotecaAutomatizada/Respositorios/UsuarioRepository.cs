using BibliotecaAutomatizada.Database;
using BibliotecaAutomatizada.Interfaces;
using BibliotecaAutomatizada.Modelos;
using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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

                // =====================================================================
                // SCRIPT TEMPORAL DE AUTO-REGISTRO (Solo para probar por primera vez)
                // =====================================================================
                string queryCheck = "SELECT COUNT(*) FROM Usuario WHERE Correo = 'admin@biblioteca.com'";
                using (SqlCommand cmdCheck = new SqlCommand(queryCheck, con))
                {
                    int existe = (int)await cmdCheck.ExecuteScalarAsync();
                    if (existe == 0)
                    {
                        string queryInsert = "INSERT INTO Usuario (Nombre, Correo, Contraseña, Rol) VALUES ('Administrador', 'admin@biblioteca.com', @Pass, 'Admin')";
                        using (SqlCommand cmdInsert = new SqlCommand(queryInsert, con))
                        {
                            cmdInsert.Parameters.AddWithValue("@Pass", EncriptarContraseña("123456"));
                            await cmdInsert.ExecuteNonQueryAsync();
                        }
                    }
                }
                // =====================================================================
            }

            return usuario;
        }

        public async Task<bool> RegistrarClienteAsync(Usuario usuario)
        {
            // Usamos .Contraseña en lugar de .Password
            string passwordEncriptada = EncriptarContraseña(usuario.Contraseña);

            using (SqlConnection con = conexion.ObtenerConexion())
            {
                await con.OpenAsync();

                string query = "INSERT INTO Usuario (Nombre, Correo, Contraseña, Rol) VALUES (@Nombre, @Correo, @Contraseña, 'Cliente')";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@Correo", usuario.Correo);
                    cmd.Parameters.AddWithValue("@Contraseña", passwordEncriptada);

                    int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                    return filasAfectadas > 0;
                }
            }
        }

        public Usuario Login(string correo, string password)
        {
            // Ejecuta el método asíncrono bloqueando el hilo de forma segura para diseño síncrono
            return Task.Run(() => LoginAsync(correo, password)).GetAwaiter().GetResult();
        }
    }
}