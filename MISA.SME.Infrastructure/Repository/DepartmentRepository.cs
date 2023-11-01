using Dapper;
using MISA.SME.Domain;
using System.Data;

namespace MISA.SME.Infrastructure
{
    /// <summary>
    /// Lớp thực hiện các thao tác cơ sở dữ liệu liên quan đến đơn vị (Department)
    /// </summary>
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        #region Constructors

        /// <summary>
        /// Khởi tạo một instance mới của <see cref="DepartmentRepository"/>
        /// </summary>
        /// <param name="transaction">Giao dịch cơ sở dữ liệu</param>
        public DepartmentRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Lấy danh sách đơn vị dựa trên từ khóa tìm kiếm
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <returns>Danh sách đơn vị phù hợp với từ khóa tìm kiếm</returns>
        /// <remarks>Created by: ttanh (18/09/2023)</remarks>
        public async Task<List<DepartmentDto>> GetFilteringAsync(string keyword)
        {
            string storeProcedureName = "Proc_Department_GetFiltering";

            var parameters = new DynamicParameters();
            parameters.Add("@Keyword", keyword);

            var result = await Connection.QueryAsync<DepartmentDto>(storeProcedureName, parameters, Transaction, commandType: CommandType.StoredProcedure)
                ?? throw new NotFoundException();

            return result.ToList();
        }

        #endregion
    }
}
