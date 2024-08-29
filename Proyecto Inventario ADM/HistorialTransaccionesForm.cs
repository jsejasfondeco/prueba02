using System;
using System.Windows.Forms;

namespace ProyectoInventarioADM
{
    public partial class HistorialTransaccionesForm : Form
    {
        private Inventario inventario;

        public HistorialTransaccionesForm(Inventario inventario)
        {
            InitializeComponent();
            this.inventario = inventario;
            CargarTransacciones();
            CargarFiltroTipos();
            CargarFiltroItems();
        }

        private void CargarTransacciones(string tipoFiltro = "Todos", string itemFiltro = "Todos")
        {
            listViewTransacciones.Items.Clear();
            foreach (var transaccion in inventario.ObtenerTransacciones())
            {
                bool tipoCoincide = (tipoFiltro == "Todos" || transaccion.Tipo.ToString() == tipoFiltro);
                bool itemCoincide = (itemFiltro == "Todos" || transaccion.NombreItem == itemFiltro);

                if (tipoCoincide && itemCoincide)
                {
                    ListViewItem item = new ListViewItem(transaccion.NombreItem);
                    item.SubItems.Add(transaccion.Cantidad.ToString());
                    item.SubItems.Add(transaccion.PrecioUnitario.ToString("C"));
                    item.SubItems.Add(transaccion.Tipo.ToString());
                    item.SubItems.Add(transaccion.Fecha.ToString());

                    // Añadir el motivo si la transacción es de tipo Baja
                    if (transaccion.Tipo == TipoTransaccion.Baja)
                    {
                        item.SubItems.Add(transaccion.Motivo ?? "Sin motivo"); // Añadir el motivo a la lista
                    }
                    else
                    {
                        item.SubItems.Add(transaccion.FechaRegistroItem.ToString("dd/MM/yyyy"));
                    }

                    listViewTransacciones.Items.Add(item);
                }
            }
        }

        private void CargarFiltroTipos()
        {
            comboBoxFiltroTipo.Items.Clear();
            comboBoxFiltroTipo.Items.Add("Todos");
            comboBoxFiltroTipo.Items.Add(TipoTransaccion.Registro.ToString());
            comboBoxFiltroTipo.Items.Add(TipoTransaccion.Compra.ToString());
            comboBoxFiltroTipo.Items.Add(TipoTransaccion.Consumo.ToString());
            comboBoxFiltroTipo.Items.Add(TipoTransaccion.Baja.ToString());  // Añadir Baja al filtro
            comboBoxFiltroTipo.SelectedIndex = 0; // Seleccionar "Todos" por defecto
        }

        private void CargarFiltroItems()
        {
            comboBoxFiltroItem.Items.Clear();
            comboBoxFiltroItem.Items.Add("Todos");
            foreach (var item in inventario.ObtenerItems())
            {
                comboBoxFiltroItem.Items.Add(item.Nombre);
            }
            comboBoxFiltroItem.SelectedIndex = 0; // Seleccionar "Todos" por defecto
        }

        private void comboBoxFiltroTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxFiltroItem.SelectedItem != null && comboBoxFiltroTipo.SelectedItem != null)
            {
                CargarTransacciones(comboBoxFiltroTipo.SelectedItem.ToString(), comboBoxFiltroItem.SelectedItem.ToString());
            }
        }

        private void comboBoxFiltroItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxFiltroItem.SelectedItem != null && comboBoxFiltroTipo.SelectedItem != null)
            {
                CargarTransacciones(comboBoxFiltroTipo.SelectedItem.ToString(), comboBoxFiltroItem.SelectedItem.ToString());
            }
        }

        private void listViewTransacciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewTransacciones.SelectedItems.Count > 0)
            {
                var selectedTransaction = listViewTransacciones.SelectedItems[0];
                var tipo = selectedTransaction.SubItems[3].Text;

                if (tipo == TipoTransaccion.Baja.ToString())
                {
                    panelDetalles.Visible = true;
                    lblMotivoBaja.Text = $"Motivo de Baja: {selectedTransaction.SubItems[5].Text}";
                }
                else
                {
                    panelDetalles.Visible = false;
                }
            }
            else
            {
                panelDetalles.Visible = false;
            }
        }
    }
}
