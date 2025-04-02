using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace L02P02_2022EO650_2022HC650.Models
{
    public class ComentarioLibro
    {
        [Key]
        public int id { get; set; }

        [ForeignKey("Libro")]
        public int id_libro { get; set; }

        public string comentarios { get; set; }

        [Required]
        [StringLength(50)]
        public string usuario { get; set; }

        public DateTime created_at { get; set; } = DateTime.Now;

        public Libro Libro { get; set; }
    }
}
