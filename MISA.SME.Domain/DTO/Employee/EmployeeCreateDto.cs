namespace MISA.SME.Domain
{
    public class EmployeeCreateDto
    {
        #region Property

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTimeOffset? DateOfBirth { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public Gender? Gender { get; set; }

        /// <summary>
        /// Số căn cước công dân
        /// </summary>
        public string? IdentityNumber { get; set; }

        /// <summary>
        /// Ngày cấp căn cước công dân
        /// </summary>
        public DateTimeOffset? IdentityIssuedDate { get; set; }

        /// <summary>
        /// Nơi cấp căn cước công dân
        /// </summary>
        public string? IdentityIssuedPlace { get; set; }

        /// <summary>
        /// Chức danh
        /// </summary>
        public string? PositionName { get; set; }

        /// <summary>
        /// ID đơn vị
        /// </summary>
        public Guid? DepartmentID { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string? DepartmentName { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Số điện thoại di động.
        /// </summary>
        public string? MobilePhone { get; set; }

        /// <summary>
        /// Số điện thoại cố định.
        /// </summary>
        public string? LandlinePhone { get; set; }

        /// <summary>
        /// Tài khoản ngân hàng.
        /// </summary>
        public string? BankAccount { get; set; }

        /// <summary>
        /// Tên ngân hàng.
        /// </summary>
        public string? BankName { get; set; }

        /// <summary>
        /// Chi nhánh ngân hàng.
        /// </summary>
        public string? BankBranch { get; set; }

        #endregion
    }
}
