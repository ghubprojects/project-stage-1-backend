using Microsoft.AspNetCore.Mvc;
using MISA.SME.Application;
using MISA.SME.Domain;

namespace MISA.SME.WebApi.Controller
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController
    {
        #region Method

        /// <summary>
        /// API lấy tất cả nhân viên theo từ khóa (nếu có) và phân trang (nếu có)
        /// </summary>
        /// <param name="keyword">từ khóa chứa mã nhân viên hoặc tên nhân viên</param>
        /// <param name="limit">số bản ghi trả về</param>
        /// <param name="offset">hàng bắt đầu truy xuất</param>
        /// <returns></returns>
        /// Created by: ttanh (01/09/2023)
        /// Modified by: ttanh (20/09/2023)
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] string keyword, [FromQuery] int limit, [FromQuery] int offset)
        {
            var result = new Response<List<EmployeeDto>>();

            // danh sách được phân trang
            if (limit > 0 && offset >= 0)
                result = string.IsNullOrEmpty(keyword)
                    ? await Mediator.Send(
                        new GetPaginationEmployeesQuery(limit, offset))
                    : await Mediator.Send(
                        new GetFilteringAndPaginationEmployeesQuery(keyword, limit, offset));
            // danh sách không phân trang
            else
                result = string.IsNullOrEmpty(keyword)
                    ? await Mediator.Send(
                        new GetAllEmployeesQuery())
                    : await Mediator.Send(
                        new GetFilteringEmployeesQuery(keyword));

            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// API lấy nhân viên theo ID
        /// </summary>
        /// <param name="id">ID nhân viên</param>
        /// <returns></returns>
        /// Created by: ttanh (01/09/2023)
        /// Modified by: ttanh (02/09/2023)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            return StatusCode(
                StatusCodes.Status200OK,
                await Mediator.Send(new GetEmployeeByIdQuery(id)));
        }

        /// <summary>
        /// API xuất khẩu danh sách nhân viên
        /// </summary>
        /// <returns></returns>
        /// Created by: ttanh (02/10/2023)
        [HttpGet("export")]
        public async Task<IActionResult> ExportToExcelAsync([FromQuery] string keyword)
        {
            // Lấy dữ liệu cho file excel
            var excelData = await Mediator.Send(new ExportEmployeesToExcelQuery(keyword));

            // Context type dành cho file excel
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            // Tạo 1 unique filename cho file excel
            string fileName = $"Danh_sach_nhan_vien_{DateTime.Now:yyyyMMddHHmmss}.xlsx";

            // Trả về file được tự động download khi gọi API
            return File(excelData, Response.ContentType, fileName);
        }

        /// <summary>
        /// API thêm nhân viên
        /// </summary>
        /// <param name="command">Dữ liệu nhân viên</param>
        /// <returns></returns>
        /// Created by: ttanh (09/09/2023)
        /// Modified by: ttanh (14/09/2023)
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CreateEmployeeCommand command)
        {
            return StatusCode(
                StatusCodes.Status201Created,
                await Mediator.Send(command));
        }

        /// <summary>
        /// API cập nhật dữ liệu nhân viên
        /// </summary>
        /// <param name="command">Dữ liệu nhân viên đã thay đổi</param>
        /// <returns></returns>
        /// Created by: ttanh (11/09/2023)
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateEmployeeCommand command)
        {
            return StatusCode(
                StatusCodes.Status200OK,
                await Mediator.Send(command));
        }

        /// <summary>
        /// API xóa 1 nhân viên theo ID
        /// </summary>
        /// <param name="id">ID nhân viên bị xóa</param>
        /// <returns></returns>
        /// Created by: ttanh (08/09/2023)
        /// Modified by: ttanh (14/09/2023)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            return StatusCode(StatusCodes.Status200OK, await Mediator.Send(new DeleteEmployeeByIdCommand { EmployeeID = id }));
        }

        /// <summary>
        /// API xóa nhiều nhân viên theo IDs
        /// </summary>
        /// <param name="ids">Danh sách ID nhân viên xoá</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteMultipleAsync([FromBody] List<Guid> ids)
        {
            return StatusCode(StatusCodes.Status200OK, await Mediator.Send(new DeleteMultipleEmployeesCommand { EmployeeIDs = ids }));
        }

        #endregion
    }
}