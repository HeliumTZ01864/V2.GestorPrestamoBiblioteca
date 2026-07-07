using BibliotecaAutomatizada.Forms;
using BibliotecaAutomatizada.Forms.Categoria;
using BibliotecaAutomatizada.Forms.Multas;
using BibliotecaAutomatizada.Forms.Usuarios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BibliotecaAutomatizada.Forms.Menus
{
    public partial class MenuAdminForm : Form
    {
        public MenuAdminForm()
        {
            InitializeComponent();
            // Opcional: Hacer que el formulario aparezca centrado al abrirse
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        /// <summary>
        /// Método mágico que limpia el panel derecho e incrusta cualquier formulario hijo
        /// </summary>
        private void AbrirFormInPanel(Form formHijo)
        {
            // 1. Si ya hay una pantalla abierta en el contenedor, la quitamos de la memoria
            if (this.pnlContenedor.Controls.Count > 0)
                this.pnlContenedor.Controls.RemoveAt(0);

            // 2. Le indicamos que no es una ventana independiente (TopLevel)
            formHijo.TopLevel = false;

            // 3. Le quitamos los bordes y los botones de cerrar/maximizar nativos
            formHijo.FormBorderStyle = FormBorderStyle.None;

            // 4. Hacemos que se estire por completo para ocupar todo el panel contenedor
            formHijo.Dock = DockStyle.Fill;

            // 5. Lo agregamos a la colección de controles del panel y lo mostramos
            this.pnlContenedor.Controls.Add(formHijo);
            this.pnlContenedor.Tag = formHijo;
            formHijo.Show();
        }
        // --- EVENTOS DE LOS BOTONES DEL SIDEBAR ---
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void pnlContenedor_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlSidebar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void MenuAdminForm_Load(object sender, EventArgs e)
        {

        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            AbrirFormInPanel(new UsuarioForm());
        }

        private void btnLibros_Click(object sender, EventArgs e)
        {
            AbrirFormInPanel(new LibrosForm());
        }

        private void btnCategorias_Click(object sender, EventArgs e)
        {
            AbrirFormInPanel(new CategoriaForm());
        }

        private void btnPrestamos_Click(object sender, EventArgs e)
        {
            
        }

        private void btnMultas_Click(object sender, EventArgs e)
        {
            AbrirFormInPanel(new MultasForm());
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro que desea cerrar sesión?", "Confirmación",
                                                     MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                this.Close(); // Cierra el menú de administrador

                // Busca el formulario de Login que dejamos oculto y lo vuelve a mostrar
                Form login = Application.OpenForms["LoginForm"];
                if (login != null)
                {
                    login.Show();
                }
            }
        }
    }
}
