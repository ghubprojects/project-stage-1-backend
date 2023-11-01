using FluentValidation.Results;

namespace MISA.SME.Domain
{
    /// <summary>
    /// Lớp ngoại lệ thể hiện việc xảy ra lỗi trong quá trình xác thực (Validation Exception)
    /// </summary>
    public class ValidateException : Exception
    {
        #region Properties

        /// <summary>
        /// Danh sách các thông báo lỗi
        /// </summary>
        public List<string> Errors { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Khởi tạo một instance mới của <see cref="ValidateException"/> với thông điệp lỗi mặc định
        /// </summary>
        public ValidateException() : base("Có một hoặc nhiều lỗi xác thực đã xảy ra.")
        {
            Errors = new List<string>();
        }

        /// <summary>
        /// Khởi tạo một instance mới của <see cref="ValidateException"/> với danh sách các lỗi xác thực
        /// </summary>
        /// <param name="failures">Danh sách các lỗi xác thực</param>
        public ValidateException(List<ValidationFailure> failures) : this()
        {
            foreach (var failure in failures)
            {
                Errors.Add(failure.ErrorMessage);
            }
        }

        #endregion
    }
}
