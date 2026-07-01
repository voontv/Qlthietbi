using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QlThietBi.AutoConfig;
using QlThietBi.DTO.Request;
using QlThietBi.DTO.Response;
using QlThietBi.Models;
using ThietBiModel = QlThietBi.Models.ThietBi;

namespace QlThietBi.Businesses.ThietBi
{
    public class ThietBiBusiness : IThietBiBusiness
    {
        private readonly QlThietBiContext context;

        public ThietBiBusiness(QlThietBiContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<DmDungChungDto>> LayDanhMucDungChungAsync(string nhomDanhMuc)
        {
            return await context.DmDungChungs
                .Where(x => x.NhomDanhMuc == nhomDanhMuc && x.IsActive)
                .OrderBy(x => x.SapXep)
                .ThenBy(x => x.Ten)
                .Select(x => new DmDungChungDto
                {
                    Id = x.Id,
                    NhomDanhMuc = x.NhomDanhMuc,
                    Ma = x.Ma,
                    Ten = x.Ten,
                    GhiChu = x.GhiChu,
                    SapXep = x.SapXep,
                    IsActive = x.IsActive
                })
                .ToListAsync();
        }

        public async Task<DmDungChungDto> LuuDanhMucDungChungAsync(CreateUpdateDmDungChungRequest request)
        {
            var duplicate = await context.DmDungChungs
                .AnyAsync(x => x.NhomDanhMuc == request.NhomDanhMuc && x.Ma == request.Ma && x.Id != request.Id);

            if (duplicate)
            {
                throw new InvalidOperationException($"Danh mục '{request.Ma}' đã tồn tại trong nhóm '{request.NhomDanhMuc}'.");
            }

            DmDungChung entity;

            if (request.Id.HasValue)
            {
                entity = await context.DmDungChungs.FindAsync(request.Id.Value) ?? throw new KeyNotFoundException("Danh mục không tồn tại.");
                entity.NhomDanhMuc = request.NhomDanhMuc;
                entity.Ma = request.Ma;
                entity.Ten = request.Ten;
                entity.GhiChu = request.GhiChu;
                entity.SapXep = request.SapXep;
                entity.IsActive = request.IsActive;
                entity.NgayChinhSuaCuoiCung = DateTime.UtcNow;
            }
            else
            {
                entity = new DmDungChung
                {
                    Id = Guid.NewGuid(),
                    NhomDanhMuc = request.NhomDanhMuc,
                    Ma = request.Ma,
                    Ten = request.Ten,
                    GhiChu = request.GhiChu,
                    SapXep = request.SapXep,
                    IsActive = request.IsActive,
                    NgayKhoiTao = DateTime.UtcNow
                };
                context.DmDungChungs.Add(entity);
            }

            await context.SaveChangesAsync();

            return new DmDungChungDto
            {
                Id = entity.Id,
                NhomDanhMuc = entity.NhomDanhMuc,
                Ma = entity.Ma,
                Ten = entity.Ten,
                GhiChu = entity.GhiChu,
                SapXep = entity.SapXep,
                IsActive = entity.IsActive
            };
        }

        public async Task<bool> XoaDanhMucDungChungAsync(Guid id)
        {
            var entity = await context.DmDungChungs.FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            entity.IsActive = false;
            entity.NgayChinhSuaCuoiCung = DateTime.UtcNow;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<NhomThietBiDto>> LayDanhSachNhomThietBiAsync()
        {
            return await context.NhomThietBis
                .Where(x => x.IsActive)
                .OrderBy(x => x.SapXep)
                .ThenBy(x => x.TenNhomThietBi)
                .Select(x => new NhomThietBiDto
                {
                    Id = x.Id,
                    MaNhomThietBi = x.MaNhomThietBi,
                    TenNhomThietBi = x.TenNhomThietBi,
                    KyHieu = x.KyHieu,
                    ParentId = x.ParentId,
                    MoTa = x.MoTa,
                    SapXep = x.SapXep,
                    IsActive = x.IsActive
                })
                .ToListAsync();
        }

        public async Task<NhomThietBiDto> LuuNhomThietBiAsync(CreateUpdateNhomThietBiRequest request)
        {
            var duplicate = await context.NhomThietBis
                .AnyAsync(x => x.MaNhomThietBi == request.MaNhomThietBi && x.Id != request.Id);

            if (duplicate)
            {
                throw new InvalidOperationException($"Nhóm thiết bị '{request.MaNhomThietBi}' đã tồn tại.");
            }

            NhomThietBi entity;

            if (request.Id.HasValue)
            {
                entity = await context.NhomThietBis.FindAsync(request.Id.Value) ?? throw new KeyNotFoundException("Nhóm thiết bị không tồn tại.");
                entity.MaNhomThietBi = request.MaNhomThietBi;
                entity.TenNhomThietBi = request.TenNhomThietBi;
                entity.KyHieu = request.KyHieu;
                entity.ParentId = request.ParentId;
                entity.MoTa = request.MoTa;
                entity.SapXep = request.SapXep;
                entity.IsActive = request.IsActive;
                entity.NgayChinhSuaCuoiCung = DateTime.UtcNow;
            }
            else
            {
                entity = new NhomThietBi
                {
                    Id = Guid.NewGuid(),
                    MaNhomThietBi = request.MaNhomThietBi,
                    TenNhomThietBi = request.TenNhomThietBi,
                    KyHieu = request.KyHieu,
                    ParentId = request.ParentId,
                    MoTa = request.MoTa,
                    SapXep = request.SapXep,
                    IsActive = request.IsActive,
                    NgayKhoiTao = DateTime.UtcNow
                };
                context.NhomThietBis.Add(entity);
            }

            await context.SaveChangesAsync();

            return new NhomThietBiDto
            {
                Id = entity.Id,
                MaNhomThietBi = entity.MaNhomThietBi,
                TenNhomThietBi = entity.TenNhomThietBi,
                KyHieu = entity.KyHieu,
                ParentId = entity.ParentId,
                MoTa = entity.MoTa,
                SapXep = entity.SapXep,
                IsActive = entity.IsActive
            };
        }

        public async Task<bool> XoaNhomThietBiAsync(Guid id)
        {
            var entity = await context.NhomThietBis.FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            entity.IsActive = false;
            entity.NgayChinhSuaCuoiCung = DateTime.UtcNow;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ThietBiDto>> LayDanhSachThietBiAsync()
        {
            var devices = await context.ThietBis
                .Where(x => x.IsActive)
                .OrderBy(x => x.MaThietBi)
                .ToListAsync();

            return devices.Select(x => new ThietBiDto
            {
                Id = x.Id,
                MaThietBi = x.MaThietBi,
                MaThietBiCu = x.MaThietBiCu,
                TenThietBi = x.TenThietBi,
                SoSerial = x.SoSerial,
                Model = x.Model,
                MaKeToan = x.MaKeToan,
                MaThietBiCha = x.MaThietBiCha,
                NhomThietBiId = x.NhomThietBiId,
                TrangThaiId = x.TrangThaiId,
                TrangThaiKiemKeId = x.TrangThaiKiemKeId,
                DonViTinhId = x.DonViTinhId,
                NhanHieuId = x.NhanHieuId,
                MauSacId = x.MauSacId,
                NuocSanXuatId = x.NuocSanXuatId,
                ChatLieuId = x.ChatLieuId,
                DonViCungCapId = x.DonViCungCapId,
                PhongBanId = x.PhongBanId,
                BoPhanId = x.BoPhanId,
                NguoiSuDungId = x.NguoiSuDungId,
                NgayMua = x.NgayMua,
                NgayNhapThietBi = x.NgayNhapThietBi,
                NgayDuaVaoSuDung = x.NgayDuaVaoSuDung,
                NguyenGia = x.NguyenGia,
                ThoiGianBaoHanh = x.ThoiGianBaoHanh,
                NgayHetBaoHanh = x.NgayHetBaoHanh,
                MaQrCode = x.MaQrCode,
                ViTriLapDat = x.ViTriLapDat,
                GhiChu = x.GhiChu,
                IsActive = x.IsActive
            }).ToList();
        }

        public async Task<ThietBiDto> LuuThietBiAsync(CreateUpdateThietBiRequest request)
        {
            var duplicate = await context.ThietBis
                .AnyAsync(x => x.MaThietBi == request.MaThietBi && x.Id != request.Id);

            if (duplicate)
            {
                throw new InvalidOperationException($"Mã thiết bị '{request.MaThietBi}' đã tồn tại.");
            }

            ThietBiModel entity;
            var now = DateTime.UtcNow;

            if (request.Id.HasValue)
            {
                entity = await context.ThietBis.FindAsync(request.Id.Value) ?? throw new KeyNotFoundException("Thiết bị không tồn tại.");
                var originalTrangThaiId = entity.TrangThaiId;
                var originalPhongBanId = entity.PhongBanId;
                var originalBoPhanId = entity.BoPhanId;
                var originalNguoiSuDungId = entity.NguoiSuDungId;

                entity.MaThietBi = request.MaThietBi;
                entity.MaThietBiCu = request.MaThietBiCu;
                entity.TenThietBi = request.TenThietBi;
                entity.SoSerial = request.SoSerial;
                entity.Model = request.Model;
                entity.MaKeToan = request.MaKeToan;
                entity.MaThietBiCha = request.MaThietBiCha;
                entity.NhomThietBiId = request.NhomThietBiId;
                entity.TrangThaiId = request.TrangThaiId;
                entity.TrangThaiKiemKeId = request.TrangThaiKiemKeId;
                entity.DonViTinhId = request.DonViTinhId;
                entity.NhanHieuId = request.NhanHieuId;
                entity.MauSacId = request.MauSacId;
                entity.NuocSanXuatId = request.NuocSanXuatId;
                entity.ChatLieuId = request.ChatLieuId;
                entity.DonViCungCapId = request.DonViCungCapId;
                entity.PhongBanId = request.PhongBanId;
                entity.BoPhanId = request.BoPhanId;
                entity.NguoiSuDungId = request.NguoiSuDungId;
                entity.NgayMua = request.NgayMua;
                entity.NgayNhapThietBi = request.NgayNhapThietBi;
                entity.NgayDuaVaoSuDung = request.NgayDuaVaoSuDung;
                entity.NguyenGia = request.NguyenGia;
                entity.ThoiGianBaoHanh = request.ThoiGianBaoHanh;
                entity.NgayHetBaoHanh = request.NgayHetBaoHanh;
                entity.MaQrCode = request.MaQrCode;
                entity.ViTriLapDat = request.ViTriLapDat;
                entity.GhiChu = request.GhiChu;
                entity.IsActive = request.IsActive;
                entity.NgayChinhSuaCuoiCung = now;

                if (originalTrangThaiId != request.TrangThaiId || originalPhongBanId != request.PhongBanId || originalBoPhanId != request.BoPhanId || originalNguoiSuDungId != request.NguoiSuDungId)
                {
                    await TaoLichSuThietBiAsync(entity, request, originalTrangThaiId, originalPhongBanId, originalBoPhanId, originalNguoiSuDungId, now);
                }
            }
            else
            {
                entity = new ThietBiModel
                {
                    Id = Guid.NewGuid(),
                    MaThietBi = request.MaThietBi,
                    MaThietBiCu = request.MaThietBiCu,
                    TenThietBi = request.TenThietBi,
                    SoSerial = request.SoSerial,
                    Model = request.Model,
                    MaKeToan = request.MaKeToan,
                    MaThietBiCha = request.MaThietBiCha,
                    NhomThietBiId = request.NhomThietBiId,
                    TrangThaiId = request.TrangThaiId,
                    TrangThaiKiemKeId = request.TrangThaiKiemKeId,
                    DonViTinhId = request.DonViTinhId,
                    NhanHieuId = request.NhanHieuId,
                    MauSacId = request.MauSacId,
                    NuocSanXuatId = request.NuocSanXuatId,
                    ChatLieuId = request.ChatLieuId,
                    DonViCungCapId = request.DonViCungCapId,
                    PhongBanId = request.PhongBanId,
                    BoPhanId = request.BoPhanId,
                    NguoiSuDungId = request.NguoiSuDungId,
                    NgayMua = request.NgayMua,
                    NgayNhapThietBi = request.NgayNhapThietBi,
                    NgayDuaVaoSuDung = request.NgayDuaVaoSuDung,
                    NguyenGia = request.NguyenGia,
                    ThoiGianBaoHanh = request.ThoiGianBaoHanh,
                    NgayHetBaoHanh = request.NgayHetBaoHanh,
                    MaQrCode = request.MaQrCode,
                    ViTriLapDat = request.ViTriLapDat,
                    GhiChu = request.GhiChu,
                    IsActive = request.IsActive,
                    NgayKhoiTao = now
                };

                context.ThietBis.Add(entity);
                await TaoLichSuThietBiAsync(entity, request, null, null, null, null, now, "NHAP_KHO");
            }

            await context.SaveChangesAsync();
            await LuuThongSoCuaThietBiAsync(entity.Id, request.ThongSo, now);

            return await LayThietBiTheoIdAsync(entity.Id) ?? throw new InvalidOperationException("Không thể lấy thiết bị sau khi lưu.");
        }

        public async Task<ThietBiDto?> LayThietBiTheoIdAsync(Guid id)
        {
            var x = await context.ThietBis.FirstOrDefaultAsync(t => t.Id == id && t.IsActive);
            if (x == null)
            {
                return null;
            }

            var result = new ThietBiDto
            {
                Id = x.Id,
                MaThietBi = x.MaThietBi,
                MaThietBiCu = x.MaThietBiCu,
                TenThietBi = x.TenThietBi,
                SoSerial = x.SoSerial,
                Model = x.Model,
                MaKeToan = x.MaKeToan,
                MaThietBiCha = x.MaThietBiCha,
                NhomThietBiId = x.NhomThietBiId,
                TrangThaiId = x.TrangThaiId,
                TrangThaiKiemKeId = x.TrangThaiKiemKeId,
                DonViTinhId = x.DonViTinhId,
                NhanHieuId = x.NhanHieuId,
                MauSacId = x.MauSacId,
                NuocSanXuatId = x.NuocSanXuatId,
                ChatLieuId = x.ChatLieuId,
                DonViCungCapId = x.DonViCungCapId,
                PhongBanId = x.PhongBanId,
                BoPhanId = x.BoPhanId,
                NguoiSuDungId = x.NguoiSuDungId,
                NgayMua = x.NgayMua,
                NgayNhapThietBi = x.NgayNhapThietBi,
                NgayDuaVaoSuDung = x.NgayDuaVaoSuDung,
                NguyenGia = x.NguyenGia,
                ThoiGianBaoHanh = x.ThoiGianBaoHanh,
                NgayHetBaoHanh = x.NgayHetBaoHanh,
                MaQrCode = x.MaQrCode,
                ViTriLapDat = x.ViTriLapDat,
                GhiChu = x.GhiChu,
                IsActive = x.IsActive,
                ThongSo = await context.ThietBiThongSos
                    .Where(t => t.ThietBiId == x.Id)
                    .Select(t => new ThietBiThongSoDto
                    {
                        Id = t.Id,
                        ThietBiId = t.ThietBiId,
                        ThongSoId = t.ThongSoId,
                        GiaTriText = t.GiaTriText,
                        GiaTriNumber = t.GiaTriNumber,
                        GiaTriDate = t.GiaTriDate,
                        GiaTriBit = t.GiaTriBit,
                        GhiChu = t.GhiChu
                    })
                    .ToListAsync()
            };

            return result;
        }

        public async Task<bool> XoaThietBiAsync(Guid id)
        {
            var entity = await context.ThietBis.FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            entity.IsActive = false;
            entity.NgayChinhSuaCuoiCung = DateTime.UtcNow;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<DmThongSoThietBiDto>> LayThongSoTheoNhomThietBiAsync(Guid nhomThietBiId)
        {
            return await context.DmThongSoThietBis
                .Where(x => x.NhomThietBiId == nhomThietBiId && x.IsActive)
                .OrderBy(x => x.SapXep)
                .ThenBy(x => x.TenThongSo)
                .Select(x => new DmThongSoThietBiDto
                {
                    Id = x.Id,
                    NhomThietBiId = x.NhomThietBiId,
                    MaThongSo = x.MaThongSo,
                    TenThongSo = x.TenThongSo,
                    KieuDuLieu = x.KieuDuLieu,
                    DonViTinhId = x.DonViTinhId,
                    BatBuoc = x.BatBuoc,
                    SapXep = x.SapXep,
                    IsActive = x.IsActive
                })
                .ToListAsync();
        }

        public async Task<DmThongSoThietBiDto> LuuThongSoThietBiAsync(CreateUpdateDmThongSoThietBiRequest request)
        {
            var duplicate = await context.DmThongSoThietBis
                .AnyAsync(x => x.NhomThietBiId == request.NhomThietBiId && x.MaThongSo == request.MaThongSo && x.Id != request.Id);

            if (duplicate)
            {
                throw new InvalidOperationException($"Thông số '{request.MaThongSo}' đã tồn tại trong nhóm thiết bị.");
            }

            DmThongSoThietBi entity;
            if (request.Id.HasValue)
            {
                entity = await context.DmThongSoThietBis.FindAsync(request.Id.Value) ?? throw new KeyNotFoundException("Thông số thiết bị không tồn tại.");
                entity.NhomThietBiId = request.NhomThietBiId;
                entity.MaThongSo = request.MaThongSo;
                entity.TenThongSo = request.TenThongSo;
                entity.KieuDuLieu = request.KieuDuLieu;
                entity.DonViTinhId = request.DonViTinhId;
                entity.BatBuoc = request.BatBuoc;
                entity.SapXep = request.SapXep;
                entity.IsActive = request.IsActive;
                entity.GhiChu = request.GhiChu;
                entity.NgayChinhSuaCuoiCung = DateTime.UtcNow;
            }
            else
            {
                entity = new DmThongSoThietBi
                {
                    Id = Guid.NewGuid(),
                    NhomThietBiId = request.NhomThietBiId,
                    MaThongSo = request.MaThongSo,
                    TenThongSo = request.TenThongSo,
                    KieuDuLieu = request.KieuDuLieu,
                    DonViTinhId = request.DonViTinhId,
                    BatBuoc = request.BatBuoc,
                    SapXep = request.SapXep,
                    IsActive = request.IsActive,
                    GhiChu = request.GhiChu,
                    NgayKhoiTao = DateTime.UtcNow
                };
                context.DmThongSoThietBis.Add(entity);
            }

            await context.SaveChangesAsync();

            return new DmThongSoThietBiDto
            {
                Id = entity.Id,
                NhomThietBiId = entity.NhomThietBiId,
                MaThongSo = entity.MaThongSo,
                TenThongSo = entity.TenThongSo,
                KieuDuLieu = entity.KieuDuLieu,
                DonViTinhId = entity.DonViTinhId,
                BatBuoc = entity.BatBuoc,
                SapXep = entity.SapXep,
                IsActive = entity.IsActive
            };
        }

        public async Task<bool> XoaThongSoThietBiAsync(Guid id)
        {
            var entity = await context.DmThongSoThietBis.FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            entity.IsActive = false;
            entity.NgayChinhSuaCuoiCung = DateTime.UtcNow;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<PhieuThietBiDto> TaoPhieuThietBiAsync(CreatePhieuThietBiRequest request)
        {
            var thietBi = await context.ThietBis.FindAsync(request.ThietBiId) ?? throw new KeyNotFoundException("Thiết bị không tồn tại.");
            var now = DateTime.UtcNow;
            var phieu = new PhieuThietBi
            {
                Id = Guid.NewGuid(),
                SoPhieu = request.SoPhieu,
                LoaiPhieu = request.LoaiPhieu,
                NgayPhieu = request.NgayPhieu,
                ThietBiId = request.ThietBiId,
                PhongBanId = request.PhongBanId,
                BoPhanId = request.BoPhanId,
                NguoiSuDungId = request.NguoiSuDungId,
                DonViThucHienId = request.DonViThucHienId,
                KetLuanId = request.KetLuanId,
                NoiDung = request.NoiDung,
                ChiPhi = request.ChiPhi,
                FileScan01 = request.FileScan01,
                FileScan02 = request.FileScan02,
                GhiChu = request.GhiChu,
                IsActive = true,
                NgayKhoiTao = now
            };

            context.PhieuThietBis.Add(phieu);

            if (request.ChiTiets != null)
            {
                foreach (var detail in request.ChiTiets)
                {
                    var detailEntry = new PhieuThietBiChiTiet
                    {
                        Id = Guid.NewGuid(),
                        PhieuThietBiId = phieu.Id,
                        CongViecId = detail.CongViecId,
                        ThongSoId = detail.ThongSoId,
                        NoiDung = detail.NoiDung,
                        GiaTri = detail.GiaTri,
                        ChiPhi = detail.ChiPhi,
                        NgayBatDau = detail.NgayBatDau,
                        NgayKetThuc = detail.NgayKetThuc,
                        GhiChu = detail.GhiChu,
                        NgayKhoiTao = now
                    };

                    context.PhieuThietBiChiTiets.Add(detailEntry);
                    phieu.ChiTiets.Add(detailEntry);
                }
            }

            var originalTrangThaiId = thietBi.TrangThaiId;
            var originalPhongBanId = thietBi.PhongBanId;
            var originalBoPhanId = thietBi.BoPhanId;
            var originalNguoiSuDungId = thietBi.NguoiSuDungId;

            if (!string.IsNullOrWhiteSpace(request.LoaiPhieu))
            {
                switch (request.LoaiPhieu.Trim().ToUpper())
                {
                    case "CAP_PHAT":
                        thietBi.TrangThaiId = await LayTrangThaiIdAsync("TRANG_THAI_TB", "DANG_SU_DUNG");
                        thietBi.PhongBanId = request.PhongBanId;
                        thietBi.BoPhanId = request.BoPhanId;
                        thietBi.NguoiSuDungId = request.NguoiSuDungId;
                        break;
                    case "LUAN_CHUYEN":
                        thietBi.PhongBanId = request.PhongBanId;
                        thietBi.BoPhanId = request.BoPhanId;
                        thietBi.NguoiSuDungId = request.NguoiSuDungId;
                        break;
                    case "THU_HOI":
                        thietBi.TrangThaiId = await LayTrangThaiIdAsync("TRANG_THAI_TB", "TRONG_KHO");
                        thietBi.PhongBanId = null;
                        thietBi.BoPhanId = null;
                        thietBi.NguoiSuDungId = null;
                        break;
                    case "SUA_CHUA":
                        thietBi.TrangThaiId = await LayTrangThaiIdAsync("TRANG_THAI_TB", "DANG_SUA_CHUA");
                        break;
                    case "BAO_TRI":
                        thietBi.TrangThaiId = await LayTrangThaiIdAsync("TRANG_THAI_TB", "DANG_BAO_TRI");
                        break;
                    case "THANH_LY":
                        thietBi.TrangThaiId = await LayTrangThaiIdAsync("TRANG_THAI_TB", "DA_THANH_LY");
                        break;
                }
            }

            thietBi.NgayChinhSuaCuoiCung = now;
            await TaoLichSuThietBiAsync(thietBi, new CreateUpdateThietBiRequest
            {
                MaThietBi = thietBi.MaThietBi,
                TenThietBi = thietBi.TenThietBi,
                NhomThietBiId = thietBi.NhomThietBiId,
                TrangThaiId = thietBi.TrangThaiId,
                PhongBanId = thietBi.PhongBanId,
                BoPhanId = thietBi.BoPhanId,
                NguoiSuDungId = thietBi.NguoiSuDungId,
                GhiChu = request.NoiDung
            }, originalTrangThaiId, originalPhongBanId, originalBoPhanId, originalNguoiSuDungId, now, request.LoaiPhieu, phieu.Id);

            await context.SaveChangesAsync();

            return new PhieuThietBiDto
            {
                Id = phieu.Id,
                SoPhieu = phieu.SoPhieu,
                LoaiPhieu = phieu.LoaiPhieu,
                NgayPhieu = phieu.NgayPhieu,
                ThietBiId = phieu.ThietBiId,
                PhongBanId = phieu.PhongBanId,
                BoPhanId = phieu.BoPhanId,
                NguoiSuDungId = phieu.NguoiSuDungId,
                DonViThucHienId = phieu.DonViThucHienId,
                KetLuanId = phieu.KetLuanId,
                NoiDung = phieu.NoiDung,
                ChiPhi = phieu.ChiPhi,
                FileScan01 = phieu.FileScan01,
                FileScan02 = phieu.FileScan02,
                GhiChu = phieu.GhiChu,
                ChiTiets = phieu.ChiTiets?.Select(c => new PhieuThietBiChiTietDto
                {
                    Id = c.Id,
                    PhieuThietBiId = c.PhieuThietBiId,
                    CongViecId = c.CongViecId,
                    ThongSoId = c.ThongSoId,
                    NoiDung = c.NoiDung,
                    GiaTri = c.GiaTri,
                    ChiPhi = c.ChiPhi,
                    NgayBatDau = c.NgayBatDau,
                    NgayKetThuc = c.NgayKetThuc,
                    GhiChu = c.GhiChu
                }).ToList()
            };
        }

        public async Task<IEnumerable<LichSuThietBiDto>> LayLichSuThietBiAsync(Guid thietBiId)
        {
            return await context.LichSuThietBis
                .Where(x => x.ThietBiId == thietBiId)
                .OrderByDescending(x => x.NgayKhoiTao)
                .Select(x => new LichSuThietBiDto
                {
                    Id = x.Id,
                    ThietBiId = x.ThietBiId,
                    LoaiNghiepVu = x.LoaiNghiepVu,
                    NghiepVuId = x.NghiepVuId,
                    TrangThaiTruocId = x.TrangThaiTruocId,
                    TrangThaiSauId = x.TrangThaiSauId,
                    PhongBanTruocId = x.PhongBanTruocId,
                    PhongBanSauId = x.PhongBanSauId,
                    BoPhanTruocId = x.BoPhanTruocId,
                    BoPhanSauId = x.BoPhanSauId,
                    NguoiSuDungTruocId = x.NguoiSuDungTruocId,
                    NguoiSuDungSauId = x.NguoiSuDungSauId,
                    NoiDung = x.NoiDung,
                    NgayPhatSinh = x.NgayPhatSinh
                })
                .ToListAsync();
        }

        private async Task LuuThongSoCuaThietBiAsync(Guid thietBiId, IEnumerable<ThietBiThongSoValueRequest>? values, DateTime now)
        {
            if (values == null)
            {
                return;
            }

            foreach (var item in values)
            {
                var existing = await context.ThietBiThongSos
                    .FirstOrDefaultAsync(x => x.ThietBiId == thietBiId && x.ThongSoId == item.ThongSoId);

                if (existing == null)
                {
                    context.ThietBiThongSos.Add(new ThietBiThongSo
                    {
                        Id = Guid.NewGuid(),
                        ThietBiId = thietBiId,
                        ThongSoId = item.ThongSoId,
                        GiaTriText = item.GiaTriText,
                        GiaTriNumber = item.GiaTriNumber,
                        GiaTriDate = item.GiaTriDate,
                        GiaTriBit = item.GiaTriBit,
                        GhiChu = item.GhiChu,
                        NgayKhoiTao = now
                    });
                }
                else
                {
                    existing.GiaTriText = item.GiaTriText;
                    existing.GiaTriNumber = item.GiaTriNumber;
                    existing.GiaTriDate = item.GiaTriDate;
                    existing.GiaTriBit = item.GiaTriBit;
                    existing.GhiChu = item.GhiChu;
                }
            }

            await context.SaveChangesAsync();
        }

        private Task TaoLichSuThietBiAsync(ThietBiModel current, CreateUpdateThietBiRequest request, Guid? trangThaiTruocId, Guid? phongBanTruocId, Guid? boPhanTruocId, Guid? nguoiSuDungTruocId, DateTime now, string loaiNghiepVu = "CAP_NHAT_THIET_BI", Guid? nghiepVuId = null)
        {
            var history = new LichSuThietBi
            {
                Id = Guid.NewGuid(),
                ThietBiId = current.Id,
                LoaiNghiepVu = loaiNghiepVu,
                NghiepVuId = nghiepVuId ?? current.Id,
                TrangThaiTruocId = trangThaiTruocId,
                TrangThaiSauId = current.TrangThaiId,
                PhongBanTruocId = phongBanTruocId,
                PhongBanSauId = current.PhongBanId,
                BoPhanTruocId = boPhanTruocId,
                BoPhanSauId = current.BoPhanId,
                NguoiSuDungTruocId = nguoiSuDungTruocId,
                NguoiSuDungSauId = current.NguoiSuDungId,
                NoiDung = request.GhiChu,
                NgayPhatSinh = now,
                NgayKhoiTao = now
            };

            context.LichSuThietBis.Add(history);
            return Task.CompletedTask;
        }

        private async Task<Guid> LayTrangThaiIdAsync(string nhomDanhMuc, string ma)
        {
            var status = await context.DmDungChungs.FirstOrDefaultAsync(x => x.NhomDanhMuc == nhomDanhMuc && x.Ma == ma && x.IsActive);
            if (status == null)
            {
                throw new KeyNotFoundException($"Không tìm thấy trạng thái '{ma}' trong nhóm '{nhomDanhMuc}'.");
            }

            return status.Id;
        }
    }
}
