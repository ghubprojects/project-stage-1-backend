using MediatR;
using MISA.SME.Domain;

namespace MISA.SME.Application
{
    /// <summary>
    /// Yêu cầu (request) để lấy danh sách đơn vị sau khi lọc dựa trên từ khoá tìm kiếm
    /// </summary>
    public class GetFilteringDepartmentsQuery : IRequest<Response<List<DepartmentDto>>>
    {
        /// <summary>
        /// Từ khoá tìm kiếm để lọc danh sách đơn vị
        /// </summary>
        public string Keyword { get; set; } = string.Empty;

        /// <summary>
        /// Khởi tạo một yêu cầu (request) để lấy danh sách đơn vị sau khi lọc dựa trên từ khoá tìm kiếm
        /// </summary>
        /// <param name="keyword">Từ khoá tìm kiếm</param>
        public GetFilteringDepartmentsQuery(string keyword)
        {
            Keyword = keyword;
        }
    }

    /// <summary>
    /// Xử lý yêu cầu (request) để lấy danh sách đơn vị sau khi lọc dựa trên từ khoá tìm kiếm
    /// </summary>
    internal sealed class GetFilteringDepartmentsQueryHandler : IRequestHandler<GetFilteringDepartmentsQuery, Response<List<DepartmentDto>>>
    {
        private readonly IDepartmentServiceQuery _departmentServiceQuery;

        public GetFilteringDepartmentsQueryHandler(IDepartmentServiceQuery departmentServiceQuery)
        {
            _departmentServiceQuery = departmentServiceQuery;
        }

        /// <summary>
        /// Xử lý yêu cầu (request) để lấy danh sách đơn vị sau khi lọc dựa trên từ khoá tìm kiếm
        /// </summary>
        /// <param name="request">Yêu cầu (request) để lọc danh sách đơn vị</param>
        /// <param name="cancellationToken">Token hủy bỏ.</param>
        /// <returns>Kết quả chứa danh sách đơn vị đã lọc và số lượng đơn vị</returns>
        /// Created by: ttanh (30/09/2023)
        public async Task<Response<List<DepartmentDto>>> Handle(GetFilteringDepartmentsQuery request, CancellationToken cancellationToken)
        {
            var departmentList = await _departmentServiceQuery.GetFilteringAsync(request.Keyword);

            return new Response<List<DepartmentDto>>(departmentList, departmentList.Count);
        }
    }
}
