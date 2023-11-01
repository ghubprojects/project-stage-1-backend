using MediatR;
using MISA.SME.Domain;

namespace MISA.SME.Application
{
    /// <summary>
    /// Yêu cầu (request) để lấy danh sách thông tin nhân viên theo từ khóa và phân trang
    /// </summary>
    public class GetFilteringAndPaginationEmployeesQuery : IRequest<Response<List<EmployeeDto>>>
    {
        /// <summary>
        /// Từ khóa tìm kiếm
        /// </summary>
        public string Keyword { get; set; } = string.Empty;

        /// <summary>
        /// Số lượng bản ghi trên mỗi trang
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// Vị trí bắt đầu của trang
        /// </summary>
        public int Offset { get; set; }

        public GetFilteringAndPaginationEmployeesQuery(string keyword, int limit, int offset)
        {
            Keyword = keyword;
            Limit = limit;
            Offset = offset;
        }
    }

    /// <summary>
    /// Xử lý yêu cầu (request) để lấy danh sách thông tin nhân viên theo từ khóa và phân trang
    /// </summary>
    internal sealed class GetFilteringAndPaginationEmployeesQueryHandler : IRequestHandler<GetFilteringAndPaginationEmployeesQuery, Response<List<EmployeeDto>>>
    {
        private readonly IEmployeeServiceQuery _employeeServiceQueries;

        public GetFilteringAndPaginationEmployeesQueryHandler(IEmployeeServiceQuery employeeServiceQueries)
        {
            _employeeServiceQueries = employeeServiceQueries;
        }

        /// <summary>
        /// Xử lý yêu cầu (request) để lấy danh sách thông tin nhân viên theo từ khóa và phân trang
        /// </summary>
        /// <param name="request">Yêu cầu (request) để lấy danh sách thông tin nhân viên theo từ khóa và phân trang</param>
        /// <param name="cancellationToken">Token hủy bỏ</param>
        /// <returns>Danh sách thông tin nhân viên và số lượng bản ghi</returns>
        /// Created by: ttanh (26/09/2023)
        public async Task<Response<List<EmployeeDto>>> Handle(GetFilteringAndPaginationEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employeeList = await _employeeServiceQueries.GetFilteringAndPaginationAsync(request.Keyword, request.Limit, request.Offset);

            return new Response<List<EmployeeDto>>(employeeList, employeeList.Count);
        }
    }
}
