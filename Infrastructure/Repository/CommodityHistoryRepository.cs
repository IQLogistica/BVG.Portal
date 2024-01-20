using AutoMapper;
using CoreLogic.Contracts;
using CoreLogic.Models;
using Infrastructure.Database.BVG;

namespace Infrastructure.Repository;

public class CommodityHistoryRepository : PostgreSQLRepository<CommodityHistory>, ICommodityHistoryRepository
{
    private readonly iql_bvgContext _context;
    private readonly IMapper _mapper;
    public CommodityHistoryRepository(iql_bvgContext context, IMapper mapper) : base(context)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Insert(List<CommodityData> commodityData)
    {
        var commodities = _context.Commodities.Where(x => x.IsActive == true).ToList();
        var currencies = _context.Currencies.Where(x => x.IsActive == true).ToList();
        var entities = _mapper.Map<List<CommodityData>, List<CommodityHistory>>(commodityData, opts => {
            opts.Items["Commodities"] = commodities;
            opts.Items["Currencies"] = currencies;
            });
        _context.CommodityHistories.AddRange(entities);
        _context.SaveChanges();
    }
}