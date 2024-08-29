using System;
using System.Windows.Forms;

namespace ProyectoInventarioADM
{
    public partial class RegistrarSaldoAnteriorForm : Form
    {
        private readonly Inventario _inventario;

        public RegistrarSaldoAnteriorForm(Inventario inventario)
        {
            InitializeComponent();
            _inventario = inventario;
        }

        private void RegistrarSaldoAnteriorForm_Load(object sender, EventArgs e)
        {
            CargarItems();

            // Configurar NumericUpDown para manejar precios en el formulario de saldo anterior
            numCostoAnterior.DecimalPlaces = 2;
            numCostoAnterior.Increment = 0.1M;
            numCostoAnterior.Maximum = 1000000M;
            numCostoAnterior.Minimum = 0.01M;
        }

        private void CargarItems()
        {
            cmbNombreItem.Items.Clear();
            var items = _inventario.ObtenerItems();
            foreach (var item in items)
            {
                cmbNombreItem.Items.Add(item.Nombre);
            }
        }

        public void ActualizarComboBox()
        {
            CargarItems();
        }

        private void NumCostoAnterior_ValueChanged(object sender, EventArgs e)
        {
            CalcularTotalBsAnterior();
        }

        private void CalcularTotalBsAnterior()
        {
            decimal saldoAnterior = numSaldoAnterior.Value;
            decimal costoAnterior = numCostoAnterior.Value;
            lblTotalBsAnterior.Text = $"Total Bs Anterior: {saldoAnterior * costoAnterior}";
        }

        private void BtnRegistrar_Click(object sender, EventArgs e)
        {
            if (cmbNombreItem.SelectedItem != null)
            {
                string nombreItem = cmbNombreItem.SelectedItem.ToString();
                int saldoAnterior = (int)numSaldoAnterior.Value;
                decimal costoAnterior = numCostoAnterior.Value;
                DateTime fechaRegistro = dtpFechaRegistro.Value;

                try
                {
                    _inventario.GuardarSaldoAnterior(nombreItem, saldoAnterior, costoAnterior, fechaRegistro);
                    MessageBox.Show("Saldo anterior registrado correctamente.");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al registrar el saldo anterior: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un ítem.");
            }
        }
    }
}

