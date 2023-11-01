using MediatR;

namespace MISA.SME.Application
{
    /// <summary>
    /// Yêu cầu (request) để xóa nhiều thông tin nhân viên theo danh sách ID
    /// </summary>
    public class DeleteMultipleEmployeesCommand : IRequest<Response<int>>
    {
        /// <summary>
        /// Danh sách ID của nhân viên cần xóa
        /// </summary>
        public List<Guid> EmployeeIDs { get; set; } = new List<Guid>();
    }

    /// <summary>
    /// Xử lý yêu cầu (request) để xóa nhiều thông tin nhân viên theo danh sách ID
    /// </summary>
    internal sealed class DeleteMultipleEmployeesCommandHandler : IRequestHandler<DeleteMultipleEmployeesCommand, Response<int>>
    {
        private readonly IEmployeeServiceCommand _employeeServiceCommands;

        public DeleteMultipleEmployeesCommandHandler(IEmployeeServiceCommand employeeServiceCommands)
        {
            _employeeServiceCommands = employeeServiceCommands;
        }

        /// <summary>
        /// Xử lý yêu cầu (request) để xóa nhiều thông tin nhân viên theo danh sách ID
        /// </summary>
        /// <param name="request">Yêu cầu (request) để xóa nhiều thông tin nhân viên</param>
        /// <param name="cancellationToken">Token hủy bỏ</param>
        /// <returns>Số dòng bị ảnh hưởng bởi việc xóa nhiều thông tin nhân viên</returns>
        /// Created by: ttanh (20/09/2023)
        public async Task<Response<int>> Handle(DeleteMultipleEmployeesCommand request, CancellationToken cancellationToken)
        {
            var affectedRows = await _employeeServiceCommands.DeleteMultipleAsync(request.EmployeeIDs);
            return new Response<int>(affectedRows);
        }
    }
}
