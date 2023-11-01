namespace MISA.SME.Domain
{
    /// <summary>
    /// Giao diện định nghĩa các phương thức kiểm tra liên quan đến nhân viên
    /// </summary>
    public interface IEmployeeValidator
    {
        #region Methods

        /// <summary>
        /// Kiểm tra sự tồn tại của mã nhân viên trong hệ thống
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên cần kiểm tra</param>
        /// <returns>Task</returns>
        Task CheckExistEmployeeCodeAsync(string employeeCode);

        #endregion
    }
}
