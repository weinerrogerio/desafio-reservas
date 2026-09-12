using Microsoft.EntityFrameworkCore;
using WorkspaceReservas.Models.Context;

namespace WorkspaceReservas.Configurations
{
    public static class DataBaseConfig
    {
        public static IServiceCollection AddDataBaseConfig(
            this IServiceCollection services, IConfiguration configuration)
        {
            //buscar a connection string (ConnectionStrings:DefaultConnection) no arquivo appsettings.json
            var conestionString = configuration["ConnectionStrings:DefaultConnection"];
            if ( string.IsNullOrEmpty(conestionString) ) throw new Exception("Connection String not found : ConnectionStrings:DefaultConnection");
            services.AddDbContext<PostgreSQLContext>(options =>
                //options.UseNpgsql(configuration.GetConnectionString(conestionString)));
                options.UseNpgsql(conestionString));
            return services;

        }
    }
}
