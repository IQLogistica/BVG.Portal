using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Infrastructure.Database.BVG
{
    public partial class iql_bvgContext : DbContext
    {
        public iql_bvgContext()
        {
        }

        public iql_bvgContext(DbContextOptions<iql_bvgContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Commodity> Commodities { get; set; } = null!;
        public virtual DbSet<CommodityHistory> CommodityHistories { get; set; } = null!;
        public virtual DbSet<Currency> Currencies { get; set; } = null!;
        public virtual DbSet<CurrencyHistory> CurrencyHistories { get; set; } = null!;
        public virtual DbSet<CurrencyPair> CurrencyPairs { get; set; } = null!;
        public virtual DbSet<MappingTest> MappingTests { get; set; } = null!;
        public virtual DbSet<SceneDownloadHistory> SceneDownloadHistories { get; set; } = null!;
        public virtual DbSet<SceneMetadatum> SceneMetadata { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                throw new InvalidOperationException("Connection string is not configured.", new Exception("Stack trace"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Commodity>(entity =>
            {
                entity.ToTable("Commodity", "bvg");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.IsActive).HasColumnName("Is_Active");

                entity.Property(e => e.Label).HasMaxLength(6);
            });

            modelBuilder.Entity<CommodityHistory>(entity =>
            {
                entity.ToTable("CommodityHistory", "bvg");

                entity.HasIndex(e => new { e.CommodityId, e.Contract }, "CommIdContract");

                entity.HasIndex(e => e.LastUpdated, "LastUpdated");

                entity.Property(e => e.Contract).HasMaxLength(50);

                entity.Property(e => e.LastTradedPrice).HasColumnType("money");

                entity.Property(e => e.LastTradedTime).HasColumnType("timestamp without time zone");

                entity.Property(e => e.LastUpdated).HasColumnType("timestamp without time zone");

                entity.Property(e => e.OpeningPrice).HasColumnType("money");

                entity.Property(e => e.Volume).HasColumnType("money");

                entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone");

                entity.HasOne(d => d.Commodity)
                    .WithMany(p => p.CommodityHistories)
                    .HasForeignKey(d => d.CommodityId)
                    .HasConstraintName("CommodityHistory_CommodityId_fkey");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.CommodityHistories)
                    .HasForeignKey(d => d.CurrencyId)
                    .HasConstraintName("CommodityHistory_CurrencyId_fkey");
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.ToTable("Currency", "bvg");

                entity.Property(e => e.IsActive).HasColumnName("Is_Active");

                entity.Property(e => e.Label).HasMaxLength(3);
            });

            modelBuilder.Entity<CurrencyHistory>(entity =>
            {
                entity.ToTable("CurrencyHistory", "bvg");

                entity.HasIndex(e => e.CurrencyPairId, "CurrencyPairIdex");

                entity.HasIndex(e => e.TimeStampUnix, "TimestampUTC");

                entity.Property(e => e.Change).HasPrecision(37, 8);

                entity.Property(e => e.ChangePercent).HasPrecision(37, 8);

                entity.Property(e => e.EndRate).HasPrecision(37, 8);

                entity.Property(e => e.Rate).HasPrecision(37, 8);

                entity.Property(e => e.StartRate).HasPrecision(37, 8);

                entity.Property(e => e.TimeStamp).HasColumnType("timestamp without time zone");

                entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone");

                entity.HasOne(d => d.CurrencyPair)
                    .WithMany(p => p.CurrencyHistories)
                    .HasForeignKey(d => d.CurrencyPairId)
                    .HasConstraintName("CurrencyHistory_CurrencyPairId_fkey");
            });

            modelBuilder.Entity<CurrencyPair>(entity =>
            {
                entity.ToTable("CurrencyPair", "bvg");

                entity.Property(e => e.Display).HasMaxLength(20);

                entity.Property(e => e.IsActive).HasColumnName("Is_Active");

                entity.Property(e => e.Label).HasMaxLength(6);
            });

            modelBuilder.Entity<MappingTest>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("MappingTests", "bvg");

                entity.Property(e => e.Currency1)
                    .HasColumnType("money")
                    .HasColumnName("currency1");

                entity.Property(e => e.Currency2)
                    .HasColumnType("money[]")
                    .HasColumnName("currency2");

                entity.Property(e => e.Doubl31).HasColumnName("doubl31");

                entity.Property(e => e.Double2).HasColumnName("double2");

                entity.Property(e => e.Numeric1).HasColumnName("numeric1");

                entity.Property(e => e.Numeric2).HasColumnName("numeric2");
            });

            modelBuilder.Entity<SceneDownloadHistory>(entity =>
            {
                entity.ToTable("scene_download_history");

                entity.Property(e => e.SceneDownloadHistoryId).HasColumnName("scene_download_history_id");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("create_date");

                entity.Property(e => e.Href)
                    .HasMaxLength(255)
                    .HasColumnName("href");

                entity.Property(e => e.ProductId)
                    .HasMaxLength(255)
                    .HasColumnName("product_id");

                entity.Property(e => e.Title)
                    .HasMaxLength(10)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<SceneMetadatum>(entity =>
            {
                entity.HasKey(e => e.SceneMetadataId)
                    .HasName("scene_metadata_pkey");

                entity.ToTable("scene_metadata");

                entity.Property(e => e.SceneMetadataId).HasColumnName("scene_metadata_id");

                entity.Property(e => e.CloudCover)
                    .HasPrecision(10, 2)
                    .HasColumnName("cloud_cover");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("create_date");

                entity.Property(e => e.DataCoverage)
                    .HasPrecision(4, 1)
                    .HasColumnName("data_coverage");

                entity.Property(e => e.ProductId)
                    .HasMaxLength(255)
                    .HasColumnName("product_id");

                entity.Property(e => e.ProjEpsg).HasColumnName("proj_epsg");

                entity.Property(e => e.ProjShape).HasColumnName("proj_shape");

                entity.Property(e => e.ProjTransform)
                    .HasMaxLength(255)
                    .HasColumnName("proj_transform");

                entity.Property(e => e.RegionCode)
                    .HasMaxLength(5)
                    .HasColumnName("region_code");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
