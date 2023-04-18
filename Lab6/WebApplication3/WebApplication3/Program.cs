using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

public class Program
{
    public static void Main(string[] args)
    {
        // Create a new instance of IHostBuilder using the default configuration
        CreateHostBuilder(args).Build().Run();
    }

    // Returns a new IHostBuilder instance for the Startup class to configure the web-app
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}

// Setting up the web-app
public class Startup
{
    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        Configuration = configuration;
        Environment = env;
    }

    public IConfiguration Configuration { get; }
    public IWebHostEnvironment Environment { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        // Support for web API controllers
        services.AddControllers();

        // If the development environment is currently running, then add support for Swagger
        if (Environment.IsDevelopment())
        {
            // Register Swagger with version
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My Web API", Version = "v1" });
            });
        }
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // If the development environment is currently running, then add Swagger middleware and widgets
        if (env.IsDevelopment())
        {
            // Enable Swagger middleware
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Web API v1");
                c.RoutePrefix = string.Empty;
            });
        }

        // Add middleware to route requests to web API controllers
        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}