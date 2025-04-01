﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace L02P02_2022EO650_2022HC650.Models
{
    public class PedidoDetalle
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("PedidoEncabezado")]
        public int IdPedido { get; set; }

        [ForeignKey("Libro")]
        public int IdLibro { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public PedidoEncabezado PedidoEncabezado { get; set; }
        public Libro Libro { get; set; }
    }
}
