namespace MISA.SME.Domain
{
    /// <summary>
    /// Giao diện định nghĩa các phương thức cơ bản cho việc truy cập dữ liệu của một đối tượng
    /// </summary>
    /// <typeparam name="TEntity">Kiểu dữ liệu của đối tượng</typeparam>
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        #region Methods

        /// <summary>
        /// Lấy danh sách tất cả các đối tượng
        /// </summary>
        /// <returns>Danh sách đối tượng</returns>
        Task<List<TEntity>> GetAllAsync();

        /// <summary>
        /// Lấy đối tượng theo ID
        /// </summary>
        /// <param name="id">ID của đối tượng</param>
        /// <returns>Đối tượng tìm thấy hoặc null nếu không tìm thấy</returns>
        Task<TEntity> GetByIdAsync(Guid id);

        /// <summary>
        /// Thêm một đối tượng mới vào cơ sở dữ liệu
        /// </summary>
        /// <param name="entity">Đối tượng cần thêm</param>
        /// <returns>Số bản ghi được thêm vào cơ sở dữ liệu</returns>
        Task<int> AddAsync(TEntity entity);

        /// <summary>
        /// Thêm nhiều đối tượng mới vào cơ sở dữ liệu
        /// </summary>
        /// <param name="entities">Danh sách đối tượng cần thêm</param>
        /// <returns>Số bản ghi được thêm vào cơ sở dữ liệu</returns>
        Task<int> AddMultipleAsync(List<TEntity> entities);

        /// <summary>
        /// Cập nhật thông tin của một đối tượng
        /// </summary>
        /// <param name="entity">Đối tượng cần cập nhật</param>
        /// <returns>Số bản ghi được cập nhật trong cơ sở dữ liệu</returns>
        Task<int> UpdateAsync(TEntity entity);

        /// <summary>
        /// Cập nhật thông tin của nhiều đối tượng
        /// </summary>
        /// <param name="entities">Danh sách đối tượng cần cập nhật</param>
        /// <returns>Số bản ghi được cập nhật trong cơ sở dữ liệu</returns>
        Task<int> UpdateMultipleAsync(List<TEntity> entities);

        /// <summary>
        /// Xóa đối tượng theo ID
        /// </summary>
        /// <param name="id">ID của đối tượng cần xóa</param>
        /// <returns>Số bản ghi bị xóa khỏi cơ sở dữ liệu</returns>
        Task<int> DeleteAsync(Guid id);

        /// <summary>
        /// Xóa nhiều đối tượng theo danh sách ID
        /// </summary>
        /// <param name="ids">Danh sách ID của các đối tượng cần xóa</param>
        /// <returns>Số bản ghi bị xóa khỏi cơ sở dữ liệu</returns>
        Task<int> DeleteMultipleAsync(List<Guid> ids);

        #endregion
    }
}
