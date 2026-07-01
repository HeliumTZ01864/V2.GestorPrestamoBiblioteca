using BibliotecaAutomatizada.Forms.Access;
using BibliotecaAutomatizada.Interfaces;
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

namespace BibliotecaAutomatizada.Formas
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void BtIngresar_Click(object sender, EventArgs e)
        {
            IUsuarioRepository repo = new UsuarioRepository();
            UsuarioService service = new UsuarioService(repo);

            Usuario usuario = service.IniciarSesion(TBUsuario.Text, TBContra.Text);

            if (usuario != null)
            {
                MessageBox.Show("Bienvenido " + usuario.Nombre);

                MenuForm menu = new MenuForm();
                menu.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Credenciales incorrectas");
            }
        }
    }
}
