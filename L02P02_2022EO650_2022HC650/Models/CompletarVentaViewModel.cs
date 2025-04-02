using System.Collections.Generic;

namespace L02P02_2022EO650_2022HC650.Models
{
    public class CompletarVentaViewModel
    {
        public Cliente Cliente { get; set; }
        public PedidoEncabezado Pedido { get; set; }
        public List<PedidoDetalle> DetallesPedido { get; set; }
    }
}
