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

namespace BibliotecaAutomatizada.Forms.Multas
{
    public partial class MultasForm : Form
    {
        private readonly MultaService multaService;
        private int multaIdSeleccionada = 0;
        public MultasForm()
        {
            InitializeComponent();
            multaService = new MultaService(new MultaRepository(), new MultaRegular());
        }
        private void MultasForm_Load(object sender, EventArgs e)
        {
            CargarMultas();
        }

        private void CargarMultas()
        {
            try
            {
                dgvMultas.DataSource = multaService.ObtenerMultas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar multas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvMultas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvMultas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow fila = dgvMultas.Rows[e.RowIndex];

            multaIdSeleccionada = Convert.ToInt32(fila.Cells["Id"].Value);
            txtPrestamoDetalleId.Text = fila.Cells["PrestamoDetalleId"].Value.ToString();
            txtMonto.Text = fila.Cells["Monto"].Value.ToString();
            chkPagada.Checked = fila.Cells["Pagada"].Value.ToString() == "Sí";
        }

        private void BtRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtPrestamoDetalleId.Text.Trim(), out int prestamoDetalleId))
                    throw new Exception("El ID de Préstamo Detalle debe ser un número entero.");

                if (!decimal.TryParse(txtMonto.Text.Trim(), out decimal monto))
                    throw new Exception("El monto debe ser un valor numérico.");

                Multa multa = new Multa
                {
                    PrestamoDetalleId = prestamoDetalleId,
                    Monto = monto,
                    Pagada = chkPagada.Checked
                };

                multaService.RegistrarMulta(multa);
                MessageBox.Show("Multa registrada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCampos();
                CargarMultas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkPagada_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void BtLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }
        private void LimpiarCampos()
        {
            multaIdSeleccionada = 0;
            txtPrestamoDetalleId.Clear();
            txtMonto.Clear();
            chkPagada.Checked = false;
        }

        private void BtEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (multaIdSeleccionada <= 0)
                    throw new Exception("Seleccione una multa de la tabla para eliminarla.");

                DialogResult confirm = MessageBox.Show(
                    "¿Está seguro que desea eliminar esta multa?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    multaService.EliminarMulta(multaIdSeleccionada);
                    MessageBox.Show("Multa eliminada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                    CargarMultas();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtPagar_Click(object sender, EventArgs e)
        {
            try
            {
                if (multaIdSeleccionada <= 0)
                    throw new Exception("Seleccione una multa de la tabla para marcarla como pagada.");

                DialogResult confirm = MessageBox.Show(
                    "¿Desea marcar esta multa como pagada?",
                    "Confirmar pago",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    multaService.PagarMulta(multaIdSeleccionada);
                    MessageBox.Show("Multa marcada como pagada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                    CargarMultas();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
