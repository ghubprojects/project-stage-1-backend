using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using MISA.SME.Domain;

namespace MISA.SME.Application
{
    public class EmployeeServiceQuery : IEmployeeServiceQuery
    {
        #region Field

        private readonly IWebHostEnvironment _env;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public EmployeeServiceQuery(IWebHostEnvironment env, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _env = env;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #endregion

        #region Method

        /// <summary>
        /// Lấy dữ liệu tất cả nhân viên
        /// </summary>
        /// <returns>Danh sách tất cả nhân viên</returns>
        /// Created by: ttanh (19/09/2023)
        public async Task<List<EmployeeDto>> GetAllAsync()
        {
            var employeeList = await _unitOfWork.EmployeeRepository.GetAllAsync();

            if (employeeList == null)
                throw new NotFoundException("Không tìm thấy danh sách nhân viên");

            var employeeDtoList = _mapper.Map<List<EmployeeDto>>(employeeList);

            _unitOfWork.Commit();

            return employeeDtoList;
        }

        /// <summary>
        /// Lấy dữ liệu nhân viên phân trang
        /// </summary>
        /// <param name="limit">Số bản ghi trả về</param>
        /// <param name="offset">Hàng bắt đầu truy xuất</param>
        /// <returns>Danh sách nhân viên phân trang</returns>
        /// Created by: ttanh (19/09/2023)
        public async Task<List<EmployeeDto>> GetPaginationAsync(int limit = 20, int offset = 0)
        {
            var employeeDtoList = await _unitOfWork.EmployeeRepository.GetPaginationAsync(limit, offset);

            if (employeeDtoList == null)
                throw new NotFoundException("Không tìm thấy danh sách nhân viên");

            _unitOfWork.Commit();

            return employeeDtoList;
        }

        /// <summary>
        /// Danh sách nhân viên lọc theo mã nhân viên hoặc tên nhân viên
        /// </summary>
        /// <param name="keyword">Mã nhân viên hoặc tên nhân viên tìm kiếm</param>
        /// <returns>Danh sách nhân viên lọc theo từ khóa</returns>
        /// Created by: ttanh (19/09/2023)
        public async Task<List<EmployeeDto>> GetFilteringAsync(string keyword = "")
        {
            var employeeDtoList = await _unitOfWork.EmployeeRepository.GetFilteringAsync(keyword);

            if (employeeDtoList == null)
                throw new NotFoundException("Không tìm thấy danh sách nhân viên");

            _unitOfWork.Commit();

            return employeeDtoList;
        }

        /// <summary>
        /// Lấy dữ liệu nhân viên phân trang và lọc theo mã nhân viên, tên nhân viên
        /// </summary>
        /// <param name="keyword">Mã nhân viên hoặc tên nhân viên tìm kiếm</param>
        /// <param name="limit">Số bản ghi trả về</param>
        /// <param name="offset">Hàng bắt đầu truy xuất</param>
        /// <returns>Danh sách nhân viên lọc và phân trang</returns>
        /// Created by: ttanh (19/09/2023)
        public async Task<List<EmployeeDto>> GetFilteringAndPaginationAsync(string keyword = "", int limit = 20, int offset = 0)
        {
            var employeeDtoList = await _unitOfWork.EmployeeRepository.GetFilteringAndPaginationAsync(keyword, limit, offset);

            if (employeeDtoList == null)
                throw new NotFoundException("Không tìm thấy danh sách nhân viên");

            _unitOfWork.Commit();

            return employeeDtoList;
        }

        /// <summary>
        /// Lấy dữ liệu nhân viên theo ID
        /// </summary>
        /// <param name="id">Mã nhân viên</param>
        /// <returns>Dữ liệu nhân viên</returns>
        /// Created by: ttanh (19/09/2023)
        public async Task<EmployeeDto> GetByIdAsync(Guid id)
        {
            var employee = await _unitOfWork.EmployeeRepository.GetByIdAsync(id);

            if (employee == null)
                throw new NotFoundException("Không tìm thấy nhân viên");

            var employeeDto = _mapper.Map<EmployeeDto>(employee);

            _unitOfWork.Commit();

            return employeeDto;
        }

        /// <summary>
        /// Lấy dữ liệu nhân viên theo mã nhân viên
        /// </summary>
        /// <param name="code">Mã nhân viên</param>
        /// <returns>Dữ liệu nhân viên</returns>
        /// Created by: ttanh (10/09/2023)
        public async Task<EmployeeDto> GetByCodeAsync(string code)
        {
            var employeeDto = await _unitOfWork.EmployeeRepository.GetByCodeAsync(code);

            if (employeeDto == null)
                throw new NotFoundException($"Không tìm thấy nhân viên {code}");

            _unitOfWork.Commit();

            return employeeDto;
        }

        /// <summary>
        /// Xuất khẩu danh sách nhân viên
        /// </summary>
        /// <returns>Dữ liệu file excel</returns>
        /// Created by: ttanh (02/10/2023)
        public async Task<byte[]> ExportToExcelAsync(string? keyword)
        {
            // Lấy dữ liệu nhân viên từ database
            var employeeExportDto = new List<EmployeeExportDto>();
            if (keyword == null)
            {
                var employeeList = await _unitOfWork.EmployeeRepository.GetAllAsync();
                if (employeeList == null)
                    throw new NotFoundException("Không tìm thấy danh sách nhân viên");

                employeeExportDto = _mapper.Map<List<EmployeeExportDto>>(employeeList);
            }
            else
            {
                var employeeDtoList = await _unitOfWork.EmployeeRepository.GetFilteringAsync(keyword);
                if (employeeDtoList == null)
                    throw new NotFoundException("Không tìm thấy danh sách nhân viên");

                employeeExportDto = _mapper.Map<List<EmployeeExportDto>>(employeeDtoList);
            }

            _unitOfWork.Commit();

            // Define đường dẫn tới file excel mẫu
            var templateFileInfo = new FileInfo(Path.Combine(_env.ContentRootPath, "Template", "Danh_sach_nhan_vien.xlsx"));

            // Gọi đến helper để lấy dữ liệu nhân viên cho file excel
            var excelData = ExportToExcelHelper.GenerateExcelFile(employeeExportDto, templateFileInfo);

            return excelData;
        }

        #endregion
    }
}
