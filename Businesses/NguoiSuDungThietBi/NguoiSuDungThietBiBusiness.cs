using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QlThietBi.Businesses;
using QlThietBi.DTO.Request;
using QlThietBi.DTO.Response;
using QlThietBi.Models;

namespace QlThietBi.Businesses.NguoiSuDungThietBi
{
    public class NguoiSuDungThietBiBusiness : INguoiSuDungThietBiBusiness
    {
        private readonly QlThietBiContext context;
        private readonly IAuthenInfo authenInfo;

        public NguoiSuDungThietBiBusiness(QlThietBiContext context, IAuthenInfo authenInfo)
        {
            this.context = context;
            this.authenInfo = authenInfo;
        }

        public async Task<IEnumerable<NguoiSuDungThietBiDto>> GetUsersAsync()
        {
            return await context.NguoiSuDungThietBis
                .Where(x => x.IsActive)
                .Select(x => new NguoiSuDungThietBiDto
                {
                    Id = x.Id,
                    MaNguoiDung = x.MaNguoiDung,
                    TenNguoiDung = x.TenNguoiDung,
                    DonViBoPhanId = x.DonViBoPhanId,
                    ChucVu = x.ChucVu,
                    SoDienThoai = x.SoDienThoai,
                    Email = x.Email,
                    GhiChu = x.GhiChu,
                    IsActive = x.IsActive
                })
                .ToListAsync();
        }

        public async Task<NguoiSuDungThietBiDto?> GetUserByIdAsync(int id)
        {
            var entity = await context.NguoiSuDungThietBis.FindAsync(id);
            if (entity == null)
            {
                return null;
            }

            return new NguoiSuDungThietBiDto
            {
                Id = entity.Id,
                MaNguoiDung = entity.MaNguoiDung,
                TenNguoiDung = entity.TenNguoiDung,
                DonViBoPhanId = entity.DonViBoPhanId,
                ChucVu = entity.ChucVu,
                SoDienThoai = entity.SoDienThoai,
                Email = entity.Email,
                GhiChu = entity.GhiChu,
                IsActive = entity.IsActive
            };
        }

        public async Task<NguoiSuDungThietBiDto> SaveUserAsync(CreateUpdateNguoiSuDungThietBiRequest request)
        {
            var user = GetCurrentUser();
            global::QlThietBi.Models.NguoiSuDungThietBi entity;
            if (request.Id.HasValue)
            {
                entity = await context.NguoiSuDungThietBis.FindAsync(request.Id.Value) ?? throw new KeyNotFoundException("Người sử dụng không tồn tại.");
                entity.MaNguoiDung = request.MaNguoiDung;
                entity.TenNguoiDung = request.TenNguoiDung;
                entity.DonViBoPhanId = request.DonViBoPhanId;
                entity.ChucVu = request.ChucVu;
                entity.SoDienThoai = request.SoDienThoai;
                entity.Email = request.Email;
                entity.GhiChu = request.GhiChu;
                entity.IsActive = request.IsActive;
                entity.NgayChinhSuaCuoiCung = DateTime.UtcNow;
                entity.MaNguoiChinhSua = user.MaNguoiDung;
                entity.TenNguoiChinhSua = user.TenNguoiDung;
            }
            else
            {
                entity = new global::QlThietBi.Models.NguoiSuDungThietBi
                {
                    MaNguoiDung = request.MaNguoiDung,
                    TenNguoiDung = request.TenNguoiDung,
                    DonViBoPhanId = request.DonViBoPhanId,
                    ChucVu = request.ChucVu,
                    SoDienThoai = request.SoDienThoai,
                    Email = request.Email,
                    GhiChu = request.GhiChu,
                    IsActive = request.IsActive,
                    NgayKhoiTao = DateTime.UtcNow,
                    MaNguoiNhap = user.MaNguoiDung,
                    TenNguoiNhap = user.TenNguoiDung
                };
                context.NguoiSuDungThietBis.Add(entity);
            }

            await context.SaveChangesAsync();

            return new NguoiSuDungThietBiDto
            {
                Id = entity.Id,
                MaNguoiDung = entity.MaNguoiDung,
                TenNguoiDung = entity.TenNguoiDung,
                DonViBoPhanId = entity.DonViBoPhanId,
                ChucVu = entity.ChucVu,
                SoDienThoai = entity.SoDienThoai,
                Email = entity.Email,
                GhiChu = entity.GhiChu,
                IsActive = entity.IsActive
            };
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = GetCurrentUser();
            var entity = await context.NguoiSuDungThietBis.FindAsync(id);
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


