using BibliotecaAutomatizada.Modelos;
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
        private Servicios.CategoriaService service = new Servicios.CategoriaService(new Respositorios.CategoriaRepository());
        private int selectedId = -1; // id de la categoría seleccionada

        public CategoriaForm()
        {
            InitializeComponent();
            CargarCategorias();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Agregar categoría con descripción
            var nombre = txtcategoria.Text.Trim();
            var descripcion = txtdescripcion.Text.Trim();

            if (string.IsNullOrEmpty(nombre))
            {
                MessageBox.Show("Indique la categoría.");
                return;
            }

            var cat = new Modelos.Categoria { Nombre = nombre, Descricao = descripcion };
            service.RegistrarCategoria(cat);
            LimpiarCampos();
            CargarCategorias();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Eliminar categoría seleccionada
            if (selectedId <= 0)
            {
                MessageBox.Show("Seleccione una categoría de la lista para eliminar.");
                return;
            }

            var confirm = MessageBox.Show("¿Eliminar categoría seleccionada?", "Confirmar", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                service.EliminarCategoria(selectedId);
                LimpiarCampos();
                CargarCategorias();
            }
        }

        private void CargarCategorias()
        {
            var lista = service.ObtenerCategorias();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = lista;
            // Mostrar solo Nombre y Descricao
            if (dataGridView1.Columns.Contains("Id")) dataGridView1.Columns["Id"].Visible = false;
            if (dataGridView1.Columns.Contains("Nombre")) dataGridView1.Columns["Nombre"].HeaderText = "Categoría";
            if (dataGridView1.Columns.Contains("Descricao")) dataGridView1.Columns["Descricao"].HeaderText = "Descripción";
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
                if (row.DataBoundItem is Modelos.Categoria cat)
                {
                    selectedId = cat.Id;
                    txtcategoria.Text = cat.Nombre;
                    txtdescripcion.Text = cat.Descricao;
                }
            }
        }

        private void LimpiarCampos()
        {
            selectedId = -1;
            txtcategoria.Text = string.Empty;
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

        private void btnbodificar_Click(object sender, EventArgs e)
        {
            
            if (selectedId <= 0)
            {
                MessageBox.Show("Seleccione una categoría para modificar.");
                return;
            }

            var nombre = txtcategoria.Text.Trim();
            var descripcion = txtdescripcion.Text.Trim();

            if (string.IsNullOrEmpty(nombre))
            {
                MessageBox.Show("Indique la categoría.");
                return;
            }

            var cat = new Modelos.Categoria { Id = selectedId, Nombre = nombre, Descricao = descripcion };
            service.EditarCategoria(cat);
            LimpiarCampos();
            CargarCategorias();
        }
    }
}
