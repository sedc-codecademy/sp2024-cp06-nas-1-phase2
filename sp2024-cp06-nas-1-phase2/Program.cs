using Serilog;
using Services.Helpers;
using Services.Implementations;
using Services.Interfaces;

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
            builder.Services.AddHttpClient();

            builder.Host.UseSerilog((ctx, lc) =>
            {
                lc.WriteTo.File($"Logs/logs.txt", outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}");
                lc.MinimumLevel.Debug();
                //lc.
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.RegisterDbContext(appSettings.ConnectionString);
            builder.Services.AddTransient<IApiService, ApiService>();
            builder.Services.AddTransient<IRssFeedService, RssFeedService>();
            builder.Services.AddTransient<IArticleService, ArticleService>();

            builder.Services.RegisterRepositories();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
