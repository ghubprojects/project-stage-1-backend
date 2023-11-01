using Microsoft.AspNetCore.Http;

namespace MISA.SME.Domain
{
    /// <summary>
    /// Lớp ngoại lệ thể hiện tình trạng không tìm thấy (Not Found Exception)
    /// </summary>
    public class NotFoundException : Exception
    {
        #region Properties

        /// <summary>
        /// Mã lỗi tương ứng với tình trạng không tìm thấy
        /// </summary>
        public int ErrorCode { get; set; } = StatusCodes.Status404NotFound;

        #endregion

        #region Constructors

        /// <summary>
        /// Khởi tạo một instance mới của <see cref="NotFoundException"/>
        /// </summary>
        public NotFoundException()
        {
        }

        /// <summary>
        /// Khởi tạo một instance mới của <see cref="NotFoundException"/> với mã lỗi
        /// </summary>
        /// <param name="errorCode">Mã lỗi</param>
        public NotFoundException(int errorCode)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Khởi tạo một instance mới của <see cref="NotFoundException"/> với thông điệp lỗi
        /// </summary>
        /// <param name="message">Thông điệp lỗi</param>
        public NotFoundException(string message) : base(message)
        {
        }

        /// <summary>
        /// Khởi tạo một instance mới của <see cref="NotFoundException"/> với thông điệp lỗi và mã lỗi
        /// </summary>
        /// <param name="message">Thông điệp lỗi</param>
        /// <param name="errorCode">Mã lỗi</param>
        public NotFoundException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        #endregion
    }
}
