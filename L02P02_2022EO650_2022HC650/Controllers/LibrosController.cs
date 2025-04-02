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

        public IActionResult ListaLibros()
        {
            var libros = _context.libros.ToList();
            return View(libros); 
        }


        public IActionResult Index()
        {
            var libros = _context.libros
                .Where(l => l.Estado == 'A') 
                .ToList();

            int idCliente = 1;
            var pedido = _context.pedido_encabezado
                .FirstOrDefault(p => p.id_cliente == idCliente);

            ViewBag.TotalLibros = pedido?.cantidad_libros ?? 0;
            ViewBag.TotalPrecio = pedido?.total ?? 0;

            return View(libros);
        }

        [HttpPost]
        public IActionResult AgregarAlCarrito(int idLibro)
        {
            int idCliente = 1;

            var pedido = _context.pedido_encabezado
                .FirstOrDefault(p => p.id_cliente == idCliente);

            if (pedido == null)
            {
                pedido = new PedidoEncabezado
                {
                    id_cliente = idCliente,
                    cantidad_libros = 0,
                    total = 0
                };
                _context.pedido_encabezado.Add(pedido);
                _context.SaveChanges();
            }

            var detalle = new PedidoDetalle
            {
                IdPedido = pedido.id,
                IdLibro = idLibro,
                CreatedAt = DateTime.Now
            };
            _context.pedido_detalle.Add(detalle);
            _context.SaveChanges();

            pedido.cantidad_libros++;
            pedido.total += _context.libros.Find(idLibro)?.Precio ?? 0;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
