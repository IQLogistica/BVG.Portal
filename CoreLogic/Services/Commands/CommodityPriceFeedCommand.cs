using CoreLogic.Contracts;
using CoreLogic.Models;
using CoreLogic.Transform;
using MediatR;

namespace CoreLogic.Services.Commands;

public class CommodityPriceFeedCommand : IRequest<Task>
{
    public Commodities Commodities { get; set; }
    private readonly IDateTransform _dateTransform;
    public CommodityPriceFeedCommand(Commodities commodities, IDateTransform dateTransform)
    {
        Commodities = commodities ?? throw new ArgumentNullException(nameof(commodities));
        var lastUpdate = Commodities.lastUpdated;
        _dateTransform = dateTransform ?? throw new ArgumentNullException(nameof(dateTransform));
        Commodities.commodityData.ForEach(x =>
        {
            x.CreatedDate = DateTime.Now;
            x.LastUpdated = lastUpdate;
            x.ContractDate = _dateTransform.MonthYearToDate1st(x.Contract).Result;
        });
    }
}
