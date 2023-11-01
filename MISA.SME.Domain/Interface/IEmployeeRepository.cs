namespace MISA.SME.Domain
{
    /// <summary>
    /// Giao diện định nghĩa các phương thức đặc biệt cho việc truy cập dữ liệu nhân viên (Employee)
    /// </summary>
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        #region Methods

        /// <summary>
        /// Lấy thông tin nhân viên dựa trên mã nhân viên
        /// </summary>
        /// <param name="code">Mã nhân viên</param>
        /// <returns>Thông tin nhân viên phù hợp với mã nhân viên</returns>
        Task<EmployeeDto> GetByCodeAsync(string code);

        /// <summary>
        /// Lấy danh sách nhân viên phân trang
        /// </summary>
        /// <param name="limit">Số lượng bản ghi trên mỗi trang</param>
        /// <param name="offset">Vị trí bắt đầu của trang</param>
        /// <returns>Danh sách nhân viên phân trang</returns>
        Task<List<EmployeeDto>> GetPaginationAsync(int limit, int offset);

        /// <summary>
        /// Lấy danh sách nhân viên dựa trên từ khóa tìm kiếm
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <returns>Danh sách nhân viên phù hợp với từ khóa tìm kiếm</returns>
        Task<List<EmployeeDto>> GetFilteringAsync(string keyword);

        /// <summary>
        /// Lấy danh sách nhân viên dựa trên từ khóa tìm kiếm và phân trang
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <param name="limit">Số lượng bản ghi trên mỗi trang</param>
        /// <param name="offset">Vị trí bắt đầu của trang</param>
        /// <returns>Danh sách nhân viên phù hợp với từ khóa tìm kiếm và phân trang</returns>
        Task<List<EmployeeDto>> GetFilteringAndPaginationAsync(string keyword, int limit, int offset);

        #endregion
    }
}
