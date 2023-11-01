namespace MISA.SME.Domain
{
    public class DepartmentDto
    {
        #region Property

        /// <summary>
        /// ID đơn vị
        /// </summary>
        public Guid DepartmentID { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string DepartmentName { get; set; } = string.Empty;

        #endregion
    }
}
