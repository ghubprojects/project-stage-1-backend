using MISA.SME.Domain;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace MISA.SME.Application
{
    /// <summary>
    /// Lớp hỗ trợ xuất dữ liệu vào file Excel
    /// </summary>
    public static class ExportToExcelHelper
    {
        /// <summary>
        /// Tạo dữ liệu cho file Excel
        /// </summary>
        /// <param name="employeeExportDtoList">Danh sách đối tượng nhân viên xuất khẩu</param>
        /// <param name="templateFileInfo">Thông tin về file Excel mẫu</param>
        /// <returns>Dữ liệu nhân viên cho file Excel</returns>
        /// <remarks>Created by: ttanh (02/10/2023)</remarks>
        public static byte[] GenerateExcelFile(List<EmployeeExportDto> employeeExportDtoList, FileInfo templateFileInfo)
        {
            // Thiết lập license context của EPPlus
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(templateFileInfo))
            {
                // Lấy trang "Danh sách nhân viên"
                var worksheet = package.Workbook.Worksheets[0];

                // Điền dữ liệu nhân viên vào sheet bắt đầu từ hàng 4 với số thứ tự là 1
                int currentRow = 4;
                int employeeIndex = 1;

                // Xoá dữ liệu nhân viên và border style từng hàng của file template excel
                for (var row = currentRow; row <= worksheet.Dimension.End.Row; row++)
                {
                    for (var col = 1; col < worksheet.Dimension.End.Column; col++)
                    {
                        worksheet.Cells[row, col].Value = "";
                    }

                    var rowRange = worksheet.Cells[row, 1, row, worksheet.Dimension.End.Column];
                    rowRange.Style.Border.Top.Style = ExcelBorderStyle.None;
                    rowRange.Style.Border.Left.Style = ExcelBorderStyle.None;
                    rowRange.Style.Border.Right.Style = ExcelBorderStyle.None;
                    rowRange.Style.Border.Bottom.Style = ExcelBorderStyle.None;
                }

                foreach (var employeeExportDto in employeeExportDtoList)
                {
                    // Điền số thứ tự nhân viên ở cột 1
                    worksheet.Cells[currentRow, 1].Value = employeeIndex;

                    // Từ cột 2 trở đi, điền dữ liệu nhân viên
                    int currentCol = 2;
                    foreach (var property in typeof(EmployeeExportDto).GetProperties())
                    {
                        var cellValue = property.GetValue(employeeExportDto);
                        worksheet.Cells[currentRow, currentCol].Value = cellValue;
                        currentCol++;
                    }

                    // Áp dụng border cho những hàng mới được thêm
                    var rowRange = worksheet.Cells[currentRow, 1, currentRow, worksheet.Dimension.End.Column];
                    rowRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    rowRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    rowRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    rowRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                    employeeIndex++;
                    currentRow++;
                }

                return package.GetAsByteArray();
            }
        }
    }
}
