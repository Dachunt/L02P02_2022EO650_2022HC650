using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using L02P02_2022EO650_2022HC650.Data;
using L02P02_2022EO650_2022HC650.Models;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace L02P02_2022EO650_2022HC650.Controllers
{
    public class LibrosController : Controller
    {
        private readonly LibreriaContext _context;

        public LibrosController(LibreriaContext context)
        {
            _context = context;
        }

        // Mostrar la lista de libros
        public IActionResult ListaLibros()
        {
            var libros = _context.libros.ToList();

            // Obtener ID del Cliente desde la sesión
            int idCliente = HttpContext.Session.GetInt32("ClienteId") ?? 1;

            // Buscar pedido en proceso
            var pedido = _context.pedido_encabezado.FirstOrDefault(p => p.id_cliente == idCliente);
            if (pedido != null)
            {
                HttpContext.Session.SetInt32("TotalLibros", pedido.cantidad_libros);
                HttpContext.Session.SetInt32("TotalPrecio", (int)pedido.total);
            }

            return View(libros);
        }

        // Agregar un libro al carrito
        [HttpPost]
        public IActionResult AgregarAlCarrito(int idLibro)
        {
            // Obtener ID del Cliente desde la sesión
            int idCliente = HttpContext.Session.GetInt32("ClienteId") ?? 1;

            // Buscar pedido en proceso
            var pedido = _context.pedido_encabezado.FirstOrDefault(p => p.id_cliente == idCliente);
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

            // Buscar el libro
            var libro = _context.libros.Find(idLibro);
            if (libro == null)
            {
                return NotFound("El libro no existe.");
            }

            // Agregar detalle del pedido
            var detalle = new PedidoDetalle
            {
                id_pedido = pedido.id,
                id_libro = idLibro,
                created_at = DateTime.Now
            };
            _context.pedido_detalle.Add(detalle);
            _context.SaveChanges();

            // Actualizar totales
            pedido.cantidad_libros++;
            pedido.total += libro.precio;
            _context.SaveChanges();

            // Guardar en sesión
            HttpContext.Session.SetInt32("TotalLibros", pedido.cantidad_libros);
            HttpContext.Session.SetInt32("TotalPrecio", (int)pedido.total);

            return RedirectToAction("ListaLibros");
        }

        // Método para completar la compra
        public IActionResult CompletarPedido()
        {
            int idCliente = HttpContext.Session.GetInt32("ClienteId") ?? 1;
            var pedido = _context.pedido_encabezado.FirstOrDefault(p => p.id_cliente == idCliente);

            if (pedido == null || pedido.cantidad_libros == 0)
            {
                TempData["MensajeError"] = "No hay libros en el carrito para completar la compra.";
                return RedirectToAction("ListaLibros");
            }

            // Limpiar sesión después de completar la compra
            HttpContext.Session.Remove("TotalLibros");
            HttpContext.Session.Remove("TotalPrecio");

            TempData["MensajeExito"] = "Compra completada con éxito.";
            return RedirectToAction("ListaLibros");
        }

        public IActionResult CierreVenta()
        {
            int idCliente = HttpContext.Session.GetInt32("ClienteId") ?? 1;

            var pedido = _context.pedido_encabezado
                .Include(p => p.Cliente)
                .Include(p => p.PedidoDetalles)
                    .ThenInclude(d => d.Libro)
                .FirstOrDefault(p => p.id_cliente == idCliente && p.estado == 'P');

            if (pedido == null)
            {
                return RedirectToAction("ListaLibros", "Libros");
            }

            return View(pedido);
        }

        [HttpPost]
        public IActionResult CerrarVenta(int idPedido)
        {
            var pedido = _context.pedido_encabezado.FirstOrDefault(p => p.id == idPedido);

            if (pedido != null)
            {
                pedido.estado = 'C'; 
                _context.SaveChanges();
                TempData["Mensaje"] = "Venta cerrada exitosamente.";
            }

            return RedirectToAction("ListaLibros", "Libros");
        }
    }
}
