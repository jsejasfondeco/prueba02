using System;
using System.Windows.Forms;

namespace ProyectoInventarioADM
{
    public partial class MainForm : Form
    {
        private Inventario inventario;

        public MainForm()
        {
            InitializeComponent();
            inventario = new Inventario();
            ConfigurarTreeView(); // Configurar el TreeView
        }
        //------------------------------------------------------------------------
        private void ConfigurarTreeView()
        {
            treeView1.Nodes.Clear();

            // Crear el nodo principal "INVENTARIO"
            TreeNode nodoInventario = new TreeNode("INVENTARIO");

            // Crear el subnodo "Sistemas" dentro de "INVENTARIO"
            TreeNode nodoSistemas = new TreeNode("Sistemas");

            // Añadir los submódulos a "Sistemas"
            nodoSistemas.Nodes.Add("Registrar Item Nuevo");
            nodoSistemas.Nodes.Add("Dar de Baja Item");
            nodoSistemas.Nodes.Add("Consultar Item");
            nodoSistemas.Nodes.Add("Registrar Consumo");
            nodoSistemas.Nodes.Add("Historial de Transacciones");
            nodoSistemas.Nodes.Add("Registrar Compra");
            nodoSistemas.Nodes.Add("Cierre Mensual");
            nodoSistemas.Nodes.Add("Cambiar Clave");

            // Crear el subnodo "Reportes" dentro de "INVENTARIO"
            TreeNode nodoReportes = new TreeNode("Reportes");

            // Añadir los subnodos al nodo "INVENTARIO"
            nodoInventario.Nodes.Add(nodoSistemas);
            nodoInventario.Nodes.Add(nodoReportes);

            // Añadir "INVENTARIO" al TreeView
            treeView1.Nodes.Add(nodoInventario);

            // Expandir solo el nodo "INVENTARIO" y colapsar los demás
            treeView1.Nodes[0].Expand(); // Expandir "INVENTARIO"
        }

        //-----------------------------------------------------------

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            switch (e.Node.Text)
            {
                case "Registrar Item Nuevo":
                    RegistrarItemForm registrarForm = new RegistrarItemForm(inventario);
                    registrarForm.ShowDialog();
                    break;
                case "Dar de Baja Item":
                    DarDeBajaItemForm darBajaForm = new DarDeBajaItemForm(inventario);
                    darBajaForm.ShowDialog();
                    break;
                case "Consultar Item":
                    ConsultarItemForm consultarForm = new ConsultarItemForm(inventario);
                    consultarForm.ShowDialog();
                    break;
                case "Registrar Consumo":
                    RegistrarConsumoForm consumoForm = new RegistrarConsumoForm(inventario);
                    consumoForm.ShowDialog();
                    break;
                case "Historial de Transacciones":
                    HistorialTransaccionesForm historialForm = new HistorialTransaccionesForm(inventario);
                    historialForm.ShowDialog();
                    break;
                case "Registrar Compra":
                    RegistroCompraForm compraForm = new RegistroCompraForm(inventario);
                    compraForm.ShowDialog();
                    break;
                case "Cierre Mensual":
                    CierreMensualForm cierreMensualForm = new CierreMensualForm(inventario);
                    cierreMensualForm.ShowDialog();
                    break;
                case "Reportes":
                    string usuario = "usuario_actual"; // Reemplaza esto con la lógica que obtiene el nombre del usuario actual
                    ReportesForm reportesForm = new ReportesForm(inventario, usuario);
                    reportesForm.ShowDialog();
                    break;
                case "Cambiar Clave":
                    CambiarClaveForm cambiarClaveForm = new CambiarClaveForm();
                    cambiarClaveForm.ShowDialog();
                    break;
            }
        }



    }
}
