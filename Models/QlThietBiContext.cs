using Microsoft.EntityFrameworkCore;
using QlThietBi.Models;

namespace QlThietBi.Models
{
    public class QlThietBiContext : DbContext
    {
        public QlThietBiContext(DbContextOptions<QlThietBiContext> options)
            : base(options)
        {
        }

        public DbSet<DmDungChung> DmDungChungs { get; set; } = null!;
        public DbSet<DonViBoPhan> DonViBoPhans { get; set; } = null!;
        public DbSet<NguoiSuDungThietBi> NguoiSuDungThietBis { get; set; } = null!;
        public DbSet<NhomThietBi> NhomThietBis { get; set; } = null!;
        public DbSet<ThietBi> ThietBis { get; set; } = null!;
        public DbSet<DmThongSoThietBi> DmThongSoThietBis { get; set; } = null!;
        public DbSet<ThietBiThongSo> ThietBiThongSos { get; set; } = null!;
        public DbSet<PhieuThietBi> PhieuThietBis { get; set; } = null!;
        public DbSet<PhieuThietBiChiTiet> PhieuThietBiChiTiets { get; set; } = null!;
        public DbSet<LichSuThietBi> LichSuThietBis { get; set; } = null!;
        public DbSet<TepDinhKem> TepDinhKems { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            QlThietBiContextSeed.Seed(modelBuilder);
            modelBuilder.Entity<DmDungChung>(entity =>
            {
                entity.ToTable("DmDungChung");
                entity.HasIndex(e => new { e.NhomDanhMuc, e.Ma }).IsUnique().HasDatabaseName("IX_DmDungChung_Nhom_Ma");
                entity.Property(e => e.NhomDanhMuc).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Ma).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Ten).HasMaxLength(250).IsRequired();
                entity.Property(e => e.GhiChu).HasMaxLength(500);
            });

            modelBuilder.Entity<DonViBoPhan>(entity =>
            {
                entity.ToTable("DonViBoPhan");
                entity.Property(e => e.MaDonVi).HasMaxLength(50).IsRequired();
                entity.Property(e => e.TenDonVi).HasMaxLength(250).IsRequired();
                entity.Property(e => e.LoaiDonVi).HasMaxLength(50);
                entity.Property(e => e.GhiChu).HasMaxLength(500);
            });

            modelBuilder.Entity<NguoiSuDungThietBi>(entity =>
            {
                entity.ToTable("NguoiSuDungThietBi");
                entity.Property(e => e.MaNguoiDung).HasMaxLength(50).IsRequired();
                entity.Property(e => e.TenNguoiDung).HasMaxLength(250).IsRequired();
                entity.Property(e => e.ChucVu).HasMaxLength(250);
                entity.Property(e => e.SoDienThoai).HasMaxLength(50);
                entity.Property(e => e.Email).HasMaxLength(250);
                entity.Property(e => e.GhiChu).HasMaxLength(500);
            });

            modelBuilder.Entity<NhomThietBi>(entity =>
            {
                entity.ToTable("NhomThietBi");
                entity.HasIndex(e => e.MaNhomThietBi).IsUnique().HasDatabaseName("IX_NhomThietBi_Ma");
                entity.Property(e => e.MaNhomThietBi).HasMaxLength(50).IsRequired();
                entity.Property(e => e.TenNhomThietBi).HasMaxLength(250).IsRequired();
                entity.Property(e => e.KyHieu).HasMaxLength(20);
                entity.Property(e => e.MoTa).HasMaxLength(1000);
                entity.Property(e => e.GhiChu).HasMaxLength(500);
            });

            modelBuilder.Entity<ThietBi>(entity =>
            {
                entity.ToTable("ThietBi");
                entity.HasIndex(e => e.MaThietBi).IsUnique().HasDatabaseName("IX_ThietBi_MaThietBi");
                entity.Property(e => e.MaThietBi).HasMaxLength(50).IsRequired();
                entity.Property(e => e.MaThietBiCu).HasMaxLength(50);
                entity.Property(e => e.TenThietBi).HasMaxLength(500).IsRequired();
                entity.Property(e => e.SoSerial).HasMaxLength(100);
                entity.Property(e => e.Model).HasMaxLength(100);
                entity.Property(e => e.MaKeToan).HasMaxLength(50);
                entity.Property(e => e.MaThietBiCha).HasMaxLength(50);
                entity.Property(e => e.MaQrCode).HasMaxLength(100);
                entity.Property(e => e.ViTriLapDat).HasMaxLength(500);
                entity.Property(e => e.GhiChu).HasMaxLength(1000);
            });

            modelBuilder.Entity<DmThongSoThietBi>(entity =>
            {
                entity.ToTable("DmThongSoThietBi");
                entity.HasIndex(e => new { e.NhomThietBiId, e.MaThongSo }).IsUnique().HasDatabaseName("IX_DmThongSoThietBi_Nhom_Ma");
                entity.Property(e => e.MaThongSo).HasMaxLength(50).IsRequired();
                entity.Property(e => e.TenThongSo).HasMaxLength(250).IsRequired();
                entity.Property(e => e.KieuDuLieu).HasMaxLength(50).IsRequired();
                entity.Property(e => e.GhiChu).HasMaxLength(500);
            });

            modelBuilder.Entity<ThietBiThongSo>(entity =>
            {
                entity.ToTable("ThietBiThongSo");
                entity.HasIndex(e => new { e.ThietBiId, e.ThongSoId }).IsUnique().HasDatabaseName("IX_ThietBiThongSo_ThietBi_ThongSo");
                entity.Property(e => e.GiaTriText).HasMaxLength(1000);
                entity.Property(e => e.GhiChu).HasMaxLength(500);
            });

            modelBuilder.Entity<PhieuThietBi>(entity =>
            {
                entity.ToTable("PhieuThietBi");
                entity.Property(e => e.SoPhieu).HasMaxLength(50).IsRequired();
                entity.Property(e => e.LoaiPhieu).HasMaxLength(50).IsRequired();
                entity.Property(e => e.NoiDung).HasMaxLength(1000);
                entity.Property(e => e.FileScan01).HasMaxLength(500);
                entity.Property(e => e.FileScan02).HasMaxLength(500);
                entity.Property(e => e.GhiChu).HasMaxLength(1000);
            });

            modelBuilder.Entity<PhieuThietBiChiTiet>(entity =>
            {
                entity.ToTable("PhieuThietBiChiTiet");
                entity.Property(e => e.NoiDung).HasMaxLength(1000);
                entity.Property(e => e.GiaTri).HasMaxLength(1000);
                entity.Property(e => e.GhiChu).HasMaxLength(500);
                entity.HasOne(e => e.PhieuThietBi)
                    .WithMany(p => p.ChiTiets)
                    .HasForeignKey(e => e.PhieuThietBiId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<LichSuThietBi>(entity =>
            {
                entity.ToTable("LichSuThietBi");
                entity.Property(e => e.LoaiNghiepVu).HasMaxLength(50).IsRequired();
                entity.Property(e => e.NoiDung).HasMaxLength(1000);
            });

            modelBuilder.Entity<TepDinhKem>(entity =>
            {
                entity.ToTable("TepDinhKem");
                entity.Property(e => e.DoiTuongLoai).HasMaxLength(50).IsRequired();
                entity.Property(e => e.TenFile).HasMaxLength(500).IsRequired();
                entity.Property(e => e.DuongDan).HasMaxLength(1000).IsRequired();
                entity.Property(e => e.LoaiFile).HasMaxLength(50);
                entity.Property(e => e.GhiChu).HasMaxLength(500);
            });
        }
    }
}
