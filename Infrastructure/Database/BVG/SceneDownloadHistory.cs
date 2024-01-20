using System;
using System.Collections.Generic;

namespace Infrastructure.Database.BVG
{
    public partial class SceneDownloadHistory
    {
        public int SceneDownloadHistoryId { get; set; }
        public string? ProductId { get; set; }
        public string? Href { get; set; }
        public string? Title { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
