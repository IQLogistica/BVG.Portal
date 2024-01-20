using CoreLogic.Models;
using MediatR;

namespace CoreLogic.Services.Commands;

public class CurrencyPriceFeedCommand : IRequest<Task>
{
    public List<CurrencyPrice> CurrencyPrice { get; set; }
    public CurrencyPriceFeedCommand(List<CurrencyPrice> currencyPrice)
    {
        CurrencyPrice = currencyPrice ?? throw new ArgumentNullException(nameof(currencyPrice));
    }
}
