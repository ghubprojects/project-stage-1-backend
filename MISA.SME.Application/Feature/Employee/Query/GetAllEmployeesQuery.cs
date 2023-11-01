using MediatR;
using MISA.SME.Domain;

namespace MISA.SME.Application
{
    /// <summary>
    /// Yêu cầu (request) để lấy tất cả thông tin nhân viên
    /// </summary>
    public class GetAllEmployeesQuery : IRequest<Response<List<EmployeeDto>>>
    {
    }

    /// <summary>
    /// Xử lý yêu cầu (request) để lấy tất cả thông tin nhân viên
    /// </summary>
    internal sealed class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, Response<List<EmployeeDto>>>
    {
        private readonly IEmployeeServiceQuery _employeeServiceQuery;

        public GetAllEmployeesQueryHandler(IEmployeeServiceQuery employeeServiceQuery)
        {
            _employeeServiceQuery = employeeServiceQuery;
        }

        /// <summary>
        /// Xử lý yêu cầu (request) để lấy tất cả thông tin nhân viên
        /// </summary>
        /// <param name="request">Yêu cầu (request) để lấy tất cả thông tin nhân viên</param>
        /// <param name="cancellationToken">Token hủy bỏ</param>
        /// <returns>Danh sách thông tin nhân viên và số lượng bản ghi</returns>
        /// Created by: ttanh (18/09/2023)
        public async Task<Response<List<EmployeeDto>>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employeeList = await _employeeServiceQuery.GetAllAsync();
            return new Response<List<EmployeeDto>>(employeeList, employeeList.Count);
        }
    }
}
