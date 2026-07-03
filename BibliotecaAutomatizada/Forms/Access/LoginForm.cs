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

        private async void BtIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                IUsuarioRepository repo = new UsuarioRepository();

                // Limpiamos espacios y pasamos a minúsculas para evitar errores de tipeo
                string correoIngresado = TBUsuario.Text.Trim().ToLower();
                string passwordIngresado = TBContra.Text;

                // Llamamos a la versión asíncrona para que la interfaz gráfica no se congele
                // LLAMADA AL REPOSITORIO
                Usuario usuario = await repo.LoginAsync(correoIngresado, passwordIngresado);

                // MODIFICACIÓN TEMPORAL: Cambia 'if (usuario != null)' por esto:
                if (usuario != null || correoIngresado == "admin@biblioteca.com")
                {
                    MessageBox.Show("¡Bienvenido Administrador! (Modo de prueba)", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    MenuForm menu = new MenuForm();
                    menu.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Credenciales incorrectas. Verifique su correo y contraseña.", "Error de Acceso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error crítico al conectar con el servidor: " + ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
