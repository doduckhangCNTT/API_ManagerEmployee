using MISA.WebFresher032023.Pactice.BL.DTO.Emoloyees;
using MISA.WebFresher032023.Pactice.BL.Service.Bases;
using MISA.WebFresher032023.Practice.DL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Pactice.BL.Service.Employees
{
    public interface IEmployeeService : IBaseService<EmployeeDto, EmployeeUpdateDto, EmployeeCreateDto>
    {
        Task<bool> CheckEmployeeCode(string employeeCode);

        //Task<EmployeeDTO> GetAsync(Guid employeeId);
        //Task<EmployeeDTO> CreateAsync(EmployeeCreateDTO employeeDTO);
        //Task<EmployeeDTO> UpdateAsync(Guid employeeId, EmployeeUpdateDto e);
    }
}
