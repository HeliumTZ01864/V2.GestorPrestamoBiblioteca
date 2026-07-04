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
        private int selectedId = -1;

        public CategoriaForm()
        {
            InitializeComponent();
            CargarCategorias();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Agregar
            var cat = new Modelos.Categoria
            {
                Nombre = textBox1.Text.Trim(),
                Descricao = textBox2.Text.Trim()
            };

            repo.Insertar(cat);
            LimpiarCampos();
            CargarCategorias();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Modificar
            if (selectedId <= 0)
            {
                MessageBox.Show("Seleccione una categoría de la lista para modificar.");
                return;
            }

            var cat = new Modelos.Categoria
            {
                Id = selectedId,
                Nombre = textBox1.Text.Trim(),
                Descricao = textBox2.Text.Trim()
            };

            repo.Modificar(cat);
            LimpiarCampos();
            CargarCategorias();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Eliminar
            if (selectedId <= 0)
            {
                MessageBox.Show("Seleccione una categoría de la lista para eliminar.");
                return;
            }

            var confirm = MessageBox.Show("¿Eliminar categoría seleccionada?", "Confirmar", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                repo.Eliminar(selectedId);
                LimpiarCampos();
                CargarCategorias();
            }
        }

        private void CargarCategorias()
        {
            var lista = repo.Listar();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = lista;
            dataGridView1.Columns["Descricao"].HeaderText = "Descripción";
            dataGridView1.Columns["Nombre"].HeaderText = "Nombre";
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
                    textBox1.Text = cat.Nombre;
                    textBox2.Text = cat.Descricao;
                }
            }
        }

        private void LimpiarCampos()
        {
            selectedId = -1;
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
        }
    }
}
