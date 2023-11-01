using Dapper;
using MISA.SME.Domain;
using System.Data;

namespace MISA.SME.Infrastructure
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        #region Method

        /// <summary>
        /// Lấy dữ liệu nhân viên theo mã nhân viên
        /// </summary>
        /// <param name="code">Mã nhân viên</param>
        /// <returns>Dữ liệu nhân viên</returns>
        /// <exception cref="NotFoundException">Không tim thấy tài nguyên</exception>
        /// Created by: ttanh (11/09/2023)
        /// Modified by: ttanh (13/09/2023)
        public async Task<EmployeeDto> GetByCodeAsync(string code)
        {
            string storeProcudureName = $"Proc_Employee_GetByCode";

            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeCode", code);

            var result = await Connection.QueryFirstOrDefaultAsync<EmployeeDto>(storeProcudureName, parameters, Transaction, commandType: CommandType.StoredProcedure);

            return result;
        }

        /// <summary>
        /// Lấy dữ liệu nhân viên phân trang
        /// </summary>
        /// <param name="limit">Số bản ghi trả về</param>
        /// <param name="offset">Hàng bắt đầu truy xuất</param>
        /// <returns>Danh sách nhân viên phân trang</returns>
        /// <exception cref="NotFoundException">Không tim thấy tài nguyên</exception>
        /// Created by: ttanh (10/09/2023)
        /// Modified by: ttanh (19/09/2023)
        public async Task<List<EmployeeDto>> GetPaginationAsync(int limit, int offset)
        {
            string storeProcudureName = "Proc_Employee_GetPagination";

            var parameters = new DynamicParameters();
            parameters.Add("@Limit", limit);
            parameters.Add("@Offset", offset);

            var result = await Connection.QueryAsync<EmployeeDto>(storeProcudureName, parameters, Transaction, commandType: CommandType.StoredProcedure)
                ?? throw new NotFoundException();

            return result.ToList();
        }

        /// <summary>
        /// Lấy dữ liệu nhân viên lọc theo mã nhân viên hoặc tên nhân viên
        /// </summary>
        /// <param name="keyword">Mã nhân viên hoặc tên nhân viên tìm kiếm</param>
        /// <returns>Danh sách nhân viên lọc theo mã nhân viên hoặc tên nhân viên</returns>
        /// <exception cref="NotFoundException">Không tim thấy tài nguyên</exception>
        /// Created by: ttanh (20/09/2023)
        public async Task<List<EmployeeDto>> GetFilteringAsync(string keyword)
        {
            string storeProcudureName = "Proc_Employee_GetFiltering";

            var parameters = new DynamicParameters();
            parameters.Add("@Keyword", keyword);

            var result = await Connection.QueryAsync<EmployeeDto>(storeProcudureName, parameters, Transaction, commandType: CommandType.StoredProcedure)
                ?? throw new NotFoundException();

            return result.ToList();
        }

        /// <summary>
        /// Lấy dữ liệu nhân viên phân trang lọc theo mã nhân viên hoặc tên nhân viên
        /// </summary>
        /// <param name="keyword">Mã nhân viên hoặc tên nhân viên tìm kiếm</param>
        /// <param name="limit">Số bản ghi trả về</param>
        /// <param name="offset">Hàng bắt đầu truy xuất</param>
        /// <returns>Danh sách nhân viên lọc và phân trang</returns>
        /// <exception cref="NotFoundException">Không tim thấy tài nguyên</exception>
        /// Created by: ttanh (10/09/2023)
        /// Modified by: ttanh (19/09/2023)
        public async Task<List<EmployeeDto>> GetFilteringAndPaginationAsync(string keyword, int limit, int offset)
        {
            string storeProcudureName = "Proc_Employee_GetFilteringAndPagination";

            var parameters = new DynamicParameters();
            parameters.Add("@Keyword", keyword);
            parameters.Add("@Limit", limit);
            parameters.Add("@Offset", offset);

            var result = await Connection.QueryAsync<EmployeeDto>(storeProcudureName, parameters, Transaction, commandType: CommandType.StoredProcedure)
                ?? throw new NotFoundException();

            return result.ToList();
        }

        #endregion
    }
}
