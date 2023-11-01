using Microsoft.Extensions.DependencyInjection;

namespace MISA.SME.Domain
{
    /// <summary>
    /// Lớp định nghĩa Dependency Injection cho domain layer
    /// </summary>
    public static class DependencyInjection
    {
        #region Methods

        /// <summary>
        /// Thêm các dịch vụ của domain layer vào IServiceCollection
        /// </summary>
        /// <param name="services">Danh sách dịch vụ</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            #region Validators

            services.AddScoped<IEmployeeValidator, EmployeeValidator>();

            #endregion

            return services;
        }

        #endregion
    }
}
