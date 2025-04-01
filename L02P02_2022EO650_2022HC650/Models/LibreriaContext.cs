using Microsoft.EntityFrameworkCore;
using L02P02_2022EO650_2022HC650.Models;

namespace L02P02_2022EO650_2022HC650.Data
{
    public class LibreriaContext : DbContext
    {
        public LibreriaContext(DbContextOptions<LibreriaContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<PedidoEncabezado> PedidoEncabezados { get; set; }
        public DbSet<PedidoDetalle> PedidoDetalles { get; set; }
        public DbSet<ComentarioLibro> ComentariosLibros { get; set; }
    }
}
