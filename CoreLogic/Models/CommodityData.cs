using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLogic.Models;

public class CommodityData
{
    public string Instrument { get; set; }
    public int CommodityId { get; set; }
    public string Description { get; set; }
    public string Contract { get; set; }
    public decimal OpeningPrice { get; set; }
    public DateTime LastTradedTime { get; set; }
    public decimal LastTradedPrice { get; set; }
    public string HighPrice { get; set; }
    public string LowPrice { get; set; }
    public decimal Volume { get; set; }
    public string Delta { get; set; }
    public string MTM { get; set; }
    public string Volatility { get; set; }
    public string OpenInterest { get; set; }
    public string ContractSortOrder { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastUpdated { get; set; }
}
