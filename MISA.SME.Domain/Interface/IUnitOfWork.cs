namespace MISA.SME.Domain
{
    /// <summary>
    /// Giao diện định nghĩa đơn vị làm việc với đối tượng Unit of Work
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        #region Properties

        /// <summary>
        /// Repository đối tượng nhân viên
        /// </summary>
        IEmployeeRepository EmployeeRepository { get; }

        /// <summary>
        /// Repository đối tượng đơn vị
        /// </summary>
        IDepartmentRepository DepartmentRepository { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Lưu các thay đổi vào cơ sở dữ liệu
        /// </summary>
        void Commit();

        #endregion
    }
}
