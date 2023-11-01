using MediatR;
using MISA.SME.Domain;

namespace MISA.SME.Application
{
    /// <summary>
    /// Yêu cầu (request) để lấy danh sách thông tin nhân viên theo từ khóa
    /// </summary>
    public class GetFilteringEmployeesQuery : IRequest<Response<List<EmployeeDto>>>
    {
        /// <summary>
        /// Từ khóa tìm kiếm
        /// </summary>
        public string Keyword { get; set; } = string.Empty;

        public GetFilteringEmployeesQuery(string keyword)
        {
            Keyword = keyword;
        }
    }

    /// <summary>
    /// Xử lý yêu cầu (request) để lấy danh sách thông tin nhân viên theo từ khóa
    /// </summary>
    internal sealed class GetFilteringEmployeesQueryHandler : IRequestHandler<GetFilteringEmployeesQuery, Response<List<EmployeeDto>>>
    {
        private readonly IEmployeeServiceQuery _employeeServiceQueries;

        public GetFilteringEmployeesQueryHandler(IEmployeeServiceQuery employeeServiceQueries)
        {
            _employeeServiceQueries = employeeServiceQueries;
        }

        /// <summary>
        /// Xử lý yêu cầu (request) để lấy danh sách thông tin nhân viên theo từ khóa
        /// </summary>
        /// <param name="request">Yêu cầu (request) để lấy danh sách thông tin nhân viên theo từ khóa</param>
        /// <param name="cancellationToken">Token hủy bỏ</param>
        /// <returns>Danh sách thông tin nhân viên và số lượng bản ghi</returns>
        /// Created by: ttanh (26/09/2023)
        public async Task<Response<List<EmployeeDto>>> Handle(GetFilteringEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employeeList = await _employeeServiceQueries.GetFilteringAsync(request.Keyword);

            return new Response<List<EmployeeDto>>(employeeList, employeeList.Count());
        }
    }
}
