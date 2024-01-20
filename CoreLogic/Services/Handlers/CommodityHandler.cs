using CoreLogic.Contracts;
using CoreLogic.Services.Commands;
using MediatR;

namespace CoreLogic.Services.Handlers;

public class CommodityHandler : IRequestHandler<CommodityPriceFeedCommand, Task>
{
    private readonly ICommodityHistoryRepository CommodityHistoryRepository;
    public CommodityHandler(ICommodityHistoryRepository commodityHistoryRepository)
    {
        CommodityHistoryRepository = commodityHistoryRepository;
    }

    public async Task<Task> Handle(CommodityPriceFeedCommand request, CancellationToken cancellationToken)
    {
        CommodityHistoryRepository.Insert(request.Commodities.commodityData);
        return Task.CompletedTask;
    }
}
