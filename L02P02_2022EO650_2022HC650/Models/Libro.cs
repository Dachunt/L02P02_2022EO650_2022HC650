using System.ComponentModel.DataAnnotations;

namespace L02P02_2022EO650_2022HC650.Models
{
    public class Libro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }
        public string UrlImagen { get; set; }
        public int IdAutor { get; set; }
        public int IdCategoria { get; set; }

        [Required]
        public decimal Precio { get; set; }
        public char Estado { get; set; } = 'A';
    }
}
