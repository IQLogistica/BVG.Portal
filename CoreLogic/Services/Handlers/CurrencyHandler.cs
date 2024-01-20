using CoreLogic.Contracts;
using CoreLogic.Services.Commands;
using MediatR;

namespace CoreLogic.Services.Handlers;

public class CurrencyHandler : IRequestHandler<CurrencyPriceFeedCommand, Task>
{
    private readonly ICurrencyHistoryRepository CurrencyHistoryRepository;
    public CurrencyHandler(ICurrencyHistoryRepository currencyHistoryRepository)
    {
        CurrencyHistoryRepository = currencyHistoryRepository;
    }
    public async Task<Task> Handle(CurrencyPriceFeedCommand request, CancellationToken cancellationToken)
    {
        CurrencyHistoryRepository.Insert(request.CurrencyPrice);

        return Task.CompletedTask;
    }
}
