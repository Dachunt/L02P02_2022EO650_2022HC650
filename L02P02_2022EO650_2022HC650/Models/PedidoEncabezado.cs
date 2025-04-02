using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace L02P02_2022EO650_2022HC650.Models
{
    public class PedidoEncabezado
    {
        [Key]
        public int id { get; set; }

        [ForeignKey("Cliente")]
        public int id_cliente { get; set; }

        public int cantidad_libros { get; set; }
        public decimal total { get; set; }

        public Cliente Cliente { get; set; }

        public char estado { get; set; } = 'P';

        // Relación de navegación a PedidoDetalle (un PedidoEncabezado tiene muchos PedidoDetalles)
        public ICollection<PedidoDetalle> PedidoDetalles { get; set; }
    }
}
