namespace MISA.SME.Domain
{
    /// <summary>
    /// Giao diện định nghĩa các phương thức cơ bản cho đối tượng thực thể (Entity).
    /// </summary>
    public interface IEntity
    {
        #region Methods

        /// <summary>
        /// Lấy giá trị ID của đối tượng thực thể.
        /// </summary>
        /// <returns>Giá trị ID của đối tượng thực thể.</returns>
        Guid GetId();

        /// <summary>
        /// Thiết lập giá trị ID cho đối tượng thực thể.
        /// </summary>
        /// <param name="id">Giá trị ID mới.</param>
        void SetId(Guid id);

        #endregion
    }
}
