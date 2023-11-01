using AutoMapper;
using MISA.SME.Domain;

namespace MISA.SME.Application
{
    /// <summary>
    /// Dịch vụ truy vấn đơn vị
    /// </summary>
    public class DepartmentServiceQuery : IDepartmentServiceQuery
    {
        #region Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        #endregion

        #region Constructors

        public DepartmentServiceQuery(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Lấy danh sách tất cả đơn vị
        /// </summary>
        /// <returns>Danh sách đơn vị</returns>
        /// <remarks>Created by: ttanh (28/09/2023)</remarks>
        public async Task<List<DepartmentDto>> GetAllAsync()
        {
            var departmentList = await _unitOfWork.DepartmentRepository.GetAllAsync();

            if (departmentList == null)
                throw new NotFoundException("Không tìm thấy danh sách đơn vị");

            var departmentDtoList = _mapper.Map<List<DepartmentDto>>(departmentList);

            _unitOfWork.Commit();

            return departmentDtoList;
        }

        /// <summary>
        /// Lấy đơn vị theo ID
        /// </summary>
        /// <param name="id">ID của đơn vị cần lấy</param>
        /// <returns>Đơn vị theo ID</returns>
        /// <remarks>Created by: ttanh (28/09/2023)</remarks>
        public async Task<DepartmentDto> GetByIdAsync(Guid id)
        {
            var department = await _unitOfWork.DepartmentRepository.GetByIdAsync(id);

            if (department == null)
                throw new NotFoundException("Không tìm thấy đơn vị");

            var departmentDto = _mapper.Map<DepartmentDto>(department);

            _unitOfWork.Commit();

            return departmentDto;
        }

        /// <summary>
        /// Lấy danh sách đơn vị sau khi áp dụng bộ lọc
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <returns>Danh sách đơn vị sau khi áp dụng bộ lọc</returns>
        /// <remarks>Created by: ttanh (28/09/2023)</remarks>
        public async Task<List<DepartmentDto>> GetFilteringAsync(string keyword)
        {
            var departmentDtoList = await _unitOfWork.DepartmentRepository.GetFilteringAsync(keyword);

            if (departmentDtoList == null)
                throw new NotFoundException("Không tìm thấy danh sách đơn vị");

            _unitOfWork.Commit();

            return departmentDtoList;
        }

        #endregion

        #region Not Implemented Methods

        public Task<List<DepartmentDto>> GetFilteringAndPaginationAsync(string keyword, int limit, int offset)
        {
            throw new NotImplementedException();
        }

        public Task<List<DepartmentDto>> GetPaginationAsync(int limit, int offset)
        {
            throw new NotImplementedException();
        }

        public Task<DepartmentDto> GetByCodeAsync(string code)
        {
            throw new NotImplementedException();
        }

        public Task<byte[]> ExportToExcelAsync(string? keyword)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
