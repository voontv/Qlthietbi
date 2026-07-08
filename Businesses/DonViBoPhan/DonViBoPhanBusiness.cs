using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QlThietBi.Businesses;
using QlThietBi.DTO.Request;
using QlThietBi.DTO.Response;
using QlThietBi.Models;

namespace QlThietBi.Businesses.DonViBoPhan
{
    public class DonViBoPhanBusiness : IDonViBoPhanBusiness
    {
        private readonly QlThietBiContext context;
        private readonly IAuthenInfo authenInfo;

        public DonViBoPhanBusiness(QlThietBiContext context, IAuthenInfo authenInfo)
        {
            this.context = context;
            this.authenInfo = authenInfo;
        }

        public async Task<IEnumerable<DonViBoPhanDto>> GetUnitsAsync()
        {
            return await context.DonViBoPhans
                .Where(x => x.IsActive)
                .Select(x => new DonViBoPhanDto
                {
                    Id = x.Id,
                    MaDonVi = x.MaDonVi,
                    TenDonVi = x.TenDonVi,
                    ParentId = x.ParentId,
                    LoaiDonVi = x.LoaiDonVi,
                    GhiChu = x.GhiChu,
                    SapXep = x.SapXep,
                    IsActive = x.IsActive
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<DonViBoPhanDto>> GetPartsByDepartmentCodeAsync(string maPhongBan)
        {
            if (string.IsNullOrWhiteSpace(maPhongBan))
            {
                return Enumerable.Empty<DonViBoPhanDto>();
            }

            var code = maPhongBan.Trim();
            var department = await context.DonViBoPhans
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IsActive && x.ParentId == null && x.MaDonVi == code);

            if (department == null)
            {
                return Enumerable.Empty<DonViBoPhanDto>();
            }

            return await context.DonViBoPhans
                .AsNoTracking()
                .Where(x => x.IsActive && x.ParentId == department.Id)
                .OrderBy(x => x.SapXep)
                .ThenBy(x => x.TenDonVi)
                .Select(x => new DonViBoPhanDto
                {
                    Id = x.Id,
                    MaDonVi = x.MaDonVi,
                    TenDonVi = x.TenDonVi,
                    ParentId = x.ParentId,
                    LoaiDonVi = x.LoaiDonVi,
                    GhiChu = x.GhiChu,
                    SapXep = x.SapXep,
                    IsActive = x.IsActive
                })
                .ToListAsync();
        }

        public async Task<DonViBoPhanDto?> GetUnitByIdAsync(int id)
        {
            var entity = await context.DonViBoPhans.FindAsync(id);
            if (entity == null)
            {
                return null;
            }

            return new DonViBoPhanDto
            {
                Id = entity.Id,
                MaDonVi = entity.MaDonVi,
                TenDonVi = entity.TenDonVi,
                ParentId = entity.ParentId,
                LoaiDonVi = entity.LoaiDonVi,
                GhiChu = entity.GhiChu,
                SapXep = entity.SapXep,
                IsActive = entity.IsActive
            };
        }

        public async Task<DonViBoPhanDto> SaveUnitAsync(CreateUpdateDonViBoPhanRequest request)
        {
            var user = GetCurrentUser();
            global::QlThietBi.Models.DonViBoPhan entity;
            if (request.Id.HasValue)
            {
                entity = await context.DonViBoPhans.FindAsync(request.Id.Value) ?? throw new KeyNotFoundException("Đơn vị/bộ phận không tồn tại.");
                entity.MaDonVi = request.MaDonVi;
                entity.TenDonVi = request.TenDonVi;
                entity.ParentId = request.ParentId;
                entity.LoaiDonVi = request.LoaiDonVi;
                entity.GhiChu = request.GhiChu;
                entity.SapXep = request.SapXep;
                entity.IsActive = request.IsActive;
                entity.NgayChinhSuaCuoiCung = DateTime.UtcNow;
                entity.MaNguoiChinhSua = user.MaNguoiDung;
                entity.TenNguoiChinhSua = user.TenNguoiDung;
            }
            else
            {
                entity = new global::QlThietBi.Models.DonViBoPhan
                {
                    MaDonVi = request.MaDonVi,
                    TenDonVi = request.TenDonVi,
                    ParentId = request.ParentId,
                    LoaiDonVi = request.LoaiDonVi,
                    GhiChu = request.GhiChu,
                    SapXep = request.SapXep,
                    IsActive = request.IsActive,
                    NgayKhoiTao = DateTime.UtcNow,
                    MaNguoiNhap = user.MaNguoiDung,
                    TenNguoiNhap = user.TenNguoiDung
                };
                context.DonViBoPhans.Add(entity);
            }

            await context.SaveChangesAsync();

            return new DonViBoPhanDto
            {
                Id = entity.Id,
                MaDonVi = entity.MaDonVi,
                TenDonVi = entity.TenDonVi,
                ParentId = entity.ParentId,
                LoaiDonVi = entity.LoaiDonVi,
                GhiChu = entity.GhiChu,
                SapXep = entity.SapXep,
                IsActive = entity.IsActive
            };
        }

        public async Task<bool> DeleteUnitAsync(int id)
        {
            var user = GetCurrentUser();
            var entity = await context.DonViBoPhans.FindAsync(id);
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

        private LoggedInUser GetCurrentUser()
        {
            return authenInfo.Get() ?? throw new UnauthorizedAccessException("Chưa đăng nhập hoặc token không hợp lệ.");
        }
    }
}


