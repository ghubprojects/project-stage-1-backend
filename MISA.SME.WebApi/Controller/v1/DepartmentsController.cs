using Microsoft.AspNetCore.Mvc;
using MISA.SME.Application;

namespace MISA.SME.WebApi.Controller
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : BaseController
    {
        #region Method

        /// <summary>
        /// API lấy danh sách tất cả đơn vị
        /// </summary>
        /// <param name="keyword">từ khóa chứa tên đơn vị</param>
        /// <returns>Danh sách tất cả đơn vị</returns>
        /// Created by: ttanh (15/09/2023)
        /// Modified by: ttanh (26/09/2023)
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string keyword)
        {
            var result = string.IsNullOrEmpty(keyword)
                ? await Mediator.Send(new GetAllDepartmentsQuery())
                : await Mediator.Send(new GetFilteringDepartmentsQuery(keyword));

            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// API lấy đơn vị theo ID
        /// </summary>
        /// <param name="id">ID đơn vị</param>
        /// <returns></returns>
        /// Created by: ttanh (15/09/2023)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await Mediator.Send(
                new GetDepartmentByIdQuery { DepartmentID = id });

            return StatusCode(StatusCodes.Status200OK, result);
        }

        #endregion
    }
}
