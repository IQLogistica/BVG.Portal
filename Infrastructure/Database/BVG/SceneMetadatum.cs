using System;
using System.Collections.Generic;
using NpgsqlTypes;

namespace Infrastructure.Database.BVG
{
    public partial class SceneMetadatum
    {
        public int SceneMetadataId { get; set; }
        public string? ProductId { get; set; }
        public int? ProjEpsg { get; set; }
        public decimal? CloudCover { get; set; }
        public string? RegionCode { get; set; }
        public decimal? DataCoverage { get; set; }
        public NpgsqlPoint? ProjShape { get; set; }
        public string? ProjTransform { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
