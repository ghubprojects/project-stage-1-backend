using AutoMapper;
using MISA.SME.Domain;

namespace MISA.SME.Application
{
    /// <summary>
    /// Dịch vụ thực hiện các tác vụ liên quan đến nhân viên (Command)
    /// </summary>
    public class EmployeeServiceCommand : IEmployeeServiceCommand
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmployeeValidator _employeeValidator;

        public EmployeeServiceCommand(IUnitOfWork unitOfWork, IMapper mapper, IEmployeeValidator employeeValidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _employeeValidator = employeeValidator;
        }

        #region AddAsync

        /// <summary>
        /// Thêm mới thông tin nhân viên
        /// </summary>
        /// <param name="createEmployeeDto">Thông tin nhân viên cần thêm</param>
        /// <returns>Số bản ghi bị ảnh hưởng sau khi thêm mới</returns>
        public async Task<int> AddAsync(EmployeeCreateDto createEmployeeDto)
        {
            // Validate dữ liệu theo nghiệp vụ
            await _employeeValidator.CheckExistEmployeeCodeAsync(createEmployeeDto.EmployeeCode);

            // Mapping dữ liệu
            var entity = _mapper.Map<Employee>(createEmployeeDto);

            if (entity.GetId() == Guid.Empty)
            {
                entity.SetId(Guid.NewGuid());
            }

            if (entity is AuditableBaseEntity)
            {
                entity.CreatedBy = "Tăng Thế Anh";
                entity.CreatedDate = DateTimeOffset.UtcNow;
                entity.ModifiedBy = "Tăng Thế Anh";
                entity.ModifiedDate = DateTimeOffset.UtcNow;
            }

            // Thêm dữ liệu vào cơ sở dữ liệu
            var affectedRows = await _unitOfWork.EmployeeRepository.AddAsync(entity);

            _unitOfWork.Commit();

            return affectedRows;
        }

        #endregion

        #region UpdateAsync

        /// <summary>
        /// Cập nhật thông tin nhân viên
        /// </summary>
        /// <param name="updateEmployeeDto">Thông tin nhân viên cần cập nhật</param>
        /// <returns>Số bản ghi bị ảnh hưởng sau khi cập nhật</returns>
        public async Task<int> UpdateAsync(EmployeeUpdateDto updateEmployeeDto)
        {
            // Validate dữ liệu theo nghiệp vụ
            if (updateEmployeeDto.CurrentEmployeeCode != updateEmployeeDto.EmployeeCode)
            {
                await _employeeValidator.CheckExistEmployeeCodeAsync(updateEmployeeDto.EmployeeCode);
            }

            // Mapping dữ liệu
            var entity = _mapper.Map<Employee>(updateEmployeeDto);

            if (entity is AuditableBaseEntity)
            {
                entity.ModifiedBy = "Tăng Thế Anh";
                entity.ModifiedDate = DateTimeOffset.UtcNow;
            }

            // Cập nhật dữ liệu vào cơ sở dữ liệu
            var affectedRows = await _unitOfWork.EmployeeRepository.UpdateAsync(entity);

            _unitOfWork.Commit();

            return affectedRows;
        }

        #endregion

        #region DeleteAsync

        /// <summary>
        /// Xóa thông tin nhân viên theo ID
        /// </summary>
        /// <param name="id">ID của nhân viên cần xóa</param>
        /// <returns>Số bản ghi bị ảnh hưởng sau khi xóa</returns>
        public async Task<int> DeleteAsync(Guid id)
        {
            var searchResult = await _unitOfWork.EmployeeRepository.GetByIdAsync(id);

            if (searchResult == null)
            {
                throw new NotFoundException("Không thể xóa nhân viên, vui lòng kiểm tra lại.");
            }

            var affectedRows = await _unitOfWork.EmployeeRepository.DeleteAsync(id);

            _unitOfWork.Commit();

            return affectedRows;
        }

        #endregion

        #region DeleteMultipleAsync

        /// <summary>
        /// Xóa danh sách nhân viên theo danh sách ID
        /// </summary>
        /// <param name="ids">Danh sách ID của nhân viên cần xóa</param>
        /// <returns>Số bản ghi bị ảnh hưởng sau khi xóa</returns>
        public async Task<int> DeleteMultipleAsync(List<Guid> ids)
        {
            foreach (var id in ids)
            {
                var searchResult = await _unitOfWork.EmployeeRepository.GetByIdAsync(id);

                if (searchResult == null)
                {
                    throw new NotFoundException("Không thể xóa danh sách nhân viên, vui lòng kiểm tra lại.");
                }
            }

            var affectedRows = await _unitOfWork.EmployeeRepository.DeleteMultipleAsync(ids);

            _unitOfWork.Commit();

            return affectedRows;
        }

        #endregion
    }
}
