using System;
using Microsoft.EntityFrameworkCore;

namespace QlThietBi.Models
{
    public static class QlThietBiContextSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DmDungChung>().HasData(
                new DmDungChung
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    NhomDanhMuc = "TRANG_THAI_TB",
                    Ma = "MOI_NHAP",
                    Ten = "Mới nhập",
                    SapXep = 1,
                    IsActive = true,
                    NgayKhoiTao = DateTime.UtcNow
                },
                new DmDungChung
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    NhomDanhMuc = "TRANG_THAI_TB",
                    Ma = "TRONG_KHO",
                    Ten = "Trong kho",
                    SapXep = 2,
                    IsActive = true,
                    NgayKhoiTao = DateTime.UtcNow
                },
                new DmDungChung
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                    NhomDanhMuc = "TRANG_THAI_TB",
                    Ma = "DANG_SU_DUNG",
                    Ten = "Đang sử dụng",
                    SapXep = 3,
                    IsActive = true,
                    NgayKhoiTao = DateTime.UtcNow
                },
                new DmDungChung
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                    NhomDanhMuc = "TRANG_THAI_TB",
                    Ma = "DANG_SUA_CHUA",
                    Ten = "Đang sửa chữa",
                    SapXep = 4,
                    IsActive = true,
                    NgayKhoiTao = DateTime.UtcNow
                },
                new DmDungChung
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                    NhomDanhMuc = "TRANG_THAI_TB",
                    Ma = "DANG_BAO_TRI",
                    Ten = "Đang bảo trì/bảo dưỡng",
                    SapXep = 5,
                    IsActive = true,
                    NgayKhoiTao = DateTime.UtcNow
                },
                new DmDungChung
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                    NhomDanhMuc = "TRANG_THAI_TB",
                    Ma = "CHO_THANH_LY",
                    Ten = "Chờ thanh lý",
                    SapXep = 6,
                    IsActive = true,
                    NgayKhoiTao = DateTime.UtcNow
                },
                new DmDungChung
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                    NhomDanhMuc = "TRANG_THAI_TB",
                    Ma = "DA_THANH_LY",
                    Ten = "Đã thanh lý",
                    SapXep = 7,
                    IsActive = true,
                    NgayKhoiTao = DateTime.UtcNow
                },
                new DmDungChung
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                    NhomDanhMuc = "TRANG_THAI_TB",
                    Ma = "MAT_HUY",
                    Ten = "Mất/Hủy",
                    SapXep = 8,
                    IsActive = true,
                    NgayKhoiTao = DateTime.UtcNow
                }
            );
        }
    }
}
