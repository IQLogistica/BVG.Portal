using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLogic.Models;

public class Commodities
{
    public DateTime lastUpdated { get; set; }
    public List<CommodityData> commodityData { get; set; }
}
