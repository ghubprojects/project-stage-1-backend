namespace MISA.SME.Domain
{
    /// <summary>
    /// Lớp thực hiện các kiểm tra liên quan đến nhân viên
    /// </summary>
    public class EmployeeValidator : IEmployeeValidator
    {
        #region Fields

        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Constructors

        /// <summary>
        /// Khởi tạo một instance mới của <see cref="EmployeeValidator"/>
        /// </summary>
        /// <param name="unitOfWork">Đối tượng Unit of Work</param>
        public EmployeeValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Kiểm tra sự tồn tại của mã nhân viên trong hệ thống
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên cần kiểm tra</param>
        /// <returns>Task</returns>
        /// <remarks>Created by: ttanh (24/09/2023)</remarks>
        public async Task CheckExistEmployeeCodeAsync(string employeeCode)
        {
            var searchResult = await _unitOfWork.EmployeeRepository.GetByCodeAsync(employeeCode);

            if (searchResult != null)
                throw new ConflictException($"Mã nhân viên <{employeeCode}> đã tồn tại trong hệ thống, vui lòng kiểm tra lại.");

            _unitOfWork.Commit();
        }

        #endregion
    }
}
