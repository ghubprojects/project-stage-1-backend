using AutoMapper;
using MISA.SME.Domain;

namespace MISA.SME.Application
{
    /// <summary>
    /// Lớp chứa cấu hình AutoMapper cho đối tượng Department
    /// </summary>
    public class DepartmentProfile : Profile
    {
        /// <summary>
        /// Hàm khởi tạo của DepartmentProfile để cấu hình mapping
        /// </summary>
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentDto>();
        }
    }
}
