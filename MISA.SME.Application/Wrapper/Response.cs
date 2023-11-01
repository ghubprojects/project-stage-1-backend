namespace MISA.SME.Application
{
    /// <summary>
    /// Đối tượng chứa thông tin kết quả trả về từ các dịch vụ
    /// </summary>
    /// <typeparam name="T">Kiểu dữ liệu của dữ liệu kết quả</typeparam>
    public class Response<T>
    {
        #region Property

        /// <summary>
        /// Xác định trạng thái thành công của kết quả
        /// </summary>
        public bool Succeeded { get; set; }

        /// <summary>
        /// Thông điệp kết quả (thường được sử dụng trong trường hợp thất bại)
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Danh sách lỗi (nếu có)
        /// </summary>
        public List<string> Errors { get; set; }

        /// <summary>
        /// Tổng số bản ghi (thường được sử dụng trong trường hợp phân trang)
        /// </summary>
        public int TotalRecord { get; set; }

        /// <summary>
        /// Dữ liệu kết quả
        /// </summary>
        public T Data { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Khởi tạo một đối tượng Response
        /// </summary>
        public Response()
        {
        }

        /// <summary>
        /// Khởi tạo một đối tượng Response với dữ liệu và thông điệp
        /// </summary>
        /// <param name="data">Dữ liệu kết quả</param>
        /// <param name="message">Thông điệp kết quả (mặc định là null)</param>
        public Response(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }

        /// <summary>
        /// Khởi tạo một đối tượng Response với dữ liệu, tổng số bản ghi và thông điệp
        /// </summary>
        /// <param name="data">Dữ liệu kết quả</param>
        /// <param name="totalRecord">Tổng số bản ghi</param>
        /// <param name="message">Thông điệp kết quả (mặc định là null)</param>
        public Response(T data, int totalRecord, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
            TotalRecord = totalRecord;
        }

        /// <summary>
        /// Khởi tạo một đối tượng Response với thông điệp kết quả (trạng thái thất bại)
        /// </summary>
        /// <param name="message">Thông điệp kết quả</param>
        public Response(string message)
        {
            Succeeded = false;
            Message = message;
        }

        #endregion
    }
}
