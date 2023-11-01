using MediatR;
using MISA.SME.Domain;

namespace MISA.SME.Application
{
    /// <summary>
    /// Yêu cầu (request) để lấy danh sách thông tin nhân viên theo phân trang
    /// </summary>
    public class GetPaginationEmployeesQuery : IRequest<Response<List<EmployeeDto>>>
    {
        /// <summary>
        /// Số lượng bản ghi trên mỗi trang
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// Vị trí bắt đầu của trang
        /// </summary>
        public int Offset { get; set; }

        public GetPaginationEmployeesQuery(int limit, int offset)
        {
            Limit = limit;
            Offset = offset;
        }
    }

    /// <summary>
    /// Xử lý yêu cầu (request) để lấy danh sách thông tin nhân viên theo phân trang
    /// </summary>
    internal sealed class GetPaginationEmployeesQueryHandler : IRequestHandler<GetPaginationEmployeesQuery, Response<List<EmployeeDto>>>
    {
        private readonly IEmployeeServiceQuery _employeeServiceQueries;

        public GetPaginationEmployeesQueryHandler(IEmployeeServiceQuery employeeServiceQueries)
        {
            _employeeServiceQueries = employeeServiceQueries;
        }

        /// <summary>
        /// Xử lý yêu cầu (request) để lấy danh sách thông tin nhân viên theo phân trang
        /// </summary>
        /// <param name="request">Yêu cầu (request) để lấy danh sách thông tin nhân viên theo phân trang</param>
        /// <param name="cancellationToken">Token hủy bỏ</param>
        /// <returns>Danh sách thông tin nhân viên và số lượng bản ghi</returns>
        /// Created by: ttanh (26/09/2023)
        public async Task<Response<List<EmployeeDto>>> Handle(GetPaginationEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employeeList = await _employeeServiceQueries.GetPaginationAsync(request.Limit, request.Offset);

            return new Response<List<EmployeeDto>>(employeeList, employeeList.Count());
        }
    }
}
