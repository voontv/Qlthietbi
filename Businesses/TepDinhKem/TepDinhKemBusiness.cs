using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QlThietBi.DTO.Request;
using QlThietBi.DTO.Response;
using QlThietBi.Models;

namespace QlThietBi.Businesses.TepDinhKem
{
    public class TepDinhKemBusiness : ITepDinhKemBusiness
    {
        private readonly QlThietBiContext context;

        public TepDinhKemBusiness(QlThietBiContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<TepDinhKemDto>> GetAttachmentsAsync(string doiTuongLoai, int doiTuongId)
        {
            return await context.TepDinhKems
                .Where(x => x.DoiTuongLoai == doiTuongLoai && x.DoiTuongId == doiTuongId)
                .Select(x => new TepDinhKemDto
                {
                    Id = x.Id,
                    DoiTuongLoai = x.DoiTuongLoai,
                    DoiTuongId = x.DoiTuongId,
                    TenFile = x.TenFile,
                    DuongDan = x.DuongDan,
                    LoaiFile = x.LoaiFile,
                    DungLuong = x.DungLuong,
                    GhiChu = x.GhiChu
                })
                .ToListAsync();
        }

        public async Task<TepDinhKemDto?> GetAttachmentByIdAsync(int id)
        {
            var entity = await context.TepDinhKems.FindAsync(id);
            if (entity == null)
            {
                return null;
            }

            return new TepDinhKemDto
            {
                Id = entity.Id,
                DoiTuongLoai = entity.DoiTuongLoai,
                DoiTuongId = entity.DoiTuongId,
                TenFile = entity.TenFile,
                DuongDan = entity.DuongDan,
                LoaiFile = entity.LoaiFile,
                DungLuong = entity.DungLuong,
                GhiChu = entity.GhiChu
            };
        }

        public async Task<TepDinhKemDto> SaveAttachmentAsync(CreateUpdateTepDinhKemRequest request)
        {
            global::QlThietBi.Models.TepDinhKem entity;
            if (request.Id.HasValue)
            {
                entity = await context.TepDinhKems.FindAsync(request.Id.Value) ?? throw new KeyNotFoundException("Tệp đính kèm không tồn tại.");
                entity.DoiTuongLoai = request.DoiTuongLoai;
                entity.DoiTuongId = request.DoiTuongId;
                entity.TenFile = request.TenFile;
                entity.DuongDan = request.DuongDan;
                entity.LoaiFile = request.LoaiFile;
                entity.DungLuong = request.DungLuong;
                entity.GhiChu = request.GhiChu;
            }
            else
            {
                entity = new global::QlThietBi.Models.TepDinhKem
                {
                    DoiTuongLoai = request.DoiTuongLoai,
                    DoiTuongId = request.DoiTuongId,
                    TenFile = request.TenFile,
                    DuongDan = request.DuongDan,
                    LoaiFile = request.LoaiFile,
                    DungLuong = request.DungLuong,
                    GhiChu = request.GhiChu,
                    NgayKhoiTao = DateTime.UtcNow
                };
                context.TepDinhKems.Add(entity);
            }

            await context.SaveChangesAsync();

            return new TepDinhKemDto
            {
                Id = entity.Id,
                DoiTuongLoai = entity.DoiTuongLoai,
                DoiTuongId = entity.DoiTuongId,
                TenFile = entity.TenFile,
                DuongDan = entity.DuongDan,
                LoaiFile = entity.LoaiFile,
                DungLuong = entity.DungLuong,
                GhiChu = entity.GhiChu
            };
        }

        public async Task<bool> DeleteAttachmentAsync(int id)
        {
            var entity = await context.TepDinhKems.FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            context.TepDinhKems.Remove(entity);
            await context.SaveChangesAsync();
            return true;
        }
    }
}


