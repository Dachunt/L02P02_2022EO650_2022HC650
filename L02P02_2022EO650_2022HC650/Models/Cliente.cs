using System;
using System.ComponentModel.DataAnnotations;

namespace L02P02_2022EO650_2022HC650.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(255)]
        public string Apellido { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string Direccion { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
