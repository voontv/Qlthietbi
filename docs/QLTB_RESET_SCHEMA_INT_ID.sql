SET NOCOUNT ON;
SET XACT_ABORT ON;

DECLARE @sql nvarchar(max) = N'';

SELECT @sql = @sql + N'ALTER TABLE ' + QUOTENAME(OBJECT_SCHEMA_NAME(parent_object_id)) + N'.' + QUOTENAME(OBJECT_NAME(parent_object_id)) +
    N' DROP CONSTRAINT ' + QUOTENAME(name) + N';' + CHAR(13) + CHAR(10)
FROM sys.foreign_keys
WHERE OBJECT_NAME(parent_object_id) IN (
    N'TepDinhKem',
    N'LichSuThietBi',
    N'PhieuThietBiChiTiet',
    N'PhieuThietBi',
    N'ThietBiThongSo',
    N'DmThongSoThietBi',
    N'ThietBi',
    N'NguoiSuDungThietBi',
    N'NhomThietBi',
    N'DonViBoPhan',
    N'DmDungChung'
);

EXEC sys.sp_executesql @sql;

DROP TABLE IF EXISTS dbo.TepDinhKem;
DROP TABLE IF EXISTS dbo.LichSuThietBi;
DROP TABLE IF EXISTS dbo.PhieuThietBiChiTiet;
DROP TABLE IF EXISTS dbo.PhieuThietBi;
DROP TABLE IF EXISTS dbo.ThietBiThongSo;
DROP TABLE IF EXISTS dbo.DmThongSoThietBi;
DROP TABLE IF EXISTS dbo.ThietBi;
DROP TABLE IF EXISTS dbo.NguoiSuDungThietBi;
DROP TABLE IF EXISTS dbo.NhomThietBi;
DROP TABLE IF EXISTS dbo.DonViBoPhan;
DROP TABLE IF EXISTS dbo.DmDungChung;
