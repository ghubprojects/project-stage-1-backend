namespace MISA.SME.Application
{
    /// <summary>
    /// Giao diện dịch vụ cơ bản cho các thao tác truy vấn dữ liệu
    /// </summary>
    /// <typeparam name="TEntityDto">Đối tượng Dto</typeparam>
    public interface IBaseServiceQuery<TEntityDto>
    {
        /// <summary>
        /// Lấy danh sách tất cả đối tượng
        /// </summary>
        /// <returns>Danh sách tất cả đối tượng</returns>
        Task<List<TEntityDto>> GetAllAsync();

        /// <summary>
        /// Lấy danh sách đối tượng phân trang
        /// </summary>
        /// <param name="limit">Số lượng đối tượng trên mỗi trang</param>
        /// <param name="offset">Số trang bắt đầu</param>
        /// <returns>Danh sách đối tượng phân trang</returns>
        Task<List<TEntityDto>> GetPaginationAsync(int limit, int offset);

        /// <summary>
        /// Lấy danh sách đối tượng sau khi áp dụng bộ lọc
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <returns>Danh sách đối tượng sau khi áp dụng bộ lọc</returns>
        Task<List<TEntityDto>> GetFilteringAsync(string keyword);

        /// <summary>
        /// Lấy danh sách đối tượng sau khi áp dụng bộ lọc và phân trang
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <param name="limit">Số lượng đối tượng trên mỗi trang</param>
        /// <param name="offset">Số trang bắt đầu</param>
        /// <returns>Danh sách đối tượng sau khi áp dụng bộ lọc và phân trang</returns>
        Task<List<TEntityDto>> GetFilteringAndPaginationAsync(string keyword, int limit, int offset);

        /// <summary>
        /// Lấy đối tượng theo ID
        /// </summary>
        /// <param name="id">ID của đối tượng cần lấy</param>
        /// <returns>Đối tượng theo ID</returns>
        Task<TEntityDto> GetByIdAsync(Guid id);

        /// <summary>
        /// Lấy đối tượng theo mã.
        /// </summary>
        /// <param name="code">Mã của đối tượng cần lấy</param>
        /// <returns>Đối tượng theo mã</returns>
        Task<TEntityDto> GetByCodeAsync(string code);

        /// <summary>
        /// Xuất dữ liệu thành tệp Excel
        /// </summary>
        /// <returns>Dữ liệu tệp Excel dưới dạng mảng byte</returns>
        Task<byte[]> ExportToExcelAsync(string? keyword);
    }
}
