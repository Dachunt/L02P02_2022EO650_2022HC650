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

        public ActionResult Index()
        {
            var libros = _context.Libros.Where(l => l.Estado == 'A').ToList();
            return View(libros);
        }

        // Agregar libro al carrito
        [HttpPost]
        public ActionResult AgregarAlCarrito(int id)
        {
            var libro = _context.Libros.Find(id);
            if (libro == null)
            {
                return RedirectToAction("Index");
            }

            var item = _context.Carrito.FirstOrDefault(c => c.LibroId == id);
            if (item != null)
            {
                item.Cantidad++;
            }
            else
            {
                _context.Carrito.Add(new Carrito { LibroId = id, Cantidad = 1 });
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // Ver Carrito
        public ActionResult VerCarrito()
        {
            var carrito = _context.Carrito.ToList();
            var libros = _context.Libros.ToList();

            var carritoViewModel = from c in carrito
                                   join l in libros on c.LibroId equals l.Id
                                   select new
                                   {
                                       c.Id,
                                       l.Nombre,
                                       l.Precio,
                                       c.Cantidad
                                   };

            return View(carritoViewModel);
        }
    }
}
