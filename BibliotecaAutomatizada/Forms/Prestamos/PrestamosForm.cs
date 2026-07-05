using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BibliotecaAutomatizada.Interfaces;
using BibliotecaAutomatizada.Modelos;
using BibliotecaAutomatizada.Respositorios;

namespace BibliotecaAutomatizada.Forms.Prestamos
{
    public partial class PrestamosForm : Form
    {
        // 1. Inyectamos los repositorios necesarios para interactuar con SQL Server
        private readonly ILibroRepository _libroRepository = new LibroRepository();
        private readonly IPrestamoRepository _prestamoRepository = new PrestamoRepository();

        public PrestamosForm()
        {
            InitializeComponent();
        }

        // Evento que se dispara al abrir la pantalla
        private void PrestamosForm_Load(object sender, EventArgs e)
        {
            CargarLibrosDisponibles();
        }

        // Método auxiliar para llenar la tabla (DataGridView) de forma limpia
        private void CargarLibrosDisponibles()
        {
            try
            {
                // Listamos los libros usando el método compatible con la interfaz
                var libros = _libroRepository.Listar();
                dgvLibros.DataSource = libros;

                // Formato estético rápido
                dgvLibros.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el catálogo de libros: " + ex.Message, "Error de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Evento del botón para confirmar el proceso de reserva en la base de datos
        private async void btnReservar_Click(object sender, EventArgs e)
        {
            // Validar que la caja de texto del ID de usuario tenga un número válido
            if (string.IsNullOrWhiteSpace(txtUsuarioId.Text) || !int.TryParse(txtUsuarioId.Text, out int usuarioId))
            {
                MessageBox.Show("Por favor, ingrese un ID de usuario válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Capturar los IDs de los libros que el usuario seleccionó en la tabla
            List<int> librosSeleccionadosIds = new List<int>();

            foreach (DataGridViewRow row in dgvLibros.SelectedRows)
            {
                if (row.Cells["Id"].Value != null)
                {
                    int libroId = (int)row.Cells["Id"].Value;
                    librosSeleccionadosIds.Add(libroId);
                }
            }

            // Validar si seleccionaron al menos un libro
            if (librosSeleccionadosIds.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione al menos un libro haciendo clic en la fila completa.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                btnReservar.Enabled = false; // Deshabilitamos temporalmente para evitar re-envíos

                // EJECUCIÓN ASÍNCRONA: Registra el préstamo y ejecuta internamente el COMMIT / ROLLBACK de SQL
                string codigoBoleta = await _prestamoRepository.RegistrarReservaClienteAsync(usuarioId, librosSeleccionadosIds);

                // Disminuimos el stock de los libros seleccionados de forma asíncrona
                foreach (int idLibro in librosSeleccionadosIds)
                {
                    await _libroRepository.ActualizarStockAsync(idLibro, -1);
                }

                MessageBox.Show($"¡Reserva procesada exitosamente!\nCódigo de Boleta generado: {codigoBoleta}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpiamos los campos y refrescamos los datos actuales de la tabla
                txtUsuarioId.Clear();
                CargarLibrosDisponibles();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al procesar la reserva transaccional: " + ex.Message, "Error en Servidor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnReservar.Enabled = true; // Restauramos el botón
            }
        }

        // El evento click del label se deja vacío ya que no requiere lógica de negocio
        private void label1_Click(object sender, EventArgs e)
        {
        }
    }
}