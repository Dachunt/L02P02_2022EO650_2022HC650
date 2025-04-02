using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace L02P02_2022EO650_2022HC650.Models
{
    public class PedidoDetalle
    {
        [Key]
        public int id { get; set; }

        [ForeignKey("PedidoEncabezado")]
        public int id_pedido { get; set; }

        [ForeignKey("Libro")]
        public int id_libro { get; set; }

        public DateTime created_at { get; set; } = DateTime.Now;

        public PedidoEncabezado PedidoEncabezado { get; set; }
        public Libro Libro { get; set; }
    }
}
