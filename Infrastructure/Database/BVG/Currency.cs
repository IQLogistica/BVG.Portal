using System;
using System.Collections.Generic;

namespace Infrastructure.Database.BVG
{
    public partial class Currency
    {
        public Currency()
        {
            CommodityHistories = new HashSet<CommodityHistory>();
        }

        public int Id { get; set; }
        public string? Label { get; set; }
        public int? Order { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<CommodityHistory> CommodityHistories { get; set; }
    }
}
