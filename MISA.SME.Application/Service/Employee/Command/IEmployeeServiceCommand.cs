using MISA.SME.Domain;

namespace MISA.SME.Application
{
    public interface IEmployeeServiceCommand : IBaseServiceCommand<EmployeeDto, EmployeeCreateDto, EmployeeUpdateDto>
    {
    }
}
