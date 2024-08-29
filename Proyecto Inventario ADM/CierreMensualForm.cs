using System;
using System.Windows.Forms;

namespace ProyectoInventarioADM
{
    public partial class CierreMensualForm : Form
    {
        private Inventario _inventario;

        public CierreMensualForm(Inventario inventario)
        {
            InitializeComponent();
            _inventario = inventario;
            CargarItems();
        }

        private void CargarItems()
        {
            foreach (var item in _inventario.ObtenerItems())
            {
                // Agrega filas al DataGridView con el estado "Pendiente"
                dataGridViewItems.Rows.Add(item.Nombre, "Pendiente");
            }
        }

        private void btnEjecutarProceso_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridViewItems.Rows)
            {
                string itemName = row.Cells[0].Value.ToString();
                // Aquí procesarías el cierre de mes para el ítem
                row.Cells[1].Value = "Procesado"; // Actualiza el estado a "Procesado"
            }

            MessageBox.Show("Cierre de Mes completado.");
        }

        private void lblSubtitulo_Click(object sender, EventArgs e)
        {

        }
    }
}
