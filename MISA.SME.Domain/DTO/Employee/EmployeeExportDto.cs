namespace MISA.SME.Domain
{
    public class EmployeeExportDto
    {
        #region Property

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string EmployeeCode { get; set; } = string.Empty;

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// Giới tính
        /// </summary>
        public string Gender { get; set; } = string.Empty;

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public string? DateOfBirth { get; set; }

        /// <summary>
        /// Chức danh
        /// </summary>
        public string PositionName { get; set; } = string.Empty;

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string DepartmentName { get; set; } = string.Empty;

        /// <summary>
        /// Tài khoản ngân hàng.
        /// </summary>
        public string BankAccount { get; set; } = string.Empty;

        /// <summary>
        /// Tên ngân hàng.
        /// </summary>
        public string BankName { get; set; } = string.Empty;

        #endregion
    }
}
