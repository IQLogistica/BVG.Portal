using AutoMapper;
using CoreLogic.Models;
using Infrastructure.Database.BVG;

namespace Infrastructure.AutoMapper;

public class CurrencyHistoryProfile : Profile
{
    public CurrencyHistoryProfile()
    {
        CreateMap<CurrencyPrice, CurrencyHistory>()
         .ConstructUsing((src, ctx) => CreateDestination(src, ctx.Items["CurrencyPairs"]))
        .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignore mapping for Id property
                  .ForMember(dest => dest.CurrencyPairId, opt => opt.MapFrom(src => src.id))
        .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => decimal.Parse(src.prices[0].rate)))
        .ForMember(dest => dest.StartRate, opt => opt.MapFrom(src => decimal.Parse(src.change.start_rate)))
        .ForMember(dest => dest.EndRate, opt => opt.MapFrom(src => decimal.Parse(src.change.end_rate)))
        .ForMember(dest => dest.Change, opt => opt.MapFrom(src => decimal.Parse(src.change.change)))
        .ForMember(dest => dest.ChangePercent, opt => opt.MapFrom(src => decimal.Parse(src.change.change_pct)))
        .ForMember(dest => dest.TimeStampUnix, opt => opt.MapFrom(src => int.Parse(src.prices[0].timestamp)))
        .ForMember(dest => dest.TimeStamp, opt => opt.MapFrom(src => DateTime.Parse(src.prices[0].timestamp_date).AddHours(-2)))
        .ForMember(dest => dest.CurrencyPairId, opt => opt.MapFrom(src => src.code))
        .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
        .ForMember(dest => dest.CurrencyPairId, opt => opt.Ignore());
    }
    private CurrencyHistory CreateDestination(CurrencyPrice source, object additionalArgument)
    {
        var destination = new CurrencyHistory();

        if (additionalArgument is List<CurrencyPair> currencyPairs)
        {
            var matchingCurrencyPair = currencyPairs.FirstOrDefault(cp => cp.Label == source.code);
            if (matchingCurrencyPair != null)
            {
                destination.CurrencyPairId = matchingCurrencyPair.Id;
            }
        }

        return destination;
    }
}