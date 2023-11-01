namespace MISA.SME.Domain
{
    public class BaseDto
    {
        #region Property

        /// <summary>
        /// Thời gian khởi tạo
        /// </summary>
        public DateTimeOffset CreatedDate { get; set; }

        /// <summary>
        /// Người khởi tạo
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        ///  Thời gian chỉnh sửa
        /// </summary>
        public DateTimeOffset ModifiedDate { get; set; }

        /// <summary>
        ///  Người chỉnh sửa
        /// </summary>
        public string ModifiedBy { get; set; } = string.Empty;

        #endregion
    }
}
