using DataAccess;
using DataAccess.Implementations;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Services.Helpers
{
    public static class DiModule
    {
        public static IServiceCollection RegisterDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<NewsAggregatorDbContext>(options =>
                options.UseSqlServer(connectionString));

            return services;
        }

        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

            services.AddTransient<IRssFeedRepository, RssFeedRepository>();
            //services.AddTransient<IUserInfoRepository, UserInfoRepository>();
            //services.AddTransient<IBeverageRepository, BeverageRepository>();
            ////services.AddTransient<IRepository<Category>>(x => new CategoryAdoRepository(connectionString));
            //services.AddTransient<IRepository<Category>>(x => new CategoryDapperRepository(connectionString));

            return services;
        }
    }
}
