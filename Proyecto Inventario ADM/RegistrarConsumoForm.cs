using System;
using System.Windows.Forms;

namespace ProyectoInventarioADM
{
    public partial class RegistrarConsumoForm : Form
    {
        private Inventario inventario;

        public RegistrarConsumoForm(Inventario inventario)
        {
            InitializeComponent();
            this.inventario = inventario;
        }

        private void RegistrarConsumoForm_Load(object sender, EventArgs e)
        {
            CargarItems();
        }

        private void CargarItems()
        {
            comboBoxItems.Items.Clear();
            var items = inventario.ObtenerItems();
            foreach (var item in items)
            {
                comboBoxItems.Items.Add(item.Nombre);
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (comboBoxItems.SelectedItem != null)
            {
                string nombreItem = comboBoxItems.SelectedItem.ToString();
                int cantidad = (int)numCantidad.Value;
                DateTime fechaSeleccionada = dateTimePickerFecha.Value;

                inventario.RegistrarConsumo(nombreItem, cantidad, fechaSeleccionada);

                MessageBox.Show("Consumo registrado correctamente.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un ítem.");
            }
        }
    }
}
