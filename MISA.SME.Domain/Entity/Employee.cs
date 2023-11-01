namespace MISA.SME.Domain
{
    /// <summary>
    /// Thông tin nhân viên
    /// </summary>
    public class Employee : AuditableBaseEntity, IEntity
    {
        #region Property

        /// <summary>
        /// ID nhân viên
        /// </summary>
        public Guid EmployeeID { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string EmployeeCode { get; set; } = string.Empty;

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTimeOffset? DateOfBirth { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public Gender Gender { get; set; }
        /// <summary>
        /// Số căn cước công dân
        /// </summary>
        public string IdentityNumber { get; set; } = string.Empty;

        /// <summary>
        /// Ngày cấp căn cước công dân
        /// </summary>
        public DateTimeOffset? IdentityIssuedDate { get; set; }

        /// <summary>
        /// Nơi cấp căn cước công dân
        /// </summary>
        public string IdentityIssuedPlace { get; set; } = string.Empty;

        /// <summary>
        /// Chức danh
        /// </summary>
        public string PositionName { get; set; } = string.Empty;

        /// <summary>
        /// ID đơn vị
        /// </summary>
        public Guid DepartmentID { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string DepartmentName { get; set; } = string.Empty;

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Số điện thoại di động.
        /// </summary>
        public string MobilePhone { get; set; } = string.Empty;

        /// <summary>
        /// Số điện thoại cố định.
        /// </summary>
        public string LandlinePhone { get; set; } = string.Empty;

        /// <summary>
        /// Tài khoản ngân hàng.
        /// </summary>
        public string BankAccount { get; set; } = string.Empty;

        /// <summary>
        /// Tên ngân hàng.
        /// </summary>
        public string BankName { get; set; } = string.Empty;

        /// <summary>
        /// Chi nhánh ngân hàng.
        /// </summary>
        public string BankBranch { get; set; } = string.Empty;

        /// <summary>
        /// Lấy giá trị ID của nhân viên
        /// </summary>
        /// <returns>Giá trị ID của nhân viên</returns>
        public Guid GetId()
        {
            return EmployeeID;
        }

        /// <summary>
        /// Thiết lập giá trị ID cho nhân viên
        /// </summary>
        /// <param name="id">Giá trị ID mới</param>
        public void SetId(Guid id)
        {
            EmployeeID = id;
        }

        #endregion
    }
}
