using System;
using System.Collections.Generic;

namespace Infrastructure.Database.BVG
{
    public partial class CurrencyHistory
    {
        public long Id { get; set; }
        public int? CurrencyPairId { get; set; }
        public decimal? Rate { get; set; }
        public decimal? StartRate { get; set; }
        public decimal? EndRate { get; set; }
        public decimal? Change { get; set; }
        public decimal? ChangePercent { get; set; }
        public int? TimeStampUnix { get; set; }
        public DateTime? TimeStamp { get; set; }

        public DateTime? CreatedDate { get; set; }

        public virtual CurrencyPair? CurrencyPair { get; set; }
    }
}
