using BibliotecaAutomatizada.Forms.Access;
using BibliotecaAutomatizada.Forms.Menus;
using BibliotecaAutomatizada.Interfaces;
using BibliotecaAutomatizada.Modelos;
using BibliotecaAutomatizada.Respositorios;
using BibliotecaAutomatizada.Servicios;
using System;
using System.Windows.Forms;
using BibliotecaAutomatizada.Sesion;
namespace BibliotecaAutomatizada.Forms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private async void BtIngresar_Click(object sender, EventArgs e)
        {
            string correoIngresado = TBUsuario.Text.Trim().ToLower();
            string passwordIngresado = TBContra.Text;

            if (string.IsNullOrWhiteSpace(correoIngresado) ||
                string.IsNullOrWhiteSpace(passwordIngresado))
            {
                MessageBox.Show("Por favor, complete todos los campos.",
                    "Campos Vacíos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                IUsuarioRepository repo = new UsuarioRepository();
                UsuarioService servicio = new UsuarioService(repo);

                // Se envía la contraseña sin encriptar.
                // El repositorio se encarga de encriptarla.
                Usuario usuarioValido = await servicio.IniciarSesionAsync(
                    correoIngresado,
                    passwordIngresado);

                if (usuarioValido != null)
                {

                    UsuarioSesion.UsuarioActual = usuarioValido;
                    MessageBox.Show(
                        $"¡Bienvenido {usuarioValido.Nombre}!\nAcceso concedido como: {usuarioValido.Rol}",
                        "Inicio de Sesión Exitoso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    if (usuarioValido.Rol == TipoRol.Admin)
                    {
                        MenuAdminForm frmAdmin = new MenuAdminForm();
                        frmAdmin.Show();
                    }
                    else
                    {
                        MenuClienteForm frmEmpleado = new MenuClienteForm();
                        frmEmpleado.Show();
                    }

                    this.Hide();
                }
                else
                {
                    MessageBox.Show(
                        "Correo o contraseña incorrectos.",
                        "Error de Acceso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al conectar con la base de datos.\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void TBUsuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void TBContra_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnRegistro_Click(object sender, EventArgs e)
        {
            RegistroForm frm = new RegistroForm();

            frm.ShowDialog();
        }
    }
}