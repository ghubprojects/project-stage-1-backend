using MISA.SME.Application;
using MISA.SME.Domain;
using System.Text.Json;

namespace MISA.SME.WebApi
{
    /// <summary>
    /// Middleware xử lý các ngoại lệ trong ứng dụng
    /// </summary>
    public class ExceptionMiddleware
    {
        #region Fields

        private readonly RequestDelegate _next;

        #endregion

        #region Constructors

        /// <summary>
        /// Khởi tạo một instance mới của <see cref="ExceptionMiddleware"/>
        /// </summary>
        /// <param name="next">Hàm xử lý HTTP request tiếp theo</param>
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Hàm xử lý các ngoại lệ trong ứng dụng
        /// </summary>
        /// <param name="context">Đối tượng HttpContext</param>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Xử lý ngoại lệ và trả về phản hồi HTTP tương ứng
        /// </summary>
        /// <param name="context">Đối tượng HttpContext</param>
        /// <param name="exception">Ngoại lệ được xử lý</param>
        public async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            Console.WriteLine(exception);

            var response = context.Response;
            response.ContentType = "application/json";
            var responseModel = new Response<string>();

            var jsonSerializerOptions = new JsonSerializerOptions() { PropertyNamingPolicy = null };

            switch (exception)
            {
                case NotFoundException e:
                    response.StatusCode = StatusCodes.Status404NotFound;
                    await response.WriteAsJsonAsync(new BaseException()
                    {
                        ErrorCode = e.ErrorCode,
                        UserMessage = "Không tìm thấy tài nguyên",
                        DevMessage = exception.Message,
                        TraceId = context.TraceIdentifier,
                        MoreInfo = exception.HelpLink
                    }, jsonSerializerOptions);
                    break;

                case ConflictException e:
                    response.StatusCode = StatusCodes.Status409Conflict;
                    await response.WriteAsJsonAsync(new BaseException()
                    {
                        ErrorCode = e.ErrorCode,
                        UserMessage = exception.Message,
                        DevMessage = exception.Message,
                        TraceId = context.TraceIdentifier,
                        MoreInfo = exception.HelpLink
                    }, jsonSerializerOptions);
                    break;

                case ValidateException e:
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    responseModel.Errors = e.Errors;
                    await response.WriteAsJsonAsync(responseModel, jsonSerializerOptions);
                    break;

                default:
                    response.StatusCode = StatusCodes.Status500InternalServerError;
                    await response.WriteAsJsonAsync(new BaseException()
                    {
                        ErrorCode = context.Response.StatusCode,
                        UserMessage = "Lỗi hệ thống. Vui lòng liên hệ MISA để được hỗ trợ.",
                        DevMessage = exception.Message,
                        TraceId = context.TraceIdentifier,
                        MoreInfo = exception.HelpLink
                    }, jsonSerializerOptions);
                    break;
            }
        }

        #endregion
    }
}
