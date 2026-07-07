using BibliotecaAutomatizada.Database;
using BibliotecaAutomatizada.Interfaces;
using BibliotecaAutomatizada.Modelos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BibliotecaAutomatizada.Respositorios
{
    public class PrestamoRepository : IPrestamoRepository
    {
        private ConexionDB conexion = new ConexionDB();

        // 1. SOLUCIÓN AL ERROR DE COMPILACIÓN: Implementación del registro de reservas
        public async Task<string> RegistrarReservaClienteAsync(int usuarioId, List<int> libroIds)
        {
            // Generamos un código único para la boleta de recojo
            string codigoBoleta = "BOL-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper();

            using (SqlConnection con = conexion.ObtenerConexion())
            {
                await con.OpenAsync();

                // Iniciamos la transacción de forma tradicional
                using (SqlTransaction transaction = con.BeginTransaction())
                {
                    try
                    {
                        // 1. Insertar la Cabecera del Préstamo
                        string queryPrestamo = @"INSERT INTO Prestamo (UsuarioId, FechaPrestamo, CodigoBoleta, EstadoPrestamo) 
                                         OUTPUT INSERTED.Id 
                                         VALUES (@UsuarioId, @FechaPrestamo, @CodigoBoleta, 'PendienteRecojo')";

                        int prestamoId = 0;

                        using (SqlCommand cmd = new SqlCommand(queryPrestamo, con, transaction))
                        {
                            cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);
                            cmd.Parameters.AddWithValue("@FechaPrestamo", DateTime.Now);
                            cmd.Parameters.AddWithValue("@CodigoBoleta", codigoBoleta);

                            prestamoId = (int)await cmd.ExecuteScalarAsync();
                        }

                        // 2. Insertar cada libro en el Detalle del Préstamo
                        string queryDetalle = "INSERT INTO PrestamoDetalle (PrestamoId, LibroId) VALUES (@PrestamoId, @LibroId)";

                        foreach (int libroId in libroIds)
                        {
                            using (SqlCommand cmdDetalle = new SqlCommand(queryDetalle, con, transaction))
                            {
                                cmdDetalle.Parameters.AddWithValue("@PrestamoId", prestamoId);
                                cmdDetalle.Parameters.AddWithValue("@LibroId", libroId);
                                await cmdDetalle.ExecuteNonQueryAsync();
                            }
                        }

                        // CORRECCIÓN: Uso de Commit síncrono compatible con SqlTransaction
                        transaction.Commit();
                        return codigoBoleta;
                    }
                    catch (Exception)
                    {
                        // CORRECCIÓN: Uso de Rollback síncrono si ocurre un fallo interno
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        // 2. Implementación para obtener el préstamo por código de boleta
        public async Task<Prestamo> ObtenerPorCodigoBoletaAsync(string codigoBoleta)
        {
            Prestamo prestamo = null;

            using (SqlConnection con = conexion.ObtenerConexion())
            {
                await con.OpenAsync();
                string query = "SELECT Id, UsuarioId, FechaPrestamo, CodigoBoleta, EstadoPrestamo FROM Prestamo WHERE CodigoBoleta = @CodigoBoleta";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@CodigoBoleta", codigoBoleta);

                    using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                    {
                        if (await dr.ReadAsync())
                        {
                            prestamo = new Prestamo()
                            {
                                Id = (int)dr["Id"],
                                UsuarioId = (int)dr["UsuarioId"],
                                FechaPrestamo = (DateTime)dr["FechaPrestamo"],
                                CodigoBoleta = dr["CodigoBoleta"].ToString(),
                                EstadoPrestamo = dr["EstadoPrestamo"].ToString()
                            };
                        }
                    }
                }
            }
            return prestamo;
        }

        // 3. Implementación para actualizar el estado (Ej: de 'PendienteRecojo' a 'Entregado')
        public async Task<bool> ActualizarEstadoPrestamoAsync(int prestamoId, string nuevoEstado)
        {
            using (SqlConnection con = conexion.ObtenerConexion())
            {
                await con.OpenAsync();
                string query = "UPDATE Prestamo SET EstadoPrestamo = @EstadoPrestamo WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@EstadoPrestamo", nuevoEstado);
                    cmd.Parameters.AddWithValue("@Id", prestamoId);

                    int filas = await cmd.ExecuteNonQueryAsync();
                    return filas > 0;
                }
            }
        }

        // 4. Tu método de cancelación de reservas expiradas
        public async Task<int> CancelarReservasExpiradasAsync()
        {
            using (SqlConnection con = conexion.ObtenerConexion())
            {
                await con.OpenAsync();
                string query = @"UPDATE Prestamo 
                                 SET EstadoPrestamo = 'Expirado' 
                                 WHERE EstadoPrestamo = 'PendienteRecojo' 
                                 AND DATEDIFF(day, FechaPrestamo, GETDATE()) >= 2";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    return await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        // 5. Tu método de historial del cliente
        public async Task<List<Prestamo>> ObtenerHistorialClienteAsync(int usuarioId)
        {
            List<Prestamo> listaHistorial = new List<Prestamo>();

            using (SqlConnection con = conexion.ObtenerConexion())
            {
                await con.OpenAsync();
                string query = "SELECT Id, UsuarioId, FechaPrestamo, CodigoBoleta, EstadoPrestamo FROM Prestamo WHERE UsuarioId = @UsuarioId ORDER BY FechaPrestamo DESC";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);

                    using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            Prestamo p = new Prestamo()
                            {
                                Id = (int)dr["Id"],
                                UsuarioId = (int)dr["UsuarioId"],
                                FechaPrestamo = (DateTime)dr["FechaPrestamo"],
                                CodigoBoleta = dr["CodigoBoleta"].ToString(),
                                EstadoPrestamo = dr["EstadoPrestamo"].ToString()
                            };
                            listaHistorial.Add(p);
                        }
                    }
                }
            }
            return listaHistorial;
        }
    }
}