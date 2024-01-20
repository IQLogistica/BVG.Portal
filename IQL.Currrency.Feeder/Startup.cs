using CoreLogic.Contracts;
using CoreLogic.Services.Commands;
using CoreLogic.Services.Handlers;
using Infrastructure.Database.BVG;
using Infrastructure.Repository;
using MediatR;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
[assembly: FunctionsStartup(typeof(IQL.Currency.Feeder.Startup))]
namespace IQL.Currency.Feeder;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
        CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

        // Configure MediatR
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(IQL.Currency.Feeder.Startup).Assembly));

        // Database connection
        string connection = Environment.GetEnvironmentVariable("BVG_Connection_PostGreSQL");
        builder.Services.AddDbContext<iql_bvgContext>(options =>
              options.UseNpgsql(connection), ServiceLifetime.Scoped);

        var utcTimeZone = TimeZoneInfo.FindSystemTimeZoneById("UTC");
        builder.Services.AddSingleton<TimeZoneInfo>(utcTimeZone);

        builder.Services.AddScoped<IRequestHandler<CurrencyPriceFeedCommand, Task>, CurrencyHandler>();
        builder.Services.AddScoped<ICurrencyHistoryRepository, CurrencyHistoryRepository>();
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }
}