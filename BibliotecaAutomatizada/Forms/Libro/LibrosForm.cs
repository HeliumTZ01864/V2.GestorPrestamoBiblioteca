
using BibliotecaAutomatizada.Modelos;
using BibliotecaAutomatizada.Respositorios;
using BibliotecaAutomatizada.Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BibliotecaAutomatizada.Forms
{
    public partial class LibrosForm : Form
    {
        private int idSeleccionado = 0;
        private LibroService service;
        public LibrosForm()
        {
            InitializeComponent();
            service = new LibroService(new LibroRepository());
        }
        private void CargarLibros()
        {
            LibroService service = new LibroService(new LibroRepository());

            dgvLibros.DataSource = service.ObtenerLibros();
        }

        private void CargarCategorias()
        {
            CategoriaRepository repo = new CategoriaRepository();
            var categorias = repo.Listar();

            cmbCategoria.DataSource = categorias;
            cmbCategoria.DisplayMember = "Nombre";
            cmbCategoria.ValueMember = "Id";
        }
        private void LibrosForm_Load(object sender, EventArgs e)
        {
            CargarCategorias();
            CargarLibros();
        }

        private void BtAgregar_Click(object sender, EventArgs e)
        {
            Libro libro = new Libro()
            {
                Titulo = txtTitulo.Text,
                Autor = txtAutor.Text,
                Stock = (int)numStock.Value,
                CategoriaId = (int)cmbCategoria.SelectedValue
            };

            
            service.RegistrarLibro(libro);

            MessageBox.Show("Libro agregado");

            CargarLibros();
        }

        private void BtModificar_Click(object sender, EventArgs e)
        {
            Libro libro = new Libro()
            {
                Id = idSeleccionado,
                Titulo = txtTitulo.Text,
                Autor = txtAutor.Text,
                Stock = (int)numStock.Value,
                CategoriaId = (int)cmbCategoria.SelectedValue
            };

            
            service.EditarLibro(libro);

            MessageBox.Show("Libro modificado");

            CargarLibros();
        }

  

        private void BtEliminar_Click(object sender, EventArgs e)
        {
            
            service.EliminarLibro(idSeleccionado);

            MessageBox.Show("Libro eliminado");

            CargarLibros();
        }

        
        private void dgvLibros_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            idSeleccionado = Convert.ToInt32(dgvLibros.CurrentRow.Cells["Id"].Value);

            txtTitulo.Text = dgvLibros.CurrentRow.Cells["Titulo"].Value.ToString();
            txtAutor.Text = dgvLibros.CurrentRow.Cells["Autor"].Value.ToString();
            numStock.Value = Convert.ToInt32(dgvLibros.CurrentRow.Cells["Stock"].Value);
            cmbCategoria.SelectedValue = dgvLibros.CurrentRow.Cells["CategoriaId"].Value;

            dgvLibros.Columns["CategoriaId"].Visible = false;
        }
    }
}
