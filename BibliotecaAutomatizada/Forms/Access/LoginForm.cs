using BibliotecaAutomatizada.Forms.Access;
using BibliotecaAutomatizada.Forms.Menus;
using BibliotecaAutomatizada.Interfaces;
using BibliotecaAutomatizada.Modelos;
using BibliotecaAutomatizada.Respositorios;
using BibliotecaAutomatizada.Servicios;
using System;
using System.Security.Cryptography;
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
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private async void BtIngresar_Click(object sender, EventArgs e)
        {
            string correoIngresado = TBUsuario.Text.Trim().ToLower(); // Pasamos a minúsculas para comparar fácil
            string passwordIngresado = TBContra.Text;

            if (string.IsNullOrEmpty(correoIngresado) || string.IsNullOrEmpty(passwordIngresado))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Campos Vacíos",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ==========================================
            // 🔥 MODO DE PRUEBA / BYPASS POR DEFAULT
            // ==========================================
            if (correoIngresado == "admin@biblioteca.com" && passwordIngresado == "123")
            {
                MessageBox.Show("¡Acceso de prueba concedido como: Administrador!", "Modo Desarrollo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MenuAdminForm frmAdmin = new MenuAdminForm();
                frmAdmin.Show();
                this.Hide();
                return; // Corta la ejecución aquí para no ir a la BD
            }
            else if (correoIngresado == "empleado@biblioteca.com" && passwordIngresado == "123")
            {
                MessageBox.Show("¡Acceso de prueba concedido como: Empleado!", "Modo Desarrollo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MenuEmpleadoForm frmEmpleado = new MenuEmpleadoForm();
                frmEmpleado.Show();
                this.Hide();
                return; // Corta la ejecución aquí para no ir a la BD
            }
            // ==========================================

            // SI NO ES NINGUNO DE LOS USUARIOS DE PRUEBA, BUSCA NORMAL EN LA BASE DE DATOS:
            string passwordCifrada = EncriptarSHA256(passwordIngresado);

            try
            {
                IUsuarioRepository repo = new UsuarioRepository();
                Usuario usuarioValido = await repo.LoginAsync(correoIngresado, passwordCifrada);

                if (usuarioValido != null)
                {
                    MessageBox.Show($"¡Bienvenido {usuarioValido.Nombre}!\nAcceso concedido como: {usuarioValido.Rol}",
                                    "Inicio de Sesión Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Evaluación del rol real (Enum de la Base de Datos convertido a String)
                    if (usuarioValido.Rol.ToString().ToUpper() == "ADMIN" ||
                        usuarioValido.Rol.ToString().ToUpper() == "ADMINISTRADOR")
                    {
                        MenuAdminForm frmAdmin = new MenuAdminForm();
                        frmAdmin.Show();
                    }
                    else
                    {
                        MenuEmpleadoForm frmEmpleado = new MenuEmpleadoForm();
                        frmEmpleado.Show();
                    }

                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Credenciales incorrectas. Verifique su correo y contraseña.", "Error de Acceso",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error crítico al conectar con el servidor: " + ex.Message, "Error de Conexión",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string EncriptarSHA256(string texto)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(texto));
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    sb.Append(bytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
        }
    }
}