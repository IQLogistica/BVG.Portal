using System;
using System.Collections.Generic;

namespace Infrastructure.Database.BVG
{
    public partial class CurrencyPair
    {
        public CurrencyPair()
        {
            CurrencyHistories = new HashSet<CurrencyHistory>();
        }

        public int Id { get; set; }
        public string? Label { get; set; }
        public int? Order { get; set; }
        public bool? IsActive { get; set; }
        public string? Display { get; set; }

        public virtual ICollection<CurrencyHistory> CurrencyHistories { get; set; }
    }
}
