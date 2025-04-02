using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using L02P02_2022EO650_2022HC650;
using L02P02_2022EO650_2022HC650.Models;
using L02P02_2022EO650_2022HC650.Data;

var builder = WebApplication.CreateBuilder(args);

// Agregar los servicios necesarios
builder.Services.AddControllersWithViews();
builder.Services.AddSession();  // Habilitar las sesiones
builder.Services.AddDistributedMemoryCache();  // Almacenar las sesiones en memoria

// Configurar la conexión a la base de datos
builder.Services.AddDbContext<LibreriaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Agregar autorización si se necesita (aunque en este caso no lo especificas)
builder.Services.AddAuthorization();

var app = builder.Build();

// Habilitar el uso de la sesión
app.UseSession();

// Configuración del middleware de la aplicación
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();  // Redirección automática a HTTPS
app.UseStaticFiles();  // Habilitar archivos estáticos como CSS, JS, imágenes, etc.

app.UseRouting();  // Habilitar el enrutamiento de solicitudes HTTP

app.UseAuthorization();  // Habilitar la autorización si es necesario (aunque no está especificado en este caso)

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Libros}/{action=VerCarrito}/{id?}");  // Asegúrate de redirigir a "Clientes/Crear" en vez de "Login/Login"

// Ejecutar la aplicación
app.Run();
