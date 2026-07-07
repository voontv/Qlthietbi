namespace QlThietBi.DTO.Response
{
    public class LoggedInUserDto
    {
        public int? NguoiSuDungId { get; set; }
        public string MaNguoiDung { get; set; } = null!;
        public string TenNguoiDung { get; set; } = null!;
        public bool? KhopMaNguoiDung { get; set; }
    }
}
