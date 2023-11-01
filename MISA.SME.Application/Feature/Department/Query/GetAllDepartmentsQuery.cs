using MediatR;
using MISA.SME.Domain;

namespace MISA.SME.Application
{
    /// <summary>
    /// Yêu cầu (request) để lấy danh sách tất cả các đơn vị
    /// </summary>
    public class GetAllDepartmentsQuery : IRequest<Response<List<DepartmentDto>>>
    {
    }

    /// <summary>
    /// Xử lý yêu cầu (request) để lấy danh sách tất cả các đơn vị
    /// </summary>
    public sealed class GetAllDepartmentsQueryHandler : IRequestHandler<GetAllDepartmentsQuery, Response<List<DepartmentDto>>>
    {
        private readonly IDepartmentServiceQuery _departmentServiceQuery;

        public GetAllDepartmentsQueryHandler(IDepartmentServiceQuery departmentServiceQuery)
        {
            _departmentServiceQuery = departmentServiceQuery;
        }

        /// <summary>
        /// Xử lý yêu cầu (request) để lấy danh sách tất cả các đơn vị
        /// </summary>
        /// <param name="request">Yêu cầu (request) để lấy danh sách phòng ban</param>
        /// <param name="cancellationToken">Token hủy bỏ</param>
        /// <returns>Kết quả chứa danh sách phòng ban và số lượng phòng ban</returns>
        /// Created by: ttanh (27/09/2023)
        public async Task<Response<List<DepartmentDto>>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
        {
            var departmentList = await _departmentServiceQuery.GetAllAsync();
            return new Response<List<DepartmentDto>>(departmentList, departmentList.Count);
        }
    }
}
