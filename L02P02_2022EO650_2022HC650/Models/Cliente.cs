using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace L02P02_2022EO650_2022HC650.Models
{
    public class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Esto asegura que el ID sea autoincremental en la base de datos
        public int id { get; set; }

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

        // El campo CreatedAt se maneja desde la base de datos, no necesitamos asignarlo manualmente
        public DateTime created_at { get; set; } = DateTime.Now;  // Este valor ahora será manejado por la base de datos
    }
}
