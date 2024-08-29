using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ProyectoInventarioADM
{
    public partial class ConsultarPorFechaForm : Form
    {
        private Inventario inventario;

        public ConsultarPorFechaForm(Inventario inventario)
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
                DateTime fechaInicio = dateTimePickerInicio.Value;
                DateTime fechaFin = dateTimePickerFin.Value;

                var itemsFiltrados = inventario.ConsultarItemPorFecha(nombreItem, fechaInicio, fechaFin);

                dataGridViewResultados.Rows.Clear();

                foreach (var item in itemsFiltrados)
                {
                    // Añadir fila con los valores correspondientes
                    dataGridViewResultados.Rows.Add(item.Nombre, item.Cantidad, item.Precio,
                        item.FechaRegistro, item.CantidadMesAnterior, item.CostoCompraUnitarioMesAnterior,
                        item.TotalBs, item.TotalProductoInventario, item.TotalBsCompraMesAnterior,
                        item.TotalConsumo, item.CompraMes, item.PromedioPonderado);
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un ítem.");
            }
        }
    }
}
