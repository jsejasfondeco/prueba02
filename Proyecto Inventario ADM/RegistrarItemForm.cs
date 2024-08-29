using System;
using System.Windows.Forms;

namespace ProyectoInventarioADM
{
    public partial class RegistrarItemForm : Form
    {
        private Inventario inventario;

        public RegistrarItemForm(Inventario inventario)
        {
            InitializeComponent();
            this.inventario = inventario;
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombreItem.Text;

            if (string.IsNullOrEmpty(nombre))
            {
                MessageBox.Show("Por favor, ingrese un nombre válido.");
                return;
            }

            Item nuevoItem = new Item(nombre, 0, 0, DateTime.Now);
            inventario.AgregarItem(nuevoItem);
            inventario.GuardarInventario(); // Guardar cambios

            MessageBox.Show("Item registrado exitosamente.");
            this.Close();
        }
    }
}
