//using Microsoft.Extensions.ConfigurationMicrosoft.Extensions.Configuration;

using Microsoft.Extensions.Configuration;

namespace FirstConsumer;

public static class Extensions
{
    public static IConfigurationBuilder AddAppSettings(
        this IConfigurationBuilder builder)
    {
        //var config = builder.Configuration.GetSection("SecurityConfiguration");
        var environment = System.Configuration.ConfigurationManager.AppSettings["Environment"];

        string pathPrefix = environment == "development"
            ? ""
            : "Bin/";

        return builder
            .AddJsonFile($"{pathPrefix}appsettings/appsettings.json", false)
            .AddJsonFile($"{pathPrefix}appsettings/appsettings.{environment}.json", false);
    }
}