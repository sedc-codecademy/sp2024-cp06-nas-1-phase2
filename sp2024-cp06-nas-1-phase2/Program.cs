using Helpers.Extensions;
using Mappers;
using Serilog;
using Shared.Settings;

namespace sp2024_cp06_nas_1_phase2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            // Add services to the container.
            var appConfig = builder.Configuration.GetSection("AppSettings");
            builder.Services.Configure<AppSettings>(appConfig);
            var appSettings = appConfig.Get<AppSettings>();

            builder.Services.AddControllers();
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            builder.Services.AddHttpClient();

            builder.Host.UseSerilog((context, configuration) =>
            {
                configuration
                    .MinimumLevel.Debug()
                    .WriteTo.Console()
                    .WriteTo.File(
                        path: $"Logs/log-.txt",
                        rollingInterval: RollingInterval.Day,
                        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                        retainedFileCountLimit: 7
                    );
            });
            
            /*
            //Configure Serilog to log all levels to file, and only info &error to console
            //Log.Logger = new LoggerConfiguration()
            //    .MinimumLevel.Debug() // Minimum log level for the application
            //    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning) // For ASP.NET core, log at least Warning level
            //    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day) // Log everything to a file
            //    .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information) // Log only Information and above to Console
            //    .CreateLogger();

            //try
            //{
            //    Log.Information("Starting up the application...");
            //    CreateHostBuilder(args).Build().Run();
            //}
            //catch (Exception ex)
            //{
            //    Log.Fatal(ex, "Application start-up failed");
            //}
            //finally
            //{
            //    Log.CloseAndFlush();
            //}
            */

            builder.Services.RegisterDbContext(appSettings.ConnectionString);
            builder.Services.RegisterRepositories();
            builder.Services.RegisterServices();
            builder.Services.AddSwagger();
            builder.Services.AddCustomCors();
            //builder.Services.AddJwt(builder.Configuration);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .UseSerilog()  // Use Serilog as the logger
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Program>();
        //        });
    }
}
