using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BibliotecaAutomatizada.Forms.Categoria
{
    public partial class CategoriaForm : Form
    {
        private Respositorios.CategoriaRepository repo = new Respositorios.CategoriaRepository();
        private Respositorios.LibroRepository libroRepo = new Respositorios.LibroRepository();
        private int selectedId = -1; // id del libro seleccionado cuando se muestran los libros

        public CategoriaForm()
        {
            InitializeComponent();
            CargarLibros();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Agregar libro usando txtnombre = Título y txtdescripcion = Nombre de la categoría
            var titulo = txtnombre.Text.Trim();
            var nombreCategoria = txtdescripcion.Text.Trim();

            if (string.IsNullOrEmpty(titulo) || string.IsNullOrEmpty(nombreCategoria))
            {
                MessageBox.Show("Debe indicar título y categoría.");
                return;
            }

            // Buscar categoría existente o crear nueva
            var categorias = repo.Listar();
            var categoria = categorias.FirstOrDefault(c => string.Equals(c.Nombre, nombreCategoria, StringComparison.OrdinalIgnoreCase));
            if (categoria == null)
            {
                var nueva = new Modelos.Categoria { Nombre = nombreCategoria, Descricao = "" };
                repo.Insertar(nueva);
                categorias = repo.Listar();
                categoria = categorias.FirstOrDefault(c => string.Equals(c.Nombre, nombreCategoria, StringComparison.OrdinalIgnoreCase));
            }

            if (categoria == null)
            {
                MessageBox.Show("No se pudo obtener o crear la categoría.");
                return;
            }

            var libro = new Modelos.Libro
            {
                Titulo = titulo,
                Autor = string.Empty,
                Stock = 1,
                CategoriaId = categoria.Id
            };

            libroRepo.Insertar(libro);
            LimpiarCampos();
            CargarLibros();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Modificar libro seleccionado
            if (selectedId <= 0)
            {
                MessageBox.Show("Seleccione un libro de la lista para modificar.");
                return;
            }

            var titulo = txtnombre.Text.Trim();
            var nombreCategoria = txtdescripcion.Text.Trim();

            if (string.IsNullOrEmpty(titulo) || string.IsNullOrEmpty(nombreCategoria))
            {
                MessageBox.Show("Debe indicar título y categoría.");
                return;
            }

            var categorias = repo.Listar();
            var categoria = categorias.FirstOrDefault(c => string.Equals(c.Nombre, nombreCategoria, StringComparison.OrdinalIgnoreCase));
            if (categoria == null)
            {
                var nueva = new Modelos.Categoria { Nombre = nombreCategoria, Descricao = "" };
                repo.Insertar(nueva);
                categorias = repo.Listar();
                categoria = categorias.FirstOrDefault(c => string.Equals(c.Nombre, nombreCategoria, StringComparison.OrdinalIgnoreCase));
            }

            if (categoria == null)
            {
                MessageBox.Show("No se pudo obtener o crear la categoría.");
                return;
            }

            var libro = new Modelos.Libro
            {
                Id = selectedId,
                Titulo = titulo,
                Autor = string.Empty,
                Stock = 1,
                CategoriaId = categoria.Id
            };

            libroRepo.Editar(libro);
            LimpiarCampos();
            CargarLibros();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Eliminar libro seleccionado
            if (selectedId <= 0)
            {
                MessageBox.Show("Seleccione un libro de la lista para eliminar.");
                return;
            }

            var confirm = MessageBox.Show("¿Eliminar libro seleccionado?", "Confirmar", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                libroRepo.Eliminar(selectedId);
                LimpiarCampos();
                CargarLibros();
            }
        }

        private void CargarLibros()
        {
            var dt = libroRepo.Listar();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = dt;
            // Ajustes visuales: los nombres de columnas vienen desde la consulta en el repositorio
            dataGridView1.AutoResizeColumns();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.CellClick -= DataGridView1_CellClick;
            dataGridView1.CellClick += DataGridView1_CellClick;
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dataGridView1.Rows[e.RowIndex];
                // Cuando la fuente es un DataTable, DataBoundItem es DataRowView
                if (row.DataBoundItem is DataRowView drv)
                {
                    var r = drv.Row;
                    selectedId = Convert.ToInt32(r["Id"]);
                    txtnombre.Text = r["Titulo"].ToString();
                    txtdescripcion.Text = r["Categoria"].ToString();
                }
            }
        }

        private void LimpiarCampos()
        {
            selectedId = -1;
            txtnombre.Text = string.Empty;
            txtdescripcion.Text = string.Empty;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {



        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
