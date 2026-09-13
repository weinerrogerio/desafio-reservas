using Serilog;

namespace WorkspaceReservas.Configurations
{
    public static class LoggingConfig
    {
        public static void AddSerilogLogging(this WebApplicationBuilder builder)
        {
            // Configurar o Serilog
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Debug()
                .CreateLogger();

            builder.Host.UseSerilog();
        }
        //public static WebApplicationBuilder AddSerilogLogging(this WebApplicationBuilder builder)
        //{
        //    // ✅ Configure Serilog usando a configuração do appsettings.json
        //    builder.Host.UseSerilog((context, services, configuration) =>
        //        configuration
        //            .ReadFrom.Configuration(context.Configuration)
        //            .ReadFrom.Services(services)
        //            .Enrich.FromLogContext());

        //    return builder;
        //}
    }
}
