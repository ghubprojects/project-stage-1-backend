namespace MISA.SME.Application
{
    /// <summary>
    /// Giao diện dịch vụ cơ bản cho các thao tác CRUD (Thêm, Sửa, Xóa)
    /// </summary>
    /// <typeparam name="TEntityDto">Đối tượng Dto</typeparam>
    /// <typeparam name="TEntityCreateDto">Đối tượng Dto cho thêm mới</typeparam>
    /// <typeparam name="TEntityUpdateDto">Đối tượng Dto cho cập nhật</typeparam>
    public interface IBaseServiceCommand<TEntityDto, TEntityCreateDto, TEntityUpdateDto>
    {
        /// <summary>
        /// Thêm đối tượng mới
        /// </summary>
        /// <param name="entity">Đối tượng Dto cho thêm mới</param>
        /// <returns>Số lượng bản ghi được thêm mới</returns>
        Task<int> AddAsync(TEntityCreateDto entity);

        /// <summary>
        /// Cập nhật đối tượng
        /// </summary>
        /// <param name="entity">Đối tượng Dto cho cập nhật</param>
        /// <returns>Số lượng bản ghi đã được cập nhật.</returns>
        Task<int> UpdateAsync(TEntityUpdateDto entity);

        /// <summary>
        /// Xóa đối tượng theo ID
        /// </summary>
        /// <param name="id">ID của đối tượng cần xóa</param>
        /// <returns>Số lượng bản ghi đã bị xóa</returns>
        Task<int> DeleteAsync(Guid id);

        /// <summary>
        /// Xóa nhiều đối tượng theo danh sách ID
        /// </summary>
        /// <param name="ids">Danh sách ID của các đối tượng cần xóa</param>
        /// <returns>Số lượng bản ghi đã bị xóa</returns>
        Task<int> DeleteMultipleAsync(List<Guid> ids);
    }
}
