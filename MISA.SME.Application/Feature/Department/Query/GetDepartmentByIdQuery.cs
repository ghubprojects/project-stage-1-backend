using MediatR;
using MISA.SME.Domain;

namespace MISA.SME.Application
{
    /// <summary>
    /// Yêu cầu (request) để lấy thông tin một đơn vị theo ID
    /// </summary>
    public class GetDepartmentByIdQuery : IRequest<DepartmentDto>
    {
        /// <summary>
        /// ID của đơn vị cần lấy thông tin.
        /// </summary>
        public Guid DepartmentID { get; set; }
    }

    /// <summary>
    /// Xử lý yêu cầu (request) để lấy thông tin một đơn vị theo ID
    /// </summary>
    internal sealed class GetDepartmentByIdQueryHandler : IRequestHandler<GetDepartmentByIdQuery, DepartmentDto>
    {
        private readonly IDepartmentServiceQuery _departmentServiceQuery;

        public GetDepartmentByIdQueryHandler(IDepartmentServiceQuery departmentServiceQuery)
        {
            _departmentServiceQuery = departmentServiceQuery;
        }

        /// <summary>
        /// Xử lý yêu cầu (request) để lấy thông tin một đơn vị theo ID
        /// </summary>
        /// <param name="request">Yêu cầu (request) để lấy thông tin đơn vị</param>
        /// <param name="cancellationToken">Token hủy bỏ</param>
        /// <returns>Thông tin của đơn vị cần lấy</returns>
        /// Created by: ttanh (20/09/2023)
        public async Task<DepartmentDto> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            var department = await _departmentServiceQuery.GetByIdAsync(request.DepartmentID);
            return department;
        }
    }
}
