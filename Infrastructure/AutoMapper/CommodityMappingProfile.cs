using AutoMapper;
using CoreLogic.Models;
using Infrastructure.Database.BVG;

namespace Infrastructure.AutoMapper;

public class CommodityMappingProfile : Profile
{
    public CommodityMappingProfile()
    {
        CreateMap<List<CommodityData>, List<CommodityHistory>>()
        .ConvertUsing((src, dest, ctx) =>
        {
            var result = new List<CommodityHistory>();
            var commodities = ctx.Items["Commodities"];
            var filtered = FilterCommodities(src, commodities);
            foreach (var commodityData in filtered)
            {
                var history = new CommodityHistory
                {
                    Contract = commodityData.Contract,
                    OpeningPrice = commodityData.OpeningPrice,
                    LastTradedTime = commodityData.LastTradedTime.AddHours(-2),
                    LastTradedPrice = commodityData.LastTradedPrice,
                    Volume = commodityData.Volume,
                    LastUpdated = commodityData.LastUpdated.AddHours(-2),
                    CreatedDate = DateTime.Now,
                    CommodityId = FindId(commodityData, commodities),
                    CurrencyId = FindId(commodityData, ctx.Items["Currencies"])
                };
                result.Add(history);
            }
            return result;
        });
    }

    private List<CommodityData> FilterCommodities(List<CommodityData> commodityData, object additionalArgument)
    {
        if (additionalArgument is List<Commodity> commodities) return commodityData.Where(f => commodities.Any(s => s.Label == f.Instrument)).ToList();
        return commodityData;
    }

    private int FindId(CommodityData source, object additionalArgument)
    {
        switch (additionalArgument)
        {
            case List<Commodity> commodities:
                var matchingCommodity = commodities.FirstOrDefault(c => c.Label == source.Instrument);
                return matchingCommodity?.Id ?? 0;

            case List<Currency> currencies:
                var matchingCurrency = currencies.FirstOrDefault(c => c.Label == "ZAR");
                return matchingCurrency?.Id ?? 0;

            default:
                return 0;
        }
    }
}