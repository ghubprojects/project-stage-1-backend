namespace MISA.SME.Domain
{
    /// <summary>
    /// Đối tượng đơn vị trong hệ thống.
    /// </summary>
    public class Department : AuditableBaseEntity, IEntity
    {
        #region Properties

        /// <summary>
        /// ID đơn vị
        /// </summary>
        public Guid DepartmentID { get; set; }

        /// <summary>
        /// Mã đơn vị 
        /// </summary>
        public string DepartmentCode { get; set; } = string.Empty;

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string DepartmentName { get; set; } = string.Empty;

        #endregion

        #region Methods

        /// <summary>
        /// Lấy giá trị ID của đơn vị.
        /// </summary>
        /// <returns>Giá trị ID của đơn vị.</returns>
        public Guid GetId()
        {
            return DepartmentID;
        }

        /// <summary>
        /// Thiết lập giá trị ID cho đơn vị.
        /// </summary>
        /// <param name="id">Giá trị ID mới.</param>
        public void SetId(Guid id)
        {
            DepartmentID = id;
        }

        #endregion
    }
}
