using BibliotecaAutomatizada.Interfaces;
using BibliotecaAutomatizada.Modelos;
using BibliotecaAutomatizada.Respositorios;
using BibliotecaAutomatizada.Servicios;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BibliotecaAutomatizada.Forms.Usuarios
{
    public partial class UsuarioForm : Form
    {
        private UsuarioService servicio;
        private int idUsuarioSeleccionado = 0;

        public UsuarioForm()
        {
            InitializeComponent();

            this.Load += UsuarioForm_Load;

            IUsuarioRepository repo = new UsuarioRepository();
            servicio = new UsuarioService(repo);

            
        }


        // Cuando abre el formulario
        private async void UsuarioForm_Load(object sender, EventArgs e)
        {
            await CargarUsuarios();
        }


        // Cargar datos al DataGridView
        private async Task CargarUsuarios()
        {
            var usuarios = await servicio.ObtenerUsuariosAsync();

            

            dataGridView1.DataSource = usuarios;

        }


        // BOTÓN AGREGAR
        private async void btAgregar_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario
            {
                Nombre = txtNombre.Text,
                Correo = txtCorreo.Text,
                Contrasena = txtContra.Text,
                
            };


            bool resultado = await servicio.RegistrarUsuario(usuario);


            if (resultado)
            {
                MessageBox.Show("Usuario registrado correctamente");

                await CargarUsuarios();

                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("No se pudo registrar el usuario");
            }
        }


        // BOTÓN MODIFICAR
        private async void btMod_Click(object sender, EventArgs e)
        {
            if (idUsuarioSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un usuario primero");
                return;
            }


            Usuario usuario = new Usuario
            {
                Id = idUsuarioSeleccionado,
                Nombre = txtNombre.Text,
                Correo = txtCorreo.Text,
                Contrasena = txtContra.Text,
                
            };


            servicio.EditarUsuario(usuario);


            MessageBox.Show("Usuario modificado correctamente");


            await CargarUsuarios();

            LimpiarCampos();
        }



        // BOTÓN ELIMINAR
        private async void btEliminar_Click(object sender, EventArgs e)
        {
            if (idUsuarioSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un usuario");
                return;
            }


            DialogResult respuesta = MessageBox.Show(
                "¿Desea eliminar este usuario?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);


            if (respuesta == DialogResult.Yes)
            {
                servicio.EliminarUsuario(idUsuarioSeleccionado);


                MessageBox.Show("Usuario eliminado");


                await CargarUsuarios();

                LimpiarCampos();
            }
        }



        // Cuando seleccionas una fila
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dataGridView1.Rows[e.RowIndex];


                idUsuarioSeleccionado =
                    Convert.ToInt32(fila.Cells["Id"].Value);


                txtNombre.Text =
                    fila.Cells["Nombre"].Value.ToString();


                txtCorreo.Text =
                    fila.Cells["Correo"].Value.ToString();


                


                // No mostramos la contraseña
                txtContra.Text = "";
            }
        }



        private void LimpiarCampos()
        {
            idUsuarioSeleccionado = 0;

            txtNombre.Clear();
            txtCorreo.Clear();
            txtContra.Clear();

        
        }

        private void cmbRol_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}