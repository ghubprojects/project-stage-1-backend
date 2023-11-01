using Microsoft.Extensions.DependencyInjection;
using MISA.SME.Domain;

namespace MISA.SME.Infrastructure
{
    /// <summary>
    /// Lớp định nghĩa Dependency Injection cho infrastructure layer
    /// </summary>
    public static class DependencyInjection
    {
        #region Methods

        /// <summary>
        /// Thêm các dịch vụ của infrastructure layer vào IServiceCollection
        /// </summary>
        /// <param name="services">Danh sách dịch vụ</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        #endregion
    }
}
