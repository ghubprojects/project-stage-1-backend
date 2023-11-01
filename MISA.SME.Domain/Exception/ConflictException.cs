using Microsoft.AspNetCore.Http;

namespace MISA.SME.Domain
{
    /// <summary>
    /// Lớp ngoại lệ thể hiện tình trạng xung đột (Conflict Exception)
    /// </summary>
    public class ConflictException : Exception
    {
        #region Properties

        /// <summary>
        /// Mã lỗi tương ứng với tình trạng xung đột
        /// </summary>
        public int ErrorCode { get; set; } = StatusCodes.Status409Conflict;

        #endregion

        #region Constructors

        /// <summary>
        /// Khởi tạo một instance mới của <see cref="ConflictException"/>
        /// </summary>
        public ConflictException()
        {
        }

        /// <summary>
        /// Khởi tạo một instance mới của <see cref="ConflictException"/> với mã lỗi
        /// </summary>
        /// <param name="errorCode">Mã lỗi</param>
        public ConflictException(int errorCode)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Khởi tạo một instance mới của <see cref="ConflictException"/> với thông điệp lỗi
        /// </summary>
        /// <param name="message">Thông điệp lỗi</param>
        public ConflictException(string message) : base(message)
        {
        }

        /// <summary>
        /// Khởi tạo một instance mới của <see cref="ConflictException"/> với thông điệp lỗi và mã lỗi
        /// </summary>
        /// <param name="message">Thông điệp lỗi</param>
        /// <param name="errorCode">Mã lỗi</param>
        public ConflictException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        #endregion
    }
}
