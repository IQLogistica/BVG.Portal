using CoreLogic.Models;

namespace CoreLogic.Contracts;

public interface ICurrencyHistoryRepository
{
    void Insert(List<CurrencyPrice> currencyPrice);
}
