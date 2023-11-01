using Dapper;
using MISA.SME.Domain;
using System.Data;

namespace MISA.SME.Infrastructure
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        #region Property

        protected IDbTransaction Transaction { get; private set; }
        protected IDbConnection Connection { get { return Transaction.Connection; } }
        public virtual Type EntityType { get; set; } = typeof(TEntity);
        public virtual string TableName { get; set; } = typeof(TEntity).Name;

        #endregion

        #region Constructor

        public BaseRepository(IDbTransaction transaction)
        {
            Transaction = transaction;
        }

        #endregion

        #region Method

        /// <summary>
        /// Lấy danh sách tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// Created by: ttanh (07/09/2023)
        /// Modified by: ttanh (09/09/2023)
        public async Task<List<TEntity>> GetAllAsync()
        {
            string storeProcudureName = $"Proc_{TableName}_GetAll";

            var parameters = new DynamicParameters();

            var result = await Connection.QueryAsync<TEntity>(storeProcudureName, parameters, Transaction, commandType: CommandType.StoredProcedure);

            return result.ToList();
        }

        /// <summary>
        /// Lấy dữ liệu bản ghi theo ID
        /// </summary>
        /// <param name="id">ID bản ghi</param>
        /// <returns>Dữ liệu bản ghi</returns>
        /// Created by: ttanh (09/09/2023)
        /// Modified by: ttanh (10/09/2023)
        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            string storeProcudureName = $"Proc_{TableName}_GetByID";

            var parameters = new DynamicParameters();
            parameters.Add($"@{TableName}ID", id);

            var result = await Connection.QueryFirstOrDefaultAsync<TEntity>(storeProcudureName, parameters, Transaction, commandType: CommandType.StoredProcedure)
                ?? throw new NotFoundException();

            return result;
        }

        /// <summary>
        /// Thêm bản ghi
        /// </summary>
        /// <param name="entity">Dữ liệu bản ghi thêm vào</param>
        /// <returns>Số hàng được thêm</returns>
        /// Created by: ttanh (10/09/2023)
        /// Modified by: ttanh (11/09/2023)
        public async Task<int> AddAsync(TEntity entity)
        {
            string storeProcudureName = $"Proc_{TableName}_InsertOne";

            var parameters = new DynamicParameters();

            var props = EntityType.GetProperties();

            foreach (var prop in props)
            {
                parameters.Add($"@{prop.Name}", prop.GetValue(entity));
            }

            var affectedRows = await Connection.ExecuteAsync(storeProcudureName, parameters, Transaction, commandType: CommandType.StoredProcedure);

            return affectedRows;
        }

        /// <summary>
        /// Cập nhật thông tin bản ghi
        /// </summary>
        /// <param name="entity">Dữ liệu bản ghi được cập nhật</param>
        /// <returns>Số hàng được cập nhật</returns>
        /// Created by: ttanh (11/09/2023)
        public async Task<int> UpdateAsync(TEntity entity)
        {
            string storeProcudureName = $"Proc_{TableName}_UpdateOne";

            var parameters = new DynamicParameters();

            var props = EntityType.GetProperties();

            foreach (var prop in props)
            {
                parameters.Add($"@{prop.Name}", prop.GetValue(entity));
            }

            var affectedRows = await Connection.ExecuteAsync(storeProcudureName, parameters, Transaction, commandType: CommandType.StoredProcedure);

            return affectedRows;
        }

        /// <summary>
        /// Xóa 1 bản ghi
        /// </summary>
        /// <param name="id">ID bản ghi bị xóa</param>
        /// <returns>Số hàng bị xóa</returns>
        /// Created by: ttanh (10/09/2023)
        /// Modified by: ttanh (11/09/2023)
        public async Task<int> DeleteAsync(Guid id)
        {
            string storeProcudureName = $"Proc_{TableName}_DeleteByID";

            var parameters = new DynamicParameters();
            parameters.Add($"@{TableName}ID", id);

            var affectedRows = await Connection.ExecuteAsync(storeProcudureName, parameters, Transaction, commandType: CommandType.StoredProcedure);

            return affectedRows;
        }

        /// <summary>
        /// Xóa nhiều bản ghi
        /// </summary>
        /// <param name="ids">Danh sách ID bản ghi bị xóa</param>
        /// <returns>Số hàng bị xóa</returns>
        /// Created by: ttanh (19/09/2023)
        public async Task<int> DeleteMultipleAsync(List<Guid> ids)
        {
            string sql = $"DELETE FROM {TableName.ToLower()} WHERE {TableName}ID IN @ids";

            var parameters = new DynamicParameters();
            parameters.Add("ids", ids);

            var affectedRows = await Connection.ExecuteAsync(sql, parameters, Transaction);

            return affectedRows;
        }

        public Task<int> AddMultipleAsync(List<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateMultipleAsync(List<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
