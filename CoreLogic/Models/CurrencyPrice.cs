using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLogic.Models;

public class CurrencyPrice
{
    public int id { get; set; }
    public string code { get; set; }
    public string order { get; set; }
    public int show { get; set; }
    public int @default { get; set; }
    public int currency_id { get; set; }
    public string created_at { get; set; }
    public string updated_at { get; set; }
    public string deleted_at { get; set; }
    public string symbol { get; set; }
    public List<Price> prices { get; set; }
    public Change change { get; set; }

    public DateTime? CreatedDate { get; set; }
}
