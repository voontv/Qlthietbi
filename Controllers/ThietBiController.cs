using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QlThietBi.Businesses.ThietBi;
using QlThietBi.DTO.Request;
using QlThietBi.DTO.Response;

namespace QlThietBi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ThietBiController : ControllerBase
    {
        private readonly IThietBiBusiness thietBiBusiness;
        private const string TrangThaiThietBi = "TRANG_THAI_TB";
        private const string TrangThaiKiemKe = "TRANG_THAI_KK";
        private const string DonViTinh = "DON_VI_TINH";
        private const string NhanHieu = "NHAN_HIEU";
        private const string MauSac = "MAU_SAC";
        private const string NuocSanXuat = "NUOC_SAN_XUAT";
        private const string ChatLieu = "CHAT_LIEU";
        private const string DonViCungCap = "DON_VI_CUNG_CAP";
        private const string KetLuan = "KET_LUAN";
        private const string CongViecBtbd = "CONG_VIEC_BTBD";

        public ThietBiController(IThietBiBusiness thietBiBusiness)
        {
            this.thietBiBusiness = thietBiBusiness;
        }

        /// <summary>
        /// Lấy danh sách danh mục dùng chung theo nhóm danh mục.
        /// </summary>
        /// <param name="nhomDanhMuc">Tên nhóm danh mục, ví dụ: TRANG_THAI_TB, LOAI_THIET_BI.</param>
        /// <response code="200">Trả về danh sách danh mục dùng chung.</response>
        [HttpGet("danh-muc/{nhomDanhMuc}")]
        [ProducesResponseType(typeof(IEnumerable<DmDungChungDto>), 200)]
        public async Task<ActionResult<IEnumerable<DmDungChungDto>>> LayDanhMucDungChung(string nhomDanhMuc)
        {
            return await LayDanhMucTheoNhom(ChuanHoaNhomDanhMuc(nhomDanhMuc));
        }

        [HttpGet("danh-muc/trang-thai-thiet-bi")]
        [ProducesResponseType(typeof(IEnumerable<DmDungChungDto>), 200)]
        public Task<ActionResult<IEnumerable<DmDungChungDto>>> LayDanhMucTrangThaiThietBi()
        {
            return LayDanhMucTheoNhom(TrangThaiThietBi);
        }

        [HttpGet("danh-muc/trang-thai-kiem-ke")]
        [ProducesResponseType(typeof(IEnumerable<DmDungChungDto>), 200)]
        public Task<ActionResult<IEnumerable<DmDungChungDto>>> LayDanhMucTrangThaiKiemKe()
        {
            return LayDanhMucTheoNhom(TrangThaiKiemKe);
        }

        [HttpGet("danh-muc/don-vi-tinh")]
        [ProducesResponseType(typeof(IEnumerable<DmDungChungDto>), 200)]
        public Task<ActionResult<IEnumerable<DmDungChungDto>>> LayDanhMucDonViTinh()
        {
            return LayDanhMucTheoNhom(DonViTinh);
        }

        [HttpGet("danh-muc/nhan-hieu")]
        [ProducesResponseType(typeof(IEnumerable<DmDungChungDto>), 200)]
        public Task<ActionResult<IEnumerable<DmDungChungDto>>> LayDanhMucNhanHieu()
        {
            return LayDanhMucTheoNhom(NhanHieu);
        }

        [HttpGet("danh-muc/mau-sac")]
        [ProducesResponseType(typeof(IEnumerable<DmDungChungDto>), 200)]
        public Task<ActionResult<IEnumerable<DmDungChungDto>>> LayDanhMucMauSac()
        {
            return LayDanhMucTheoNhom(MauSac);
        }

        [HttpGet("danh-muc/nuoc-san-xuat")]
        [ProducesResponseType(typeof(IEnumerable<DmDungChungDto>), 200)]
        public Task<ActionResult<IEnumerable<DmDungChungDto>>> LayDanhMucNuocSanXuat()
        {
            return LayDanhMucTheoNhom(NuocSanXuat);
        }

        [HttpGet("danh-muc/chat-lieu")]
        [ProducesResponseType(typeof(IEnumerable<DmDungChungDto>), 200)]
        public Task<ActionResult<IEnumerable<DmDungChungDto>>> LayDanhMucChatLieu()
        {
            return LayDanhMucTheoNhom(ChatLieu);
        }

        [HttpGet("danh-muc/don-vi-cung-cap")]
        [ProducesResponseType(typeof(IEnumerable<DmDungChungDto>), 200)]
        public Task<ActionResult<IEnumerable<DmDungChungDto>>> LayDanhMucDonViCungCap()
        {
            return LayDanhMucTheoNhom(DonViCungCap);
        }

        [HttpGet("danh-muc/ket-luan")]
        [ProducesResponseType(typeof(IEnumerable<DmDungChungDto>), 200)]
        public Task<ActionResult<IEnumerable<DmDungChungDto>>> LayDanhMucKetLuan()
        {
            return LayDanhMucTheoNhom(KetLuan);
        }

        [HttpGet("danh-muc/cong-viec-btbd")]
        [ProducesResponseType(typeof(IEnumerable<DmDungChungDto>), 200)]
        public Task<ActionResult<IEnumerable<DmDungChungDto>>> LayDanhMucCongViecBtbd()
        {
            return LayDanhMucTheoNhom(CongViecBtbd);
        }

        /// <summary>
        /// Tạo hoặc cập nhật danh mục dùng chung.
        /// </summary>
        /// <remarks>
        /// Request mẫu:
        /// {
        ///   "id": "int?",
        ///   "nhomDanhMuc": "TRANG_THAI_TB",
        ///   "ma": "DANG_SU_DUNG",
        ///   "ten": "Đang sử dụng",
        ///   "ghiChu": "",
        ///   "sapXep": 1,
        ///   "isActive": true
        /// }
        /// </remarks>
        /// <response code="200">Danh mục dùng chung được lưu.</response>
        [Authorize]
        [HttpPost("danh-muc")]
        public async Task<ActionResult<DmDungChungDto>> LuuDanhMucDungChung(CreateUpdateDmDungChungRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.NhomDanhMuc))
            {
                return BadRequest("Thiếu nhóm danh mục.");
            }

            request.NhomDanhMuc = ChuanHoaNhomDanhMuc(request.NhomDanhMuc);
            var result = await thietBiBusiness.LuuDanhMucDungChungAsync(request);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("danh-muc/{nhomDanhMuc}")]
        public Task<ActionResult<DmDungChungDto>> LuuDanhMucDungChungTheoNhom(string nhomDanhMuc, CreateUpdateDmDungChungRequest request)
        {
            return LuuDanhMucTheoNhom(ChuanHoaNhomDanhMuc(nhomDanhMuc), request);
        }

        [Authorize]
        [HttpPost("danh-muc/trang-thai-thiet-bi")]
        public Task<ActionResult<DmDungChungDto>> LuuDanhMucTrangThaiThietBi(CreateUpdateDmDungChungRequest request)
        {
            return LuuDanhMucTheoNhom(TrangThaiThietBi, request);
        }

        [Authorize]
        [HttpPost("danh-muc/trang-thai-kiem-ke")]
        public Task<ActionResult<DmDungChungDto>> LuuDanhMucTrangThaiKiemKe(CreateUpdateDmDungChungRequest request)
        {
            return LuuDanhMucTheoNhom(TrangThaiKiemKe, request);
        }

        [Authorize]
        [HttpPost("danh-muc/don-vi-tinh")]
        public Task<ActionResult<DmDungChungDto>> LuuDanhMucDonViTinh(CreateUpdateDmDungChungRequest request)
        {
            return LuuDanhMucTheoNhom(DonViTinh, request);
        }

        [Authorize]
        [HttpPost("danh-muc/nhan-hieu")]
        public Task<ActionResult<DmDungChungDto>> LuuDanhMucNhanHieu(CreateUpdateDmDungChungRequest request)
        {
            return LuuDanhMucTheoNhom(NhanHieu, request);
        }

        [Authorize]
        [HttpPost("danh-muc/mau-sac")]
        public Task<ActionResult<DmDungChungDto>> LuuDanhMucMauSac(CreateUpdateDmDungChungRequest request)
        {
            return LuuDanhMucTheoNhom(MauSac, request);
        }

        [Authorize]
        [HttpPost("danh-muc/nuoc-san-xuat")]
        public Task<ActionResult<DmDungChungDto>> LuuDanhMucNuocSanXuat(CreateUpdateDmDungChungRequest request)
        {
            return LuuDanhMucTheoNhom(NuocSanXuat, request);
        }

        [Authorize]
        [HttpPost("danh-muc/chat-lieu")]
        public Task<ActionResult<DmDungChungDto>> LuuDanhMucChatLieu(CreateUpdateDmDungChungRequest request)
        {
            return LuuDanhMucTheoNhom(ChatLieu, request);
        }

        [Authorize]
        [HttpPost("danh-muc/don-vi-cung-cap")]
        public Task<ActionResult<DmDungChungDto>> LuuDanhMucDonViCungCap(CreateUpdateDmDungChungRequest request)
        {
            return LuuDanhMucTheoNhom(DonViCungCap, request);
        }

        [Authorize]
        [HttpPost("danh-muc/ket-luan")]
        public Task<ActionResult<DmDungChungDto>> LuuDanhMucKetLuan(CreateUpdateDmDungChungRequest request)
        {
            return LuuDanhMucTheoNhom(KetLuan, request);
        }

        [Authorize]
        [HttpPost("danh-muc/cong-viec-btbd")]
        public Task<ActionResult<DmDungChungDto>> LuuDanhMucCongViecBtbd(CreateUpdateDmDungChungRequest request)
        {
            return LuuDanhMucTheoNhom(CongViecBtbd, request);
        }

        /// <summary>
        /// Xóa danh mục dùng chung.
        /// </summary>
        /// <param name="id">Id danh mục dùng chung.</param>
        /// <response code="204">Xóa thành công.</response>
        /// <response code="404">Không tìm thấy danh mục.</response>
        [Authorize]
        [HttpDelete("danh-muc/{id}")]
        public async Task<ActionResult> XoaDanhMucDungChung(int id)
        {
            var removed = await thietBiBusiness.XoaDanhMucDungChungAsync(id);
            if (!removed)
            {
                return NotFound();
            }
            return NoContent();
        }

        /// <summary>
        /// Lấy danh sách nhóm thiết bị.
        /// </summary>
        /// <response code="200">Trả về danh sách nhóm thiết bị.</response>
        [HttpGet("nhom-thiet-bi")]
        public async Task<ActionResult<IEnumerable<NhomThietBiDto>>> LayDanhSachNhomThietBi()
        {
            var result = await thietBiBusiness.LayDanhSachNhomThietBiAsync();
            return Ok(result);
        }

        /// <summary>
        /// Tạo hoặc cập nhật nhóm thiết bị.
        /// </summary>
        /// <remarks>
        /// Request mẫu:
        /// {
        ///   "id": "int?",
        ///   "maNhom": "NHOM_01",
        ///   "tenNhom": "Máy in",
        ///   "ghiChu": "",
        ///   "sapXep": 1,
        ///   "isActive": true
        /// }
        /// </remarks>
        /// <response code="200">Nhóm thiết bị được lưu.</response>
        [Authorize]
        [HttpPost("nhom-thiet-bi")]
        public async Task<ActionResult<NhomThietBiDto>> LuuNhomThietBi(CreateUpdateNhomThietBiRequest request)
        {
            var result = await thietBiBusiness.LuuNhomThietBiAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Xóa nhóm thiết bị.
        /// </summary>
        /// <param name="id">Id nhóm thiết bị.</param>
        /// <response code="204">Xóa thành công.</response>
        /// <response code="404">Không tìm thấy nhóm thiết bị.</response>
        [Authorize]
        [HttpDelete("nhom-thiet-bi/{id}")]
        public async Task<ActionResult> XoaNhomThietBi(int id)
        {
            var removed = await thietBiBusiness.XoaNhomThietBiAsync(id);
            if (!removed)
            {
                return NotFound();
            }
            return NoContent();
        }

        /// <summary>
        /// Lấy danh sách hồ sơ thiết bị.
        /// </summary>
        /// <response code="200">Trả về danh sách thiết bị.</response>
        [HttpGet("thiet-bi")]
        public async Task<ActionResult<IEnumerable<ThietBiDto>>> LayDanhSachThietBi()
        {
            var result = await thietBiBusiness.LayDanhSachThietBiAsync();
            return Ok(result);
        }

        /// <summary>
        /// Thống kê và tìm kiếm thiết bị theo phòng ban, bộ phận, nhóm thiết bị.
        /// </summary>
        /// <remarks>
        /// Không truyền tham số nào thì xem như lấy tất cả.
        /// Nếu truyền nhóm thiết bị cha thì thống kê cả các nhóm con trực thuộc.
        /// </remarks>
        /// <param name="request">Bộ lọc thống kê.</param>
        /// <response code="200">Tổng hợp và danh sách thiết bị sau lọc.</response>
        [HttpGet("thiet-bi/thong-ke")]
        public async Task<ActionResult<ThongKeThietBiDto>> ThongKeThietBi([FromQuery] QueryThongKeThietBiRequest request)
        {
            var result = await thietBiBusiness.ThongKeThietBiAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Lấy thiết bị theo Id.
        /// </summary>
        /// <param name="id">Id thiết bị.</param>
        /// <response code="200">Trả về thông tin thiết bị.</response>
        /// <response code="404">Không tìm thấy thiết bị.</response>
        [HttpGet("thiet-bi/{id}")]
        public async Task<ActionResult<ThietBiDto>> LayThietBiTheoId(int id)
        {
            var result = await thietBiBusiness.LayThietBiTheoIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        /// <summary>
        /// Tạo hoặc cập nhật hồ sơ thiết bị, kèm thông số động.
        /// </summary>
        /// <remarks>
        /// Request mẫu: xem DTO `CreateUpdateThietBiRequest`.
        /// </remarks>
        /// <response code="200">Thiết bị được lưu.</response>
        [Authorize]
        [HttpPost("thiet-bi")]
        public async Task<ActionResult<ThietBiDto>> LuuThietBi(CreateUpdateThietBiRequest request)
        {
            var result = await thietBiBusiness.LuuThietBiAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Xóa hồ sơ thiết bị.
        /// </summary>
        /// <param name="id">Id thiết bị.</param>
        /// <response code="204">Xóa thành công.</response>
        /// <response code="404">Không tìm thấy thiết bị.</response>
        [Authorize]
        [HttpDelete("thiet-bi/{id}")]
        public async Task<ActionResult> XoaThietBi(int id)
        {
            var removed = await thietBiBusiness.XoaThietBiAsync(id);
            if (!removed)
            {
                return NotFound();
            }
            return NoContent();
        }

        /// <summary>
        /// Lấy danh sách thông số cho nhóm thiết bị.
        /// </summary>
        /// <param name="nhomThietBiId">Id nhóm thiết bị.</param>
        /// <response code="200">Trả về danh sách thông số thiết bị.</response>
        [HttpGet("thiet-bi/{nhomThietBiId}/thong-so")]
        public async Task<ActionResult<IEnumerable<DmThongSoThietBiDto>>> LayThongSoTheoNhomThietBi(int nhomThietBiId)
        {
            var result = await thietBiBusiness.LayThongSoTheoNhomThietBiAsync(nhomThietBiId);
            return Ok(result);
        }

        /// <summary>
        /// Tạo hoặc cập nhật thông số thiết bị.
        /// </summary>
        /// <remarks>
        /// Request mẫu:
        /// {
        ///   "id": "int?",
        ///   "nhomThietBiId": "int",
        ///   "maThongSo": "RAM",
        ///   "tenThongSo": "Bộ nhớ RAM",
        ///   "donVi": "GB",
        ///   "ghiChu": "",
        ///   "sapXep": 1,
        ///   "isActive": true
        /// }
        /// </remarks>
        /// <response code="200">Thông số thiết bị được lưu.</response>
        [Authorize]
        [HttpPost("thiet-bi/thong-so")]
        public async Task<ActionResult<DmThongSoThietBiDto>> LuuThongSoThietBi(CreateUpdateDmThongSoThietBiRequest request)
        {
            var result = await thietBiBusiness.LuuThongSoThietBiAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Xóa thông số thiết bị.
        /// </summary>
        /// <param name="id">Id thông số thiết bị.</param>
        /// <response code="204">Xóa thành công.</response>
        /// <response code="404">Không tìm thấy thông số.</response>
        [Authorize]
        [HttpDelete("thiet-bi/thong-so/{id}")]
        public async Task<ActionResult> XoaThongSoThietBi(int id)
        {
            var removed = await thietBiBusiness.XoaThongSoThietBiAsync(id);
            if (!removed)
            {
                return NotFound();
            }
            return NoContent();
        }

        /// <summary>
        /// Tạo phiếu nghiệp vụ thiết bị và ghi lịch sử.
        /// </summary>
        /// <remarks>
        /// Request mẫu:
        /// {
        ///   "thietBiId": "int",
        ///   "loaiPhieu": "CAP_PHAT",
        ///   "noiDung": "Cấp phát cho phòng Kỹ thuật",
        ///   "phongBanId": "int?",
        ///   "boPhanId": "int?",
        ///   "nguoiSuDungId": "int?",
        ///   "chiTiet": [
        ///     {
        ///       "thongSoId": "int",
        ///       "giaTriTruoc": "4GB",
        ///       "giaTriSau": "8GB"
        ///     }
        ///   ]
        /// }
        /// </remarks>
        /// <response code="200">Phiếu nghiệp vụ thiết bị được lưu.</response>
        [Authorize]
        [HttpPost("phieu-thiet-bi")]
        public async Task<ActionResult<PhieuThietBiDto>> TaoPhieuThietBi(CreatePhieuThietBiRequest request)
        {
            var result = await thietBiBusiness.TaoPhieuThietBiAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Lấy lịch sử thay đổi của một thiết bị.
        /// </summary>
        /// <param name="thietBiId">Id thiết bị.</param>
        /// <response code="200">Danh sách lịch sử thiết bị.</response>
        [HttpGet("thiet-bi/{thietBiId}/lich-su")]
        public async Task<ActionResult<IEnumerable<LichSuThietBiDto>>> LayLichSuThietBi(int thietBiId)
        {
            var result = await thietBiBusiness.LayLichSuThietBiAsync(thietBiId);
            return Ok(result);
        }

        private async Task<ActionResult<IEnumerable<DmDungChungDto>>> LayDanhMucTheoNhom(string nhomDanhMuc)
        {
            var result = await thietBiBusiness.LayDanhMucDungChungAsync(nhomDanhMuc);
            return Ok(result);
        }

        private async Task<ActionResult<DmDungChungDto>> LuuDanhMucTheoNhom(string nhomDanhMuc, CreateUpdateDmDungChungRequest request)
        {
            request.NhomDanhMuc = nhomDanhMuc;
            var result = await thietBiBusiness.LuuDanhMucDungChungAsync(request);
            return Ok(result);
        }

        private static string ChuanHoaNhomDanhMuc(string nhomDanhMuc)
        {
            return nhomDanhMuc.Trim().ToUpperInvariant() switch
            {
                "TRANG-THAI-THIET-BI" => TrangThaiThietBi,
                "TRANG-THAI-KIEM-KE" => TrangThaiKiemKe,
                "DON-VI-TINH" => DonViTinh,
                "NHAN-HIEU" => NhanHieu,
                "MAU-SAC" => MauSac,
                "NUOC-SAN-XUAT" => NuocSanXuat,
                "CHAT-LIEU" => ChatLieu,
                "DON-VI-CUNG-CAP" => DonViCungCap,
                "KET-LUAN" => KetLuan,
                "CONG-VIEC-BTBD" => CongViecBtbd,
                var value => value
            };
        }
    }
}


