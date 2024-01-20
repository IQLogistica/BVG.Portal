using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLogic.Models;

public class Price
{
    public string id { get; set; }
    public string destination { get; set; }
    public string rate { get; set; }
    public string start_rate { get; set; }
    public string change { get; set; }
    public decimal change_pct { get; set; }
    public string timestamp { get; set; }
    public string timestamp_date { get; set; }
}