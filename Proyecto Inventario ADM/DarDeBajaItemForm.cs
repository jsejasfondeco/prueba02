using System;
using System.Windows.Forms;

namespace ProyectoInventarioADM
{
    public partial class DarDeBajaItemForm : Form
    {
        private Inventario inventario;

        public DarDeBajaItemForm(Inventario inventario)
        {
            InitializeComponent();
            this.inventario = inventario;
        }

        private void DarDeBajaItemForm_Load(object sender, EventArgs e)
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

        private void btnDarDeBaja_Click(object sender, EventArgs e)
        {
            if (comboBoxItems.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccione un ítem.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMotivo.Text) || txtMotivo.Text == "Ingrese el motivo para dar de baja...")
            {
                MessageBox.Show("Por favor, ingrese el motivo para dar de baja el ítem.");
                return;
            }

            string nombreItem = comboBoxItems.SelectedItem.ToString();
            string motivo = txtMotivo.Text;

            inventario.EliminarItem(nombreItem, motivo);

            MessageBox.Show($"El ítem '{nombreItem}' ha sido dado de baja con el motivo: {motivo}");
            this.Close();
        }

        private void txtMotivo_Enter(object sender, EventArgs e)
        {
            if (txtMotivo.Text == "Ingrese el motivo para dar de baja...")
            {
                txtMotivo.Text = "";
                txtMotivo.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void txtMotivo_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMotivo.Text))
            {
                txtMotivo.Text = "Ingrese el motivo para dar de baja...";
                txtMotivo.ForeColor = System.Drawing.Color.Gray;
            }
        }
    }
}
