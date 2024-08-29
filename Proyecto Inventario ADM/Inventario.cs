using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace ProyectoInventarioADM
{
    [Serializable]
    public class Inventario
    {
        private List<Item> items;
        private List<Transaccion> transacciones;

        public Inventario()
        {
            items = new List<Item>();
            transacciones = new List<Transaccion>();
            CargarInventario();
        }

        public void AgregarItem(Item item)
        {
            items.Add(item);
            var nuevaTransaccion = new Transaccion(item.Nombre, item.Cantidad, item.Precio, TipoTransaccion.Registro, item.FechaRegistro, item.FechaRegistro);
            RegistrarTransaccion(nuevaTransaccion);
            GuardarInventario();
        }

        public void EliminarItem(string nombre, string motivoBaja)
        {
            Item item = items.Find(i => i.Nombre == nombre);
            if (item != null)
            {
                // Registrar la transacción de baja
                var transaccionBaja = new Transaccion(nombre, item.Cantidad, item.Precio, TipoTransaccion.Baja, DateTime.Now, item.FechaRegistro);
                transaccionBaja.Motivo = motivoBaja; // Asignar el motivo de la baja
                RegistrarTransaccion(transaccionBaja);

                items.Remove(item);
                GuardarInventario();
            }
        }

        public Item ConsultarItem(string nombre)
        {
            return items.Find(i => i.Nombre == nombre);
        }

        public List<Item> ConsultarItemPorFecha(string nombreItem, DateTime fechaInicio, DateTime fechaFin)
        {
            return items.Where(item => item.Nombre == nombreItem && item.FechaRegistro >= fechaInicio && item.FechaRegistro <= fechaFin).ToList();
        }

        public List<Item> ObtenerItems()
        {
            return new List<Item>(items);
        }

        public List<Transaccion> ObtenerTransacciones()
        {
            return new List<Transaccion>(transacciones);
        }

        public void RegistrarConsumo(string nombreItem, int cantidad, DateTime fecha)
        {
            Item item = items.Find(i => i.Nombre == nombreItem);
            if (item != null)
            {
                item.Cantidad -= cantidad;
                if (item.Cantidad < 0) item.Cantidad = 0;

                var nuevaTransaccion = new Transaccion(nombreItem, cantidad, item.Precio, TipoTransaccion.Consumo, fecha, item.FechaRegistro);
                RegistrarTransaccion(nuevaTransaccion);
                GuardarInventario();
            }
        }
        //--------------------------------------------------------------------------------------------------------------------------------------------
        public void RegistrarTransaccion(Transaccion transaccion)
        {
            transacciones.Add(transaccion);
            Item item = items.Find(i => i.Nombre == transaccion.NombreItem);
            if (item != null)
            {
                if (transaccion.Tipo == TipoTransaccion.Compra)
                {
                    if (transaccion.Fecha.Month < DateTime.Now.Month)
                    {
                        // Actualizar valores del mes anterior
                        item.CantidadMesAnterior += transaccion.Cantidad;
                        item.CostoCompraUnitarioMesAnterior = transaccion.PrecioUnitario;
                        item.TotalBsCompraMesAnterior = Math.Round(item.CantidadMesAnterior * item.CostoCompraUnitarioMesAnterior, 2);
                    }
                    else
                    {
                        // Actualizar valores del mes actual
                        item.CompraMes += transaccion.Cantidad;
                        item.Precio = transaccion.PrecioUnitario;

                        // Si no hay saldo anterior, la cantidad se ajusta con la compra del mes
                        if (item.CantidadMesAnterior == 0)
                        {
                            item.Cantidad = item.CompraMes - item.TotalConsumo;
                        }
                        else
                        {
                            item.Cantidad = item.TotalProductoInventario - item.TotalConsumo;
                        }
                    }

                    // Actualizar Total Producto Inventario
                    item.TotalProductoInventario = item.CantidadMesAnterior + item.CompraMes;

                    // Calcular y actualizar el Promedio Ponderado
                    item.ActualizarPromedioPonderado();

                    // Calcular y actualizar Total Bs utilizando el Promedio Ponderado y la Cantidad
                    item.ActualizarTotalBs();
                }
                else if (transaccion.Tipo == TipoTransaccion.Consumo)
                {
                    item.TotalConsumo += transaccion.Cantidad;

                    // Actualizar la cantidad después del consumo
                    item.Cantidad = item.TotalProductoInventario - item.TotalConsumo;

                    // Calcular y actualizar Total Bs utilizando el Promedio Ponderado
                    item.ActualizarTotalBs();
                }
            }

            GuardarInventario();
        }
        //-------------------------------------------------------------------------------------------------------------------------------------
       
        
        public void GuardarInventario()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };

            string itemsJson = JsonSerializer.Serialize(items, options);
            File.WriteAllText("inventario.json", itemsJson);

            string transaccionesJson = JsonSerializer.Serialize(transacciones, options);
            File.WriteAllText("transacciones.json", transaccionesJson);
        }

        private void CargarInventario()
        {
            if (File.Exists("inventario.json"))
            {
                string itemsJson = File.ReadAllText("inventario.json");
                items = JsonSerializer.Deserialize<List<Item>>(itemsJson);
            }

            if (File.Exists("transacciones.json"))
            {
                string transaccionesJson = File.ReadAllText("transacciones.json");
                transacciones = JsonSerializer.Deserialize<List<Transaccion>>(transaccionesJson);
            }
        }
    }
}
