namespace MISA.SME.Domain
{
    /// <summary>
    /// Thực thể cơ sở ghi nhận thông tin
    /// </summary>
    /// Created by: ttanh (05/09/2023)
    public abstract class AuditableBaseEntity
    {
        #region Property

        /// <summary>
        /// Thời gian khởi tạo
        /// </summary>
        public DateTimeOffset? CreatedDate { get; set; }

        /// <summary>
        /// Người khởi tạo
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        ///  Thời gian chỉnh sửa
        /// </summary>
        public DateTimeOffset? ModifiedDate { get; set; }

        /// <summary>
        ///  Người chỉnh sửa
        /// </summary>
        public string ModifiedBy { get; set; } = string.Empty;

        #endregion
    }
}
