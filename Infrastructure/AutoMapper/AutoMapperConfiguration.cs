using AutoMapper;

namespace Infrastructure.AutoMapper;
public static class AutoMapperConfiguration
{
    public static MapperConfiguration MapperConfiguration { get; private set; }
    public static IMapper Mapper { get; private set; }
    public static void Configure()
    {
        MapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<CurrencyHistoryProfile>();
            cfg.AddProfile<CommodityMappingProfile>();
        });

        MapperConfiguration.AssertConfigurationIsValid();
        Mapper = MapperConfiguration.CreateMapper();
    }
}
