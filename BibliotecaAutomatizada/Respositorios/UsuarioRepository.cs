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
    public class UsuarioRepository : IUsuarioRepository
    {
        private ConexionDB conexion = new ConexionDB();

        public Usuario Login(string correo, string password)
        {
            Usuario usuario = null;

            using (SqlConnection con = conexion.ObtenerConexion())
            {
                con.Open();

                string query = "SELECT * FROM Usuario WHERE Correo=@Correo AND Password=@Password";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Correo", correo);
                cmd.Parameters.AddWithValue("@Password", password);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    usuario = new Usuario()
                    {
                        Id = (int)dr["Id"],
                        Nombre = dr["Nombre"].ToString(),
                        Correo = dr["Correo"].ToString(),
                        Password = dr["Password"].ToString()
                    };
                }
            }

            return usuario;
        }
    }
}
