using ExportDemo.DAL.Repositories.Implementations;
using ExportDemo.DAL.Repositories.Interfaces;
using Rotativa.AspNetCore;


var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IDataRepository, DataRepository>();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
// app.UseHttpsRedirection();
app.UseAuthorization();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

RotativaConfiguration.Setup(app.Environment.WebRootPath, "Rotativa");

app.Run();
