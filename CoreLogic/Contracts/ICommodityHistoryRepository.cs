using CoreLogic.Models;

namespace CoreLogic.Contracts;

public interface ICommodityHistoryRepository
{
    void Insert(List<CommodityData> commodityData);
}
