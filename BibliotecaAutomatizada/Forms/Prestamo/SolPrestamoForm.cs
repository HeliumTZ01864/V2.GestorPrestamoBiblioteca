using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BibliotecaAutomatizada.Servicios;
using PrestamoModelo = BibliotecaAutomatizada.Modelos.Prestamo;
using BibliotecaAutomatizada.Modelos;
using BibliotecaAutomatizada.Interfaces;
using BibliotecaAutomatizada.Respositorios;
using BibliotecaAutomatizada.Sesion;

namespace BibliotecaAutomatizada.Forms.Prestamo
{
    public partial class SolPrestamoForm : Form
    {
        private PrestamoService prestamoService;

        private List<PrestamoDetalle> detalles =
            new List<PrestamoDetalle>();


        public SolPrestamoForm()
        {
            InitializeComponent();


            IPrestamoRepository repo =
                new PrestamoRepository();


            prestamoService =
                new PrestamoService(repo);
        }



        private void SolPrestamoForm_Load(object sender, EventArgs e)
        {
            MostrarUsuario();
            CargarLibros();
        }



        private void MostrarUsuario()
        {
            if (UsuarioSesion.UsuarioActual != null)
            {
                txtUsuario.Text =
                    UsuarioSesion.UsuarioActual.Nombre;
            }
        }
        private void CargarLibros()
        {
            ILibroRepository repo =
                new LibroRepository();


            DataTable dt =
                repo.Listar();


            dgvLibro.DataSource = dt;
        }

        private void btAgLibro_Click(object sender, EventArgs e)
        {
            if (dgvLibro.CurrentRow == null)
            {
                MessageBox.Show(
                    "Seleccione un libro");
                return;
            }


            int libroId =
            Convert.ToInt32(
            dgvLibro.CurrentRow.Cells["Id"].Value);



            PrestamoDetalle detalle =
                new PrestamoDetalle();


            detalle.LibroId = libroId;


            detalles.Add(detalle);



            dgvDetalle.DataSource = null;

            dgvDetalle.DataSource = detalles;
        }

        private void dgvDetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void button1_Click(object sender, EventArgs e)
        {
            try
            {
                PrestamoModelo prestamo = new PrestamoModelo();


                prestamo.UsuarioId =
                    UsuarioSesion.UsuarioActual.Id;


                prestamo.Detalles =
                    detalles;



                prestamoService.RegistrarPrestamo(prestamo);



                MessageBox.Show(
                    "Solicitud enviada correctamente",
                    "Préstamo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);



                detalles.Clear();


                dgvDetalle.DataSource = null;

            }
            catch (Exception ex)
            {

                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

            }

        }

    }

}
