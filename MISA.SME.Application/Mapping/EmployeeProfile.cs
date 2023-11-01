using AutoMapper;
using MISA.SME.Domain;

namespace MISA.SME.Application
{
    /// <summary>
    /// Lớp chứa cấu hình AutoMapper cho đối tượng Employee
    /// </summary>
    public class EmployeeProfile : Profile
    {
        /// <summary>
        /// Hàm khởi tạo của EmployeeProfile để cấu hình mapping
        /// </summary>
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>();

            CreateMap<EmployeeCreateDto, Employee>();

            CreateMap<EmployeeUpdateDto, Employee>();

            CreateMap<Employee, EmployeeExportDto>()
                .ForMember(des => des.Gender, opt => opt.MapFrom(src => GenderHelper.ConvertToResource(src.Gender)))
                .ForMember(des => des.DateOfBirth, otp => otp.MapFrom(src => ConvertDateToString(src.DateOfBirth)));

            CreateMap<EmployeeDto, EmployeeExportDto>()
                .ForMember(des => des.Gender, opt => opt.MapFrom(src => GenderHelper.ConvertToResource(src.Gender)))
                .ForMember(des => des.DateOfBirth, otp => otp.MapFrom(src => ConvertDateToString(src.DateOfBirth)));
        }

        /// <summary>
        /// Chuyển đổi thời gian từ DateTimeOffset sang DateOnly để hiển thị trong file excel
        /// </summary>
        /// <param name="date">Đối tượng thời gian theo DateTimeOffset</param>
        /// <returns>Chuỗi ngày tháng năm</returns>
        /// <remarks>Created by: ttanh (02/10/2023)</remarks>
        private dynamic ConvertDateToString(DateTimeOffset? date)
        {
            if (!date.HasValue)
                return date;

            // Chuyển đổi DateTimeOffset sang DateOnly.
            DateOnly dateOnly = new DateOnly(date.Value.Year, date.Value.Month, date.Value.Day);

            return dateOnly.ToString();
        }
    }
}
