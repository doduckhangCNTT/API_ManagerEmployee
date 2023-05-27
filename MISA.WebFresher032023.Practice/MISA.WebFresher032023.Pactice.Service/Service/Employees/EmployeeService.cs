using AutoMapper;
using MISA.WebFresher032023.Pactice.BL.DTO.Emoloyees;
using MISA.WebFresher032023.Pactice.BL.Service.Bases;
using MISA.WebFresher032023.Practice.Common.Enum;
using MISA.WebFresher032023.Practice.Common.Exception;
using MISA.WebFresher032023.Practice.Common.Resources;
using MISA.WebFresher032023.Practice.DL.Entity;
using MISA.WebFresher032023.Practice.DL.Repository.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Pactice.BL.Service.Employees
{
    /*
     - Kế thừa lớp Base (chứa những phương thức dùng chung), và những phương thức dùng riêng (IEmployeeService)
     - Thực hiện khai báo các kiểu cho lớp abstract Base (Employee, EmployeeDto...)
     */
    public class EmployeeService : BaseService<Employee, EmployeeDto, EmployeeUpdateDto>, IEmployeeService
    {
        #region Field
        // Khai báo kết nối đến DL
        //private readonly IEmployeeRepository _employeeRepository;
        //private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper) : base(employeeRepository, mapper)
        {
            //_employeeRepository = employeeRepository;
            //_mapper = mapper;
        }
        #endregion

        public Task<bool> CheckEmployeeCode(string employeeCode)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeDto> CreateAsync(EmployeeCreateDto employeeDTO)
        {
            throw new NotImplementedException();
        }

        //public async Task<EmployeeDto?> GetAsync(Guid employeeId)
        //{
        //    var employee = await _employeeRepository.GetAsync(employeeId);
        //    if (employee == null) return null;

        //    // Cach 1
        //    /*
        //        - Tự động ánh xác các thuộc tính từ employee -> employeeDTO
        //     */
        //    var employeeDto = _mapper.Map<EmployeeDto>(employee);
        //    return employeeDto;

        //    // Cach 2
        //    //return new EmployeeDto
        //    //{
        //    //    EmployeeId = employee.EmployeeId,
        //    //    FullName = employee.FullName,
        //    //    EmployeeCode = employee.EmployeeCode,
        //    //    DateOfBirth = employee.DateOfBirth,
        //    //    Gender = employee.Gender,
        //    //    Email = employee.Email,
        //    //    PhoneNumber=employee.PhoneNumber,
        //    //    LandlinePhone=employee.LandlinePhone,
        //    //    IdentityNumber=employee.IdentityNumber,
        //    //    IdentityPlace=employee.IdentityPlace,
        //    //    CreatedDate=employee.CreatedDate,
        //    //    CreatedBy=employee.CreatedBy,
        //    //    DepartmentId=employee.DepartmentId,
        //    //    BankId=employee.BankId
        //    //};
        //}

        //public override async Task<EmployeeDto> UpdateAsync(Guid employeeId, EmployeeUpdateDto employeeUpdateDTO)
        //{
        //    // validate employeeUpdateDto
        //    var employee = await _baseRepository.GetAsync(employeeId);

        //    if (employee == null) throw new NotImplementedException(ResourceVN.Validate_EmployeeNotFound);
        //    var newEmployee = _mapper.Map<Employee>(employeeUpdateDTO);
        //    var employeeUpdated = await _baseRepository.UpdateAsync(employeeId, newEmployee);
        //    return _mapper.Map<EmployeeDto>(employee);
        //}

        public override async Task DeleteTaskAsync(Guid id)
        {
            // Kiểm tra thực thể có tồn tại
            var entity = await _baseRepository.GetAsync(id);

            if (entity == null)
            {
                // Bắt lỗi thông qua Middleware
                throw new NotFoundException("Khong tim thay nhung baor server loi");
            }

            if (entity.Gender == (int?)GenderEnum.Female)
            {
                throw new Exception("Không xóa phụ nữ");
            }

            // Thực hiện xóa
            await _baseRepository.DeleteAsync(id);
        }
    }
}
