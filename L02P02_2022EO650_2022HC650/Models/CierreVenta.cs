using System;
using System.Collections.Generic;

namespace L02P02_2022EO650_2022HC650.Models
{
    public class CierreVenta
    {
        // Datos del cliente
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }

        // Detalle de los libros en el carrito
        public List<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();

        // total a pagar
        public decimal Total { get; set; }

        // El id del Pedido
        public int PedidoId { get; set; }
    }

    public class DetallePedido
    {
        public string NombreLibro { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
    }
}
