using BibliotecaAutomatizada.Formas;
using BibliotecaAutomatizada.Forms.Prestamos;
using BibliotecaAutomatizada.Forms.Multas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BibliotecaAutomatizada.Forms.Access
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        private void MenuForm_Load(object sender, EventArgs e)
        {
            
        }

        private void BtLibros_Click(object sender, EventArgs e)
        {
            LibrosForm form = new LibrosForm();
            form.ShowDialog();
        }

        private void BtPrestamos_Click(object sender, EventArgs e)
        {
            PrestamosForm form = new PrestamosForm();
            form.Show();
        }

        private void BtSalir_Click(object sender, EventArgs e)
        {
            this.Hide();

            LoginForm login = new LoginForm();
            login.Show();
        }

        private void BtMultas_Click(object sender, EventArgs e)
        {
            MultasForm form = new MultasForm();
            form.Show();
        }
    }
}
