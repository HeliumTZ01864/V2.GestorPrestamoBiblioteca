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

namespace BibliotecaAutomatizada.Forms.Access
{
    public partial class RegistroForm : Form
    {
        private UsuarioService servicio;

        public RegistroForm()
        {
            InitializeComponent();

            IUsuarioRepository repo = new UsuarioRepository();
            servicio = new UsuarioService(repo);
        }

        private async void btRegistro_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
               string.IsNullOrWhiteSpace(txtCorreo.Text) ||
               string.IsNullOrWhiteSpace(txtContra.Text))
            {
                MessageBox.Show(
                    "Complete todos los campos",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }


            Usuario usuario = new Usuario
            {
                Nombre = txtNombre.Text,
                Correo = txtCorreo.Text,
                Contrasena = txtContra.Text,

                // Siempre será cliente
                Rol = TipoRol.Cliente
            };


            bool registrado = await servicio.RegistrarUsuario(usuario);


            if (registrado)
            {
                MessageBox.Show(
                    "Registro exitoso, ya puede iniciar sesión",
                    "Correcto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);


                this.Close();
            }
            else
            {
                MessageBox.Show(
                    "No se pudo registrar el usuario");
            }
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtContra_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCorreo_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
