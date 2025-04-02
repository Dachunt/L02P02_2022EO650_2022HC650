using L02P02_2022EO650_2022HC650.Models;
using L02P02_2022EO650_2022HC650.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace L02P02_2022EO650_2022HC650.Controllers
{
    public class CierreVentaController : Controller
    {
        private readonly LibreriaContext _context;

        public CierreVentaController(LibreriaContext context)
        {
            _context = context;
        }

        // Acción para mostrar el formulario de cierre de venta
        public IActionResult CierreVenta()
        {
            var clienteId = HttpContext.Session.GetInt32("ClienteId");
            var pedidoId = HttpContext.Session.GetInt32("PedidoId");

            if (clienteId == null || pedidoId == null)
            {
                return RedirectToAction("Index", "Home"); // Redirigir si no hay cliente o pedido
            }

            var cliente = _context.clientes.Find(clienteId);
            var pedido = _context.pedido_encabezado.Find(pedidoId);

            if (cliente == null || pedido == null)
            {
                return RedirectToAction("Index", "Home"); // Redirigir si no se encuentra cliente o pedido
            }

            var detallePedidos = _context.pedido_detalle
                                          .Where(pd => pd.id_pedido == pedidoId)
                                          .Select(pd => new DetallePedido
                                          {
                                              NombreLibro = pd.Libro.Nombre,
                                              Precio = pd.Libro.precio,
                                              Cantidad = 1 // Suponemos que cada detalle es de un solo libro
                                          }).ToList();

            var cierreVenta = new CierreVenta
            {
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                Email = cliente.Email,
                Direccion = cliente.Direccion,
                DetallePedidos = detallePedidos,
                Total = detallePedidos.Sum(dp => dp.Precio), // total calculado
                PedidoId = pedidoId.Value
            };

            return View(cierreVenta); // Pasamos el modelo a la vista
        }

        // Acción para cerrar la venta
        [HttpPost]
        public IActionResult CerrarVenta(int pedidoId)
        {
            var pedido = _context.pedido_encabezado.Find(pedidoId);

            if (pedido != null)
            {
                _context.SaveChanges(); // Guardar los cambios en la base de datos
                TempData["Mensaje"] = "Venta cerrada exitosamente."; // Mensaje de confirmación
            }

            return RedirectToAction("CierreVenta", new { pedidoId }); // Redirigir al formulario con el estado actualizado
        }
    }
}
