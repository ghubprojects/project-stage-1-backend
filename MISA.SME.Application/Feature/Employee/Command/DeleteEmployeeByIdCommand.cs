using MediatR;

namespace MISA.SME.Application
{
    /// <summary>
    /// Yêu cầu (request) để xóa thông tin một nhân viên theo ID
    /// </summary>
    public class DeleteEmployeeByIdCommand : IRequest<Response<int>>
    {
        /// <summary>
        /// ID của nhân viên cần xóa
        /// </summary>
        public Guid EmployeeID { get; set; }
    }

    /// <summary>
    /// Xử lý yêu cầu (request) để xóa thông tin một nhân viên theo ID
    /// </summary>
    internal sealed class DeleteEmployeeByIdCommandHandler : IRequestHandler<DeleteEmployeeByIdCommand, Response<int>>
    {
        private readonly IEmployeeServiceCommand _employeeServiceCommands;

        public DeleteEmployeeByIdCommandHandler(IEmployeeServiceCommand employeeServiceCommands)
        {
            _employeeServiceCommands = employeeServiceCommands;
        }

        /// <summary>
        /// Xử lý yêu cầu (request) để xóa thông tin một nhân viên theo ID
        /// </summary>
        /// <param name="request">Yêu cầu (request) để xóa thông tin nhân viên</param>
        /// <param name="cancellationToken">Token hủy bỏ</param>
        /// <returns>Số dòng bị ảnh hưởng bởi việc xóa thông tin nhân viên</returns>
        /// Created by: ttanh (20/09/2023)
        public async Task<Response<int>> Handle(DeleteEmployeeByIdCommand request, CancellationToken cancellationToken)
        {
            var affectedRows = await _employeeServiceCommands.DeleteAsync(request.EmployeeID);
            return new Response<int>(affectedRows);
        }
    }
}
