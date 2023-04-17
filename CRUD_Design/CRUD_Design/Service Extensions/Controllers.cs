using CRUD_Design.Contracts;
using CRUD_Design.Repository;
using CRUD_Design_Contracts;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Controllers
    {
        public static IServiceCollection AddRepoServices(
        this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IRunInfoRepository, RunInfoRepository>();

            return services;
        }
    }
}
