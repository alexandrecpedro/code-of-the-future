using Entity.Context;
using Microsoft.EntityFrameworkCore;

namespace Entity;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        // var connection = Configuration["appsettings.property"];
        var connection = Environment.GetEnvironmentVariable("DATABASE_CODE_OF_THE_FUTURE");
        if (connection is null) connection = Configuration.GetConnectionString("connection");

        services.AddDbContext<DbContexto>(options =>
        {
            options.UseMySql(connection, ServerVersion.AutoDetect(connection));
        });
        // services.AddControllers();
        services.AddControllersWithViews();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (!env.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            // endpoints.MapControllers(); // use my own routes (controller => ex: [Route("/privacy")])
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}