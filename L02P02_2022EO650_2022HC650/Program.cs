using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using L02P02_2022EO650_2022HC650;
using L02P02_2022EO650_2022HC650.Models;
using L02P02_2022EO650_2022HC650.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddDistributedMemoryCache();

// Configurar la conexión a la base de datos
builder.Services.AddDbContext<LibreriaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Agrega el servicio de autorización
builder.Services.AddAuthorization();

// Asegúrate de agregar los controladores
builder.Services.AddControllersWithViews();

var app = builder.Build();
builder.Services.AddSession();
app.UseSession();

// Configure the HTTP request pipeline. 
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
