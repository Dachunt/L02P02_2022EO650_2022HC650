using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using L02P02_2022EO650_2022HC650.Data;
using L02P02_2022EO650_2022HC650.Models;
using System;

namespace L02P02_2022EO650_2022HC650.Controllers
{
    public class ClientesController : Controller
    {
        private readonly LibreriaContext _context;

        public ClientesController(LibreriaContext context)
        {
            _context = context;
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.clientes.Add(cliente);
                _context.SaveChanges();

                HttpContext.Session.SetInt32("ClienteId", cliente.id);

                PedidoEncabezado pedido = new PedidoEncabezado
                {
                    id_cliente = cliente.id,
                    cantidad_libros = 0,
                    total = 0
                };

                _context.pedido_encabezado.Add(pedido);
                _context.SaveChanges();

                HttpContext.Session.SetInt32("PedidoId", pedido.id);

                return RedirectToAction("ListaLibros", "libros");
            }

            return View(cliente); 
        }
    }
}
