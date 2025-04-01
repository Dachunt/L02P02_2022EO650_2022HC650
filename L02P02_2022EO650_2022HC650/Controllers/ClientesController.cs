using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using L02P02_2022EO650_2022HC650.Data;
using L02P02_2022EO650_2022HC650.Models;
using System;
using System.Linq;

namespace L02P02_2022EO650_2022HC650.Controllers
{
    public class ClientesController : Controller
    {
        private readonly LibreriaContext _context;

        public ClientesController(LibreriaContext context)
        {
            _context = context;
        }

        // Mostrar formulario de registro
        public IActionResult Crear()
        {
            return View();
        }

        // Procesar formulario de registro
        [HttpPost]
        public IActionResult Crear(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                cliente.CreatedAt = DateTime.Now;
                _context.Clientes.Add(cliente);
                _context.SaveChanges();

                // Guardar el ID del cliente en sesión
                HttpContext.Session.SetInt32("ClienteId", cliente.Id);

                // Crear pedido en proceso
                PedidoEncabezado pedido = new PedidoEncabezado
                {
                    IdCliente = cliente.Id,
                    CantidadLibros = 0,
                    Total = 0,
                    Estado = 'P'
                };

                _context.PedidoEncabezados.Add(pedido);
                _context.SaveChanges();

                // Guardar el ID del pedido en sesión
                HttpContext.Session.SetInt32("PedidoId", pedido.Id);

                return RedirectToAction("ListaLibros", "Libros");
            }
            return View(cliente);
        }
    }
}
