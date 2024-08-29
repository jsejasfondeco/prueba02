using System;
using System.Windows.Forms;

namespace ProyectoInventarioADM
{
    public partial class ReportesForm : Form
    {
        private Inventario inventario;
        private string nombreUsuario;

        public ReportesForm(Inventario inventario, string usuario)
        {
            InitializeComponent();
            this.inventario = inventario;
            nombreUsuario = usuario;
            ActualizarInformacionUsuario();
            CargarItems();
        }

        private void ActualizarInformacionUsuario()
        {
            // Actualiza el label con el nombre del usuario y la fecha actual
            lblUsuarioFecha.Text = $"{nombreUsuario} - {DateTime.Now.ToString("dd/MM/yyyy")}";
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

        private void btnGenerarReporte_Click(object sender, EventArgs e)
        {
            if (comboBoxItems.SelectedItem != null)
            {
                string nombreItem = comboBoxItems.SelectedItem.ToString();
                int tipoReporte = (int)numTipoReporte.Value;
                DateTime fechaProceso = dtpFechaProceso.Value;

                // Lógica para generar el reporte según el tipo
                if (tipoReporte == 1)
                {
                    // Generar reporte detallado
                    GenerarReporteDetallado(nombreItem, fechaProceso);
                }
                else if (tipoReporte == 2)
                {
                    // Generar reporte resumido
                    GenerarReporteResumido(nombreItem, fechaProceso);
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un ítem.");
            }
        }

        private void GenerarReporteDetallado(string nombreItem, DateTime fechaProceso)
        {
            var item = inventario.ConsultarItem(nombreItem);

            if (item != null)
            {
                DetalleReportForm detalleForm = new DetalleReportForm(item);
                detalleForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Item no encontrado.");
            }
        }

        private void GenerarReporteResumido(string nombreItem, DateTime fechaProceso)
        {
            var item = inventario.ConsultarItem(nombreItem);

            if (item != null)
            {
                ResumenReportForm resumenForm = new ResumenReportForm(item);
                resumenForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Item no encontrado.");
            }
        }
    }
}
