using Microsoft.AspNetCore.Http;

namespace MISA.SME.Domain
{
    /// <summary>
    /// Lớp ngoại lệ thể hiện tình trạng không hợp lệ (Invalid Exception)
    /// </summary>
    public class InvalidException : Exception
    {
        #region Properties

        /// <summary>
        /// Mã lỗi tương ứng với tình trạng không hợp lệ
        /// </summary>
        public int ErrorCode { get; set; } = StatusCodes.Status400BadRequest;

        #endregion

        #region Constructors

        /// <summary>
        /// Khởi tạo một instance mới của <see cref="InvalidException"/>
        /// </summary>
        public InvalidException() { }

        /// <summary>
        /// Khởi tạo một instance mới của <see cref="InvalidException"/> với mã lỗi
        /// </summary>
        /// <param name="errorCode">Mã lỗi</param>
        public InvalidException(int errorCode)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Khởi tạo một instance mới của <see cref="InvalidException"/> với thông điệp lỗi
        /// </summary>
        /// <param name="message">Thông điệp lỗi</param>
        public InvalidException(string message) : base(message) { }

        /// <summary>
        /// Khởi tạo một instance mới của <see cref="InvalidException"/> với thông điệp lỗi và mã lỗi
        /// </summary>
        /// <param name="message">Thông điệp lỗi</param>
        /// <param name="errorCode">Mã lỗi</param>
        public InvalidException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        #endregion
    }
}
