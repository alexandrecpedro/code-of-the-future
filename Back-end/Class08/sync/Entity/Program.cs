// forma (dotnet 3, 5, 6 e 7)
namespace Entity;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}

/* minimal api (dotnet 6 e 7)
using Entity.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.UseStartup();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DbContexto>(options =>
{
    var connection = Environment.GetEnvironmentVariable("DATABASE_CODE_OF_THE_FUTURE");
    if (connection is null) connection = "Server=localhost;Database=persistence_code_of_the_future;Uid=root;Pwd=root;";
    options.UseMySql(connection, ServerVersion.AutoDetect(connection));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
*/