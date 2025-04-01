using Microsoft.AspNetCore.Mvc;
using L02P02_2022EO650_2022HC650.Data;
using L02P02_2022EO650_2022HC650.Models;
using System.Linq;

namespace L02P02_2022EO650_2022HC650.Controllers
{
    public class LibrosController : Controller
    {
        private readonly LibreriaContext _context;

        public LibrosController(LibreriaContext context)
        {
            _context = context;
        }

        // Acción para mostrar el listado de libros
        public IActionResult ListaLibros()
        {
            // Obtener la lista de libros desde la base de datos
            var libros = _context.Libros.ToList();
            return View(libros);  // Pasar la lista de libros a la vista
        }

        // Acción para agregar un libro al carrito (pedido)
        [HttpPost]
        public IActionResult AgregarAlCarrito(int libroId)
        {
            var clienteId = HttpContext.Session.GetInt32("ClienteId");
            var pedidoId = HttpContext.Session.GetInt32("PedidoId");

            if (clienteId != null && pedidoId != null)
            {
                // Obtener el libro seleccionado
                var libro = _context.Libros.Find(libroId);
                if (libro != null)
                {
                    // Aquí podrías agregar lógica para agregar el libro al pedido.
                    // Ejemplo: Crear un nuevo detalle de pedido
                    PedidoDetalle detalle = new PedidoDetalle
                    {
                        IdPedido = pedidoId.Value,
                        IdLibro = libro.Id,
                        CreatedAt = DateTime.Now
                    };

                    _context.PedidoDetalles.Add(detalle);
                    _context.SaveChanges();

                    // Actualizar cantidad y total del pedido
                    var pedido = _context.PedidoEncabezados.Find(pedidoId.Value);
                    pedido.CantidadLibros += 1;
                    pedido.Total += libro.Precio;
                    _context.SaveChanges();
                }
            }

            return RedirectToAction("ListaLibros", "Libros");
        }
    }
}
