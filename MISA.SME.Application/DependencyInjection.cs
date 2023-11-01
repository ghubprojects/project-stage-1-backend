using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MISA.SME.Application
{
    /// <summary>
    /// Đối tượng cung cấp các dịch vụ cấu hình và đăng ký dịch vụ trong ứng dụng.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Phương thức mở rộng để đăng ký các dịch vụ trong ứng dụng.
        /// </summary>
        /// <param name="services">Dịch vụ cung cấp bởi ASP.NET Core.</param>
        /// <returns>IServiceCollection sau khi đã đăng ký các dịch vụ.</returns>
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Cấu hình AutoMapper để ánh xạ các đối tượng.
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Đăng ký các validators được sử dụng bởi FluentValidation.
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Đăng ký các dịch vụ MediatR để xử lý các yêu cầu và trả về kết quả.
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            // Đăng ký một số dịch vụ sẽ được sử dụng trong ứng dụng.
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            #region Services

            // Đăng ký các dịch vụ liên quan đến Nhân viên (Employee).
            services.AddScoped<IEmployeeServiceCommand, EmployeeServiceCommand>();
            services.AddScoped<IEmployeeServiceQuery, EmployeeServiceQuery>();

            // Đăng ký các dịch vụ liên quan đến Phòng ban (Department).
            services.AddScoped<IDepartmentServiceQuery, DepartmentServiceQuery>();

            #endregion

            return services;
        }
    }
}
