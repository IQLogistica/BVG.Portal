using System;
using System.Collections.Generic;

namespace Infrastructure.Database.BVG
{
    public partial class CommodityHistory
    {
        public long Id { get; set; }
        public int? CommodityId { get; set; }
        public int? CurrencyId { get; set; }
        public string? Contract { get; set; }
        public decimal? OpeningPrice { get; set; }
        public DateTime? LastTradedTime { get; set; }
        public decimal? LastTradedPrice { get; set; }
        public decimal? Volume { get; set; }
        public DateTime? LastUpdated { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateOnly ContractDate {  get; set; }

        public virtual Commodity? Commodity { get; set; }
        public virtual Currency? Currency { get; set; }
    }
}
