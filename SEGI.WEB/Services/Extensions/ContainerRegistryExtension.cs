using Microsoft.Extensions.DependencyInjection;
using SEGI.Services.FileServices;
using SEGI.Services.Users;

namespace SEGI.Services.Extenstions
{
    public static class ContainerRegistryExtension
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            // Register the in-memory cache and the cache service
            services.AddMemoryCache(); // Registers IMemoryCache

            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IInterfaceServices, InterfaceServices>();

            // Register your custom ApplicationUserManager

            return services;
        }
    }
}
