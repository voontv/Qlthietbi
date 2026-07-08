using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QlThietBi.Businesses;
using QlThietBi.AutoConfig;
using QlThietBi.DTO.Request;
using QlThietBi.DTO.Response;
using QlThietBi.Models;
using DonViBoPhanModel = QlThietBi.Models.DonViBoPhan;
using NguoiSuDungThietBiModel = QlThietBi.Models.NguoiSuDungThietBi;
using ThietBiModel = QlThietBi.Models.ThietBi;

namespace QlThietBi.Businesses.ThietBi
{
    public class ThietBiBusiness : IThietBiBusiness
    {
        private readonly QlThietBiContext context;
        private readonly IAuthenInfo authenInfo;

        public ThietBiBusiness(QlThietBiContext context, IAuthenInfo authenInfo)
        {
            this.context = context;
            this.authenInfo = authenInfo;
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
            var user = GetCurrentUser();
            if (string.IsNullOrWhiteSpace(request.NhomDanhMuc))
            {
                throw new InvalidOperationException("Thiếu nhóm danh mục.");
            }

            request.NhomDanhMuc = request.NhomDanhMuc.Trim().ToUpperInvariant();
            request.Ma = request.Ma.Trim();
            request.Ten = request.Ten.Trim();

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
                entity.MaNguoiChinhSua = user.MaNguoiDung;
                entity.TenNguoiChinhSua = user.TenNguoiDung;
            }
            else
            {
                entity = new DmDungChung
                {
                    NhomDanhMuc = request.NhomDanhMuc,
                    Ma = request.Ma,
                    Ten = request.Ten,
                    GhiChu = request.GhiChu,
                    SapXep = request.SapXep,
                    IsActive = request.IsActive,
                    NgayKhoiTao = DateTime.UtcNow,
                    MaNguoiNhap = user.MaNguoiDung,
                    TenNguoiNhap = user.TenNguoiDung
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

        public async Task<bool> XoaDanhMucDungChungAsync(int id)
        {
            var user = GetCurrentUser();
            var entity = await context.DmDungChungs.FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            entity.IsActive = false;
            entity.NgayChinhSuaCuoiCung = DateTime.UtcNow;
            entity.MaNguoiChinhSua = user.MaNguoiDung;
            entity.TenNguoiChinhSua = user.TenNguoiDung;
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
            var user = GetCurrentUser();
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
                entity.MaNguoiChinhSua = user.MaNguoiDung;
                entity.TenNguoiChinhSua = user.TenNguoiDung;
            }
            else
            {
                entity = new NhomThietBi
                {
                    MaNhomThietBi = request.MaNhomThietBi,
                    TenNhomThietBi = request.TenNhomThietBi,
                    KyHieu = request.KyHieu,
                    ParentId = request.ParentId,
                    MoTa = request.MoTa,
                    SapXep = request.SapXep,
                    IsActive = request.IsActive,
                    NgayKhoiTao = DateTime.UtcNow,
                    MaNguoiNhap = user.MaNguoiDung,
                    TenNguoiNhap = user.TenNguoiDung
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

        public async Task<bool> XoaNhomThietBiAsync(int id)
        {
            var user = GetCurrentUser();
            var entity = await context.NhomThietBis.FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            entity.IsActive = false;
            entity.NgayChinhSuaCuoiCung = DateTime.UtcNow;
            entity.MaNguoiChinhSua = user.MaNguoiDung;
            entity.TenNguoiChinhSua = user.TenNguoiDung;
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

        public async Task<ThongKeThietBiDto> ThongKeThietBiAsync(QueryThongKeThietBiRequest request)
        {
            var query = context.ThietBis
                .Where(x => x.IsActive);

            var maThietBi = request.MaThietBi?.Trim();
            if (!string.IsNullOrWhiteSpace(maThietBi))
            {
                query = query.Where(x =>
                    x.MaThietBi.Contains(maThietBi) ||
                    (x.MaThietBiCu != null && x.MaThietBiCu.Contains(maThietBi)));
            }

            if (request.PhongBanId.HasValue)
            {
                query = query.Where(x => x.PhongBanId == request.PhongBanId.Value);
            }

            if (request.BoPhanId.HasValue)
            {
                query = query.Where(x => x.BoPhanId == request.BoPhanId.Value);
            }

            if (request.NhomThietBiId.HasValue)
            {
                var nhomIds = await context.NhomThietBis
                    .Where(x => x.Id == request.NhomThietBiId.Value || x.ParentId == request.NhomThietBiId.Value)
                    .Select(x => x.Id)
                    .ToListAsync();

                if (!nhomIds.Contains(request.NhomThietBiId.Value))
                {
                    nhomIds.Add(request.NhomThietBiId.Value);
                }

                query = query.Where(x => nhomIds.Contains(x.NhomThietBiId));
            }

            if (request.NguoiSuDungId.HasValue)
            {
                query = query.Where(x => x.NguoiSuDungId == request.NguoiSuDungId.Value);
            }

            var nguoiSuDung = request.NguoiSuDung?.Trim();
            if (!string.IsNullOrWhiteSpace(nguoiSuDung))
            {
                var nguoiSuDungIds = await context.NguoiSuDungThietBis
                    .Where(x =>
                        x.IsActive &&
                        (x.MaNguoiDung.Contains(nguoiSuDung) || x.TenNguoiDung.Contains(nguoiSuDung)))
                    .Select(x => x.Id)
                    .ToListAsync();

                query = query.Where(x => x.NguoiSuDungId.HasValue && nguoiSuDungIds.Contains(x.NguoiSuDungId.Value));
            }

            var trangThaiId = request.TrangThaiId ?? request.TinhTrangThietBiId;
            if (trangThaiId.HasValue)
            {
                query = query.Where(x => x.TrangThaiId == trangThaiId.Value);
            }

            if (request.NgayNhapTu.HasValue)
            {
                var ngayNhapTu = request.NgayNhapTu.Value.Date;
                query = query.Where(x => x.NgayNhapThietBi.HasValue && x.NgayNhapThietBi.Value >= ngayNhapTu);
            }

            if (request.NgayNhapDen.HasValue)
            {
                var ngayNhapDen = request.NgayNhapDen.Value.Date.AddDays(1);
                query = query.Where(x => x.NgayNhapThietBi.HasValue && x.NgayNhapThietBi.Value < ngayNhapDen);
            }

            if (request.NgayDuaVaoSuDungTu.HasValue)
            {
                var ngayDuaVaoSuDungTu = request.NgayDuaVaoSuDungTu.Value.Date;
                query = query.Where(x => x.NgayDuaVaoSuDung.HasValue && x.NgayDuaVaoSuDung.Value >= ngayDuaVaoSuDungTu);
            }

            if (request.NgayDuaVaoSuDungDen.HasValue)
            {
                var ngayDuaVaoSuDungDen = request.NgayDuaVaoSuDungDen.Value.Date.AddDays(1);
                query = query.Where(x => x.NgayDuaVaoSuDung.HasValue && x.NgayDuaVaoSuDung.Value < ngayDuaVaoSuDungDen);
            }

            var devices = await query
                .OrderBy(x => x.MaThietBi)
                .ToListAsync();

            var nhomThietBiMap = await context.NhomThietBis
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
                .ToDictionaryAsync(x => x.Id);
            var donViMap = await context.DonViBoPhans
                .ToDictionaryAsync(x => x.Id);
            var nguoiSuDungMap = await context.NguoiSuDungThietBis
                .ToDictionaryAsync(x => x.Id);
            var trangThaiMap = await context.DmDungChungs
                .Where(x => x.NhomDanhMuc == "TRANG_THAI_TB")
                .ToDictionaryAsync(x => x.Id);

            ThongKeThietBiNhomDto TaoThongKeDonVi(int? id, IEnumerable<ThietBiModel> items, string tenMacDinh)
            {
                DonViBoPhanModel? donVi = null;
                if (id.HasValue)
                {
                    donViMap.TryGetValue(id.Value, out donVi);
                }

                return new ThongKeThietBiNhomDto
                {
                    Id = id,
                    Ma = donVi?.MaDonVi,
                    Ten = donVi?.TenDonVi ?? tenMacDinh,
                    SoLuong = items.Count(),
                    TongNguyenGia = items.Sum(x => x.NguyenGia ?? 0)
                };
            }

            ThongKeThietBiNhomDto TaoThongKeNhomThietBi(int id, IEnumerable<ThietBiModel> items)
            {
                nhomThietBiMap.TryGetValue(id, out var nhomThietBi);

                return new ThongKeThietBiNhomDto
                {
                    Id = id,
                    Ma = nhomThietBi?.MaNhomThietBi,
                    Ten = nhomThietBi?.TenNhomThietBi ?? "Không xác định",
                    SoLuong = items.Count(),
                    TongNguyenGia = items.Sum(x => x.NguyenGia ?? 0)
                };
            }

            ThongKeThietBiNhomDto TaoThongKeTrangThai(int id, IEnumerable<ThietBiModel> items)
            {
                trangThaiMap.TryGetValue(id, out var trangThai);

                return new ThongKeThietBiNhomDto
                {
                    Id = id,
                    Ma = trangThai?.Ma,
                    Ten = trangThai?.Ten ?? "Không xác định",
                    SoLuong = items.Count(),
                    TongNguyenGia = items.Sum(x => x.NguyenGia ?? 0)
                };
            }

            var result = new ThongKeThietBiDto
            {
                MaThietBi = maThietBi,
                PhongBanId = request.PhongBanId,
                BoPhanId = request.BoPhanId,
                NhomThietBiId = request.NhomThietBiId,
                NguoiSuDungId = request.NguoiSuDungId,
                NguoiSuDung = nguoiSuDung,
                TrangThaiId = trangThaiId,
                NgayNhapTu = request.NgayNhapTu,
                NgayNhapDen = request.NgayNhapDen,
                NgayDuaVaoSuDungTu = request.NgayDuaVaoSuDungTu,
                NgayDuaVaoSuDungDen = request.NgayDuaVaoSuDungDen,
                TongSoLuong = devices.Count,
                TongNguyenGia = devices.Sum(x => x.NguyenGia ?? 0),
                TheoPhongBan = devices
                    .GroupBy(x => x.PhongBanId)
                    .Select(x => TaoThongKeDonVi(x.Key, x, "Chưa có phòng ban"))
                    .OrderByDescending(x => x.SoLuong)
                    .ThenBy(x => x.Ten)
                    .ToList(),
                TheoBoPhan = devices
                    .GroupBy(x => x.BoPhanId)
                    .Select(x => TaoThongKeDonVi(x.Key, x, "Chưa có bộ phận"))
                    .OrderByDescending(x => x.SoLuong)
                    .ThenBy(x => x.Ten)
                    .ToList(),
                TheoNhomThietBi = devices
                    .GroupBy(x => x.NhomThietBiId)
                    .Select(x => TaoThongKeNhomThietBi(x.Key, x))
                    .OrderByDescending(x => x.SoLuong)
                    .ThenBy(x => x.Ten)
                    .ToList(),
                TheoTrangThai = devices
                    .GroupBy(x => x.TrangThaiId)
                    .Select(x => TaoThongKeTrangThai(x.Key, x))
                    .OrderByDescending(x => x.SoLuong)
                    .ThenBy(x => x.Ten)
                    .ToList(),
                DanhSach = devices.Select(x =>
                {
                    nhomThietBiMap.TryGetValue(x.NhomThietBiId, out var nhomThietBi);
                    NhomThietBiDto? danhMucThietBi = nhomThietBi;
                    if (nhomThietBi?.ParentId.HasValue == true)
                    {
                        nhomThietBiMap.TryGetValue(nhomThietBi.ParentId.Value, out danhMucThietBi);
                    }

                    trangThaiMap.TryGetValue(x.TrangThaiId, out var trangThai);
                    DonViBoPhanModel? phongBan = null;
                    DonViBoPhanModel? boPhan = null;
                    NguoiSuDungThietBiModel? nguoiSuDung = null;
                    if (x.PhongBanId.HasValue)
                    {
                        donViMap.TryGetValue(x.PhongBanId.Value, out phongBan);
                    }

                    if (x.BoPhanId.HasValue)
                    {
                        donViMap.TryGetValue(x.BoPhanId.Value, out boPhan);
                    }

                    if (x.NguoiSuDungId.HasValue)
                    {
                        nguoiSuDungMap.TryGetValue(x.NguoiSuDungId.Value, out nguoiSuDung);
                    }

                    return new ThongKeThietBiItemDto
                    {
                        Id = x.Id,
                        MaThietBi = x.MaThietBi,
                        MaThietBiCu = x.MaThietBiCu,
                        TenThietBi = x.TenThietBi,
                        NhomThietBiId = x.NhomThietBiId,
                        MaNhomThietBi = nhomThietBi?.MaNhomThietBi,
                        TenNhomThietBi = nhomThietBi?.TenNhomThietBi,
                        DanhMucThietBiId = danhMucThietBi?.Id,
                        MaDanhMucThietBi = danhMucThietBi?.MaNhomThietBi,
                        TenDanhMucThietBi = danhMucThietBi?.TenNhomThietBi,
                        TrangThaiId = x.TrangThaiId,
                        MaTrangThai = trangThai?.Ma,
                        TenTrangThai = trangThai?.Ten,
                        PhongBanId = x.PhongBanId,
                        MaPhongBan = phongBan?.MaDonVi,
                        TenPhongBan = phongBan?.TenDonVi,
                        BoPhanId = x.BoPhanId,
                        MaBoPhan = boPhan?.MaDonVi,
                        TenBoPhan = boPhan?.TenDonVi,
                        NguoiSuDungId = x.NguoiSuDungId,
                        MaNguoiSuDung = nguoiSuDung?.MaNguoiDung,
                        TenNguoiSuDung = nguoiSuDung?.TenNguoiDung,
                        NguyenGia = x.NguyenGia,
                        NgayNhapThietBi = x.NgayNhapThietBi,
                        NgayDuaVaoSuDung = x.NgayDuaVaoSuDung,
                        GhiChu = x.GhiChu
                    };
                }).ToList()
            };

            return result;
        }

        public async Task<ThietBiDto> LuuThietBiAsync(CreateUpdateThietBiRequest request)
        {
            var user = GetCurrentUser();
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
                entity.MaNguoiChinhSua = user.MaNguoiDung;
                entity.TenNguoiChinhSua = user.TenNguoiDung;

                if (originalTrangThaiId != request.TrangThaiId || originalPhongBanId != request.PhongBanId || originalBoPhanId != request.BoPhanId || originalNguoiSuDungId != request.NguoiSuDungId)
                {
                    await TaoLichSuThietBiAsync(entity, request, originalTrangThaiId, originalPhongBanId, originalBoPhanId, originalNguoiSuDungId, now);
                }
            }
            else
            {
                entity = new ThietBiModel
                {
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
                    NgayKhoiTao = now,
                    MaNguoiNhap = user.MaNguoiDung,
                    TenNguoiNhap = user.TenNguoiDung
                };

                context.ThietBis.Add(entity);
                await context.SaveChangesAsync();
                await TaoLichSuThietBiAsync(entity, request, null, null, null, null, now, "NHAP_KHO");
            }

            await context.SaveChangesAsync();
            await LuuThongSoCuaThietBiAsync(entity.Id, request.ThongSo, now);

            return await LayThietBiTheoIdAsync(entity.Id) ?? throw new InvalidOperationException("Không thể lấy thiết bị sau khi lưu.");
        }

        public async Task<ThietBiDto?> LayThietBiTheoIdAsync(int id)
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

        public async Task<bool> XoaThietBiAsync(int id)
        {
            var user = GetCurrentUser();
            var entity = await context.ThietBis.FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            entity.IsActive = false;
            entity.NgayChinhSuaCuoiCung = DateTime.UtcNow;
            entity.MaNguoiChinhSua = user.MaNguoiDung;
            entity.TenNguoiChinhSua = user.TenNguoiDung;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<DmThongSoThietBiDto>> LayThongSoTheoNhomThietBiAsync(int nhomThietBiId)
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
            var user = GetCurrentUser();
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
                entity.MaNguoiChinhSua = user.MaNguoiDung;
                entity.TenNguoiChinhSua = user.TenNguoiDung;
            }
            else
            {
                entity = new DmThongSoThietBi
                {
                    NhomThietBiId = request.NhomThietBiId,
                    MaThongSo = request.MaThongSo,
                    TenThongSo = request.TenThongSo,
                    KieuDuLieu = request.KieuDuLieu,
                    DonViTinhId = request.DonViTinhId,
                    BatBuoc = request.BatBuoc,
                    SapXep = request.SapXep,
                    IsActive = request.IsActive,
                    GhiChu = request.GhiChu,
                    NgayKhoiTao = DateTime.UtcNow,
                    MaNguoiNhap = user.MaNguoiDung,
                    TenNguoiNhap = user.TenNguoiDung
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

        public async Task<bool> XoaThongSoThietBiAsync(int id)
        {
            var user = GetCurrentUser();
            var entity = await context.DmThongSoThietBis.FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            entity.IsActive = false;
            entity.NgayChinhSuaCuoiCung = DateTime.UtcNow;
            entity.MaNguoiChinhSua = user.MaNguoiDung;
            entity.TenNguoiChinhSua = user.TenNguoiDung;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<PhieuThietBiDto> TaoPhieuThietBiAsync(CreatePhieuThietBiRequest request)
        {
            var user = GetCurrentUser();
            var thietBi = await context.ThietBis.FindAsync(request.ThietBiId) ?? throw new KeyNotFoundException("Thiết bị không tồn tại.");
            var now = DateTime.UtcNow;
            var phieu = new PhieuThietBi
            {
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
                NgayKhoiTao = now,
                MaNguoiNhap = user.MaNguoiDung,
                TenNguoiNhap = user.TenNguoiDung
            };

            context.PhieuThietBis.Add(phieu);
            await context.SaveChangesAsync();

            if (request.ChiTiets != null)
            {
                foreach (var detail in request.ChiTiets)
                {
                    var detailEntry = new PhieuThietBiChiTiet
                    {
                        PhieuThietBiId = phieu.Id,
                        CongViecId = detail.CongViecId,
                        ThongSoId = detail.ThongSoId,
                        NoiDung = detail.NoiDung,
                        GiaTri = detail.GiaTri,
                        ChiPhi = detail.ChiPhi,
                        NgayBatDau = detail.NgayBatDau,
                        NgayKetThuc = detail.NgayKetThuc,
                        GhiChu = detail.GhiChu,
                        NgayKhoiTao = now,
                        MaNguoiNhap = user.MaNguoiDung,
                        TenNguoiNhap = user.TenNguoiDung
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
            thietBi.MaNguoiChinhSua = user.MaNguoiDung;
            thietBi.TenNguoiChinhSua = user.TenNguoiDung;
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

        public async Task<IEnumerable<LichSuThietBiDto>> LayLichSuThietBiAsync(int thietBiId)
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

        private async Task LuuThongSoCuaThietBiAsync(int thietBiId, IEnumerable<ThietBiThongSoValueRequest>? values, DateTime now)
        {
            if (values == null)
            {
                return;
            }

            var user = GetCurrentUser();

            foreach (var item in values)
            {
                var existing = await context.ThietBiThongSos
                    .FirstOrDefaultAsync(x => x.ThietBiId == thietBiId && x.ThongSoId == item.ThongSoId);

                if (existing == null)
                {
                    context.ThietBiThongSos.Add(new ThietBiThongSo
                    {
                        ThietBiId = thietBiId,
                        ThongSoId = item.ThongSoId,
                        GiaTriText = item.GiaTriText,
                        GiaTriNumber = item.GiaTriNumber,
                        GiaTriDate = item.GiaTriDate,
                        GiaTriBit = item.GiaTriBit,
                        GhiChu = item.GhiChu,
                        NgayKhoiTao = now,
                        MaNguoiNhap = user.MaNguoiDung,
                        TenNguoiNhap = user.TenNguoiDung
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

        private Task TaoLichSuThietBiAsync(ThietBiModel current, CreateUpdateThietBiRequest request, int? trangThaiTruocId, int? phongBanTruocId, int? boPhanTruocId, int? nguoiSuDungTruocId, DateTime now, string loaiNghiepVu = "CAP_NHAT_THIET_BI", int? nghiepVuId = null)
        {
            var history = new LichSuThietBi
            {
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
            var user = GetCurrentUser();
            history.MaNguoiNhap = user.MaNguoiDung;
            history.TenNguoiNhap = user.TenNguoiDung;

            context.LichSuThietBis.Add(history);
            return Task.CompletedTask;
        }

        private LoggedInUser GetCurrentUser()
        {
            return authenInfo.Get() ?? throw new UnauthorizedAccessException("Chưa đăng nhập hoặc token không hợp lệ.");
        }

        private async Task<int> LayTrangThaiIdAsync(string nhomDanhMuc, string ma)
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


