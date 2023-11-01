using MediatR;
using MISA.SME.Domain;

namespace MISA.SME.Application
{
    /// <summary>
    /// Yêu cầu (request) để lấy thông tin nhân viên theo ID
    /// </summary>
    public class GetEmployeeByIdQuery : IRequest<Response<EmployeeDto>>
    {
        /// <summary>
        /// ID nhân viên
        /// </summary>
        public Guid EmployeeID { get; set; }

        public GetEmployeeByIdQuery(Guid id)
        {
            EmployeeID = id;
        }
    }

    /// <summary>
    /// Xử lý yêu cầu (request) để lấy thông tin nhân viên theo ID
    /// </summary>
    internal sealed class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, Response<EmployeeDto>>
    {
        private readonly IEmployeeServiceQuery _employeeServiceQueries;

        public GetEmployeeByIdQueryHandler(IEmployeeServiceQuery employeeServiceQueries)
        {
            _employeeServiceQueries = employeeServiceQueries;
        }

        /// <summary>
        /// Xử lý yêu cầu (request) để lấy thông tin nhân viên theo ID
        /// </summary>
        /// <param name="request">Yêu cầu (request) để lấy thông tin nhân viên theo ID</param>
        /// <param name="cancellationToken">Token hủy bỏ</param>
        /// <returns>Thông tin nhân viên và kết quả xử lý</returns>
        /// Created by: ttanh (23/09/2023)
        public async Task<Response<EmployeeDto>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await _employeeServiceQueries.GetByIdAsync(request.EmployeeID);
            return new Response<EmployeeDto>(employee);
        }
    }
}
