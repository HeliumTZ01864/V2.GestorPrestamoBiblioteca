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
                        Nombre = dr["Nombre"].ToString()
                    });
                }
            }

            return lista;
        }
    }
}
