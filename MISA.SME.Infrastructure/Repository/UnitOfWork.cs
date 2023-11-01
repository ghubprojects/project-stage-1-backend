using MISA.SME.Domain;
using System.Data;

namespace MISA.SME.Infrastructure
{
    /// <summary>
    /// Lớp đại diện cho Unit of Work, quản lý các repository và giao dịch cơ sở dữ liệu
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        #region Fields

        private IDbConnection _connection;
        private IDbTransaction _transaction;

        private IEmployeeRepository _employeeRepository;
        private IDepartmentRepository _departmentRepository;

        #endregion

        #region Constructors

        /// <summary>
        /// Khởi tạo một instance mới của <see cref="UnitOfWork"/>
        /// </summary>
        /// <param name="connection">Đối tượng kết nối cơ sở dữ liệu</param>
        public UnitOfWork(IDbConnection connection)
        {
            _connection = connection;
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Repository đối tượng nhân viên
        /// </summary>
        public IEmployeeRepository EmployeeRepository =>
            _employeeRepository ??= new EmployeeRepository(_transaction);

        /// <summary>
        /// Repository đối tượng đơn vị
        /// </summary>
        public IDepartmentRepository DepartmentRepository =>
            _departmentRepository ??= new DepartmentRepository(_transaction);

        #endregion

        #region Methods

        /// <summary>
        /// Lưu các thay đổi vào cơ sở dữ liệu
        /// </summary>
        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                ResetRepositories();
            }
        }

        /// <summary>
        /// Đặt lại các repository
        /// </summary>
        private void ResetRepositories()
        {
            _employeeRepository = null;
            _departmentRepository = null;
        }

        /// <summary>
        /// Giải phóng tài nguyên
        /// </summary>
        public void Dispose()
        {
            _transaction?.Dispose();
            _connection?.Dispose();
        }

        #endregion
    }
}
