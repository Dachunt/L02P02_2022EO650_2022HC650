using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace L02P02_2022EO650_2022HC650.Models
{
    public class ComentarioLibro
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Libro")]
        public int IdLibro { get; set; }

        public string Comentarios { get; set; }

        [Required]
        [StringLength(50)]
        public string Usuario { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Libro Libro { get; set; }
    }
}
