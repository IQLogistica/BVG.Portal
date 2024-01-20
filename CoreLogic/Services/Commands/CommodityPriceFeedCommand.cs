using CoreLogic.Models;
using MediatR;

namespace CoreLogic.Services.Commands;

public class CommodityPriceFeedCommand : IRequest<Task>
{
    public Commodities Commodities { get; set; }
    public CommodityPriceFeedCommand(Commodities commodities)
    {
        Commodities = commodities ?? throw new ArgumentNullException(nameof(commodities));
        var lastUpdate = Commodities.lastUpdated;
        Commodities.commodityData.ForEach(x =>
        {
            x.CreatedDate = DateTime.Now;
            x.LastUpdated = lastUpdate;
        });
    }
}
