using System;
using System.Windows.Forms;

namespace ProyectoInventarioADM
{
    public partial class ConsultarItemForm : Form
    {
        private Inventario inventario;

        public ConsultarItemForm(Inventario inventario)
        {
            InitializeComponent();
            this.inventario = inventario;
            CargarItems();
        }

        private void CargarItems()
        {
            comboBoxItems.Items.Clear();

            var items = inventario.ObtenerItems();
            foreach (var item in items)
            {
                if (!comboBoxItems.Items.Contains(item.Nombre))
                {
                    comboBoxItems.Items.Add(item.Nombre);
                }
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (comboBoxItems.SelectedItem != null)
            {
                string nombreItem = comboBoxItems.SelectedItem.ToString();
                Item item = inventario.ConsultarItem(nombreItem);

                if (item != null)
                {
                    dataGridViewDetalles.Rows.Clear();

                    // Añadir fila con los valores correspondientes
                    dataGridViewDetalles.Rows.Add(
                        item.TotalConsumo,  // Total de consumo hasta la fecha
                        item.CantidadMesAnterior,  // Cantidad del mes anterior
                        item.CostoCompraUnitarioMesAnterior,  // Precio unitario del mes anterior
                        item.TotalBsCompraMesAnterior,  // Total Bs mes anterior
                        item.CompraMes,  // Compra del mes actual
                        item.Precio,  // Precio de compra del mes actual
                        item.CompraMes * item.Precio,  // Total Bs compra del mes actual
                        item.TotalProductoInventario,  // Total producto en inventario (cantidad mes anterior + compra mes)
                        item.Cantidad,  // Cantidad actual en inventario
                        item.PromedioPonderado,  // Precio unitario ponderado
                        item.TotalBs  // Total Bs (Cantidad * Precio Unitario)
                    );
                }
                else
                {
                    MessageBox.Show("Item no encontrado.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un ítem.");
            }
        }

        private void btnConsultarPorFecha_Click(object sender, EventArgs e)
        {
            if (comboBoxItems.SelectedItem != null)
            {
                string nombreItem = comboBoxItems.SelectedItem.ToString();
                ConsultarPorFechaForm form = new ConsultarPorFechaForm(inventario);
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un ítem.");
            }
        }
    }
}
