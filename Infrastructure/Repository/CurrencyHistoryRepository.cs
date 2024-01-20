using AutoMapper;
using CoreLogic.Contracts;
using CoreLogic.Models;
using Infrastructure.Database.BVG;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository;

public class CurrencyHistoryRepository : PostgreSQLRepository<CurrencyHistory>, ICurrencyHistoryRepository
{
    private readonly iql_bvgContext _context;
    private readonly IMapper _mapper;
    public CurrencyHistoryRepository(iql_bvgContext context, IMapper mapper) : base(context)
    {
        _context = context;
        _mapper = mapper;
    }

    public void Insert(List<CurrencyPrice> currencyPrice)
    {
        var currencyPairs = _context.CurrencyPairs.Where(x => x.IsActive == true).ToList();
        var entities = _mapper.Map<List<CurrencyPrice>, List<CurrencyHistory>>(currencyPrice, opts => opts.Items["CurrencyPairs"] = currencyPairs);
        _context.CurrencyHistories.AddRange(entities);
        _context.SaveChanges();
    }
}