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

// Configurar la conexi�n a la base de datos
builder.Services.AddDbContext<LibreriaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Agregar autorizaci�n si se necesita (aunque en este caso no lo especificas)
builder.Services.AddAuthorization();

var app = builder.Build();

// Habilitar el uso de la sesi�n
app.UseSession();

// Configuraci�n del middleware de la aplicaci�n
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();  // Redirecci�n autom�tica a HTTPS
app.UseStaticFiles();  // Habilitar archivos est�ticos como CSS, JS, im�genes, etc.

app.UseRouting();  // Habilitar el enrutamiento de solicitudes HTTP

app.UseAuthorization();  // Habilitar la autorizaci�n si es necesario (aunque no est� especificado en este caso)

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Libros}/{action=VerCarrito}/{id?}");  // Aseg�rate de redirigir a "Clientes/Crear" en vez de "Login/Login"

// Ejecutar la aplicaci�n
app.Run();
