using System.ComponentModel.DataAnnotations;

namespace L02P02_2022EO650_2022HC650.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Nombre { get; set; }
    }
}
