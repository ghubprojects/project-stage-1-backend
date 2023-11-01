using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MISA.SME.WebApi.Controller
{
    /// <summary>
    /// Lớp cơ sở cho tất cả các Controller trong ứng dụng
    /// </summary>
    public abstract class BaseController : ControllerBase
    {
        #region Fields

        private IMediator _mediator;

        #endregion

        #region Properties

        /// <summary>
        /// Đối tượng Mediator để thực hiện các yêu cầu và xử lý sự kiện
        /// </summary>
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        #endregion
    }
}
