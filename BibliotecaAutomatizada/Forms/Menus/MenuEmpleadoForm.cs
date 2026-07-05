using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BibliotecaAutomatizada.Forms.Prestamos;

namespace BibliotecaAutomatizada.Forms.Menus
{
    public partial class MenuEmpleadoForm : Form
    {
        public MenuEmpleadoForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen; // Centra el menú al iniciar
        }

        /// <summary>
        /// Método que limpia el panel derecho del empleado e incrusta el formulario seleccionado
        /// </summary>
        private void AbrirFormInPanel(Form formHijo)
        {
            // 1. Limpiamos el contenedor del empleado por si hay otra pantalla visible
            if (this.pnlContenedorEmpleado.Controls.Count > 0)
                this.pnlContenedorEmpleado.Controls.RemoveAt(0);

            // 2. Configuraciones para incrustar de forma limpia sin bordes
            formHijo.TopLevel = false;
            formHijo.FormBorderStyle = FormBorderStyle.None;
            formHijo.Dock = DockStyle.Fill;

            // 3. Lo añadimos al panel del empleado y lo mostramos
            this.pnlContenedorEmpleado.Controls.Add(formHijo);
            this.pnlContenedorEmpleado.Tag = formHijo;
            formHijo.Show();
        }

        // --- EVENTOS GENERADOS POR DOBLE CLIC EN LOS BOTONES ---

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void MenuEmpleadoForm_Load(object sender, EventArgs e)
        {

        }

        private void pnlSidebarEmpleado_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnNuevaReserva_Click(object sender, EventArgs e)
        {
            // Abrimos directamente el formulario de préstamos que creamos y reparamos
            AbrirFormInPanel(new PrestamosForm());
        }

        private void btnVerLibros_Click(object sender, EventArgs e)
        {
            // Aquí puedes reutilizar tu 'LibrosForm' existente. 
            // Tip Pro: Si quieres, puedes pasarle un parámetro para indicarle que el empleado 
            // no pueda usar los botones de agregar, editar o borrar (Solo Lectura).
            // AbrirFormInPanel(new LibrosForm());
        }

        private void btnCobrarMultas_Click(object sender, EventArgs e)
        {
            // Módulo operativo para procesar los pagos cuando los usuarios devuelven libros tarde
            // AbrirFormInPanel(new CobroMultasForm())
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro que desea cerrar sesión?", "Confirmación",
                                                     MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                this.Close(); // Cierra el menú operativo de empleados

                // Retorna al formulario de Login guardado en la memoria de la aplicación
                Form login = Application.OpenForms["LoginForm"];
                if (login != null)
                {
                    login.Show();
                }
            }
        }
    }
}
