using System.ComponentModel.DataAnnotations;

namespace L02P02_2022EO650_2022HC650.Models
{
    public class Libro
    {
        [Key]
        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string nombre { get; set; }

        public string descripcion { get; set; }
        public string url_imagen { get; set; }
        public int id_autor { get; set; }
        public int id_categoria { get; set; }

        [Required]
        public decimal precio { get; set; }
        public char estado { get; set; } = 'A';
    }
}
