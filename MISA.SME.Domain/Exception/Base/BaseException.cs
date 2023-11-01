namespace MISA.SME.Domain
{
    /// <summary>
    /// Lớp cơ sở cho việc xử lý ngoại lệ trong hệ thống
    /// </summary>
    public class BaseException
    {
        #region Properties

        /// <summary>
        /// Mã lỗi
        /// </summary>
        public int ErrorCode;

        /// <summary>
        /// Thông điệp dành cho nhà phát triển (Developer Message)
        /// </summary>
        public string? DevMessage { get; set; }

        /// <summary>
        /// Thông điệp dành cho người dùng (User Message)
        /// </summary>
        public string? UserMessage { get; set; }

        /// <summary>
        /// Định danh truy vết (Trace ID)
        /// </summary>
        public string? TraceId { get; set; }

        /// <summary>
        /// Thông tin bổ sung liên quan đến lỗi (More Information)
        /// </summary>
        public string? MoreInfo { get; set; }

        /// <summary>
        /// Danh sách các lỗi con hoặc chi tiết thêm về lỗi (Errors)
        /// </summary>
        public object? Errors { get; set; }

        #endregion               
    }
}
