using Microsoft.EntityFrameworkCore;
using L02P02_2022EO650_2022HC650.Models;

namespace L02P02_2022EO650_2022HC650.Data
{
    public class LibreriaContext : DbContext
    {
        public LibreriaContext(DbContextOptions<LibreriaContext> options) : base(options) { }

        public DbSet<Cliente> clientes { get; set; }
        public DbSet<Autor> autores { get; set; }
        public DbSet<Categoria> categorias { get; set; }
        public DbSet<Libro> libros { get; set; }
        public DbSet<PedidoEncabezado> pedido_encabezado { get; set; }
        public DbSet<PedidoDetalle> pedido_detalle { get; set; }
        public DbSet<ComentarioLibro> comentarios_libros { get; set; }
        public DbSet<Carrito> Carrito { get; set; }
    }
}
