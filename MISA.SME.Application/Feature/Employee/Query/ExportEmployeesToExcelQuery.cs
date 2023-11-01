using MediatR;

namespace MISA.SME.Application
{
    /// <summary>
    /// Yêu cầu (request) để xuất dữ liệu nhân viên ra file Excel
    /// </summary>
    public class ExportEmployeesToExcelQuery : IRequest<byte[]>
    {
        /// <summary>
        /// Từ khóa tìm kiếm
        /// </summary>
        public string? Keyword { get; set; } = string.Empty;

        public ExportEmployeesToExcelQuery(string? keyword)
        {
            Keyword = keyword;
        }
    }

    /// <summary>
    /// Xử lý yêu cầu (request) để xuất dữ liệu nhân viên ra file Excel
    /// </summary>
    internal sealed class ExportEmployeesToExcelQueryHandler : IRequestHandler<ExportEmployeesToExcelQuery, byte[]>
    {
        private readonly IEmployeeServiceQuery _employeeServiceQuery;

        public ExportEmployeesToExcelQueryHandler(IEmployeeServiceQuery employeeServiceQuery)
        {
            _employeeServiceQuery = employeeServiceQuery;
        }

        /// <summary>
        /// Xử lý yêu cầu (request) để xuất dữ liệu nhân viên ra file Excel
        /// </summary>
        /// <param name="request">Yêu cầu (request) để xuất dữ liệu ra Excel</param>
        /// <param name="cancellationToken">Token hủy bỏ</param>
        /// <returns>Mảng byte chứa dữ liệu file Excel</returns>
        /// <remarks>Created by: ttanh (02/10/2023)</remarks>
        public async Task<byte[]> Handle(ExportEmployeesToExcelQuery request, CancellationToken cancellationToken)
        {
            return await _employeeServiceQuery.ExportToExcelAsync(request.Keyword);
        }
    }
}
