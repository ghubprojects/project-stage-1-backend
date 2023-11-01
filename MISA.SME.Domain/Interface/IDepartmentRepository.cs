namespace MISA.SME.Domain
{
    /// <summary>
    /// Giao diện định nghĩa các phương thức đặc biệt cho việc truy cập dữ liệu đơn vị (Department)
    /// </summary>
    public interface IDepartmentRepository : IBaseRepository<Department>
    {
        #region Methods

        /// <summary>
        /// Lấy danh sách các đơn vị dựa trên từ khóa tìm kiếm
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <returns>Danh sách đơn vị phù hợp với từ khóa tìm kiếm</returns>
        Task<List<DepartmentDto>> GetFilteringAsync(string keyword);

        #endregion
    }
}
