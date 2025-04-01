using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace L02P02_2022EO650_2022HC650.Models
{
    public class PedidoEncabezado
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Cliente")]
        public int IdCliente { get; set; }

        public int CantidadLibros { get; set; }
        public decimal Total { get; set; }
        public char Estado { get; set; } = 'P'; // P = Proceso, C = Cerrado

        public Cliente Cliente { get; set; }
    }
}
