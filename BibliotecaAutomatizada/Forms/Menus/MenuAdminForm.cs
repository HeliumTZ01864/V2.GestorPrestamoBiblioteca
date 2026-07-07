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
            // Cuando tengamos listo el CRUD de usuarios descomentas la línea de abajo:
            // AbrirFormInPanel(new UsuariosForm());
        }

        private void btnLibros_Click(object sender, EventArgs e)
        {
            // Instancia tu formulario de libros existente (ajusta el nombre si varía)
            // AbrirFormInPanel(new LibrosForm());
        }

        private void btnCategorias_Click(object sender, EventArgs e)
        {
            // AbrirFormInPanel(new CategoriasForm());
        }

        private void btnPrestamos_Click(object sender, EventArgs e)
        {
            // Carga el visor general de auditoría de préstamos para el Admin
            // AbrirFormInPanel(new HistorialPrestamosForm())
        }

        private void btnMultas_Click(object sender, EventArgs e)
        {
            // Carga la configuración global de costos por mora
            // AbrirFormInPanel(new ConfigMultasForm());
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
