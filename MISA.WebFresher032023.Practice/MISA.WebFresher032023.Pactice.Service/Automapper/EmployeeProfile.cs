using AutoMapper;
using MISA.WebFresher032023.Pactice.BL.DTO.Emoloyees;
using MISA.WebFresher032023.Practice.DL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Pactice.BL.Automapper
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            /*
             - Thực hiện ánh xạ các thuộc tính từ Employee thành các trường tương ứng trong EmployeeDTO (những trường ko được 
            khai báo trong EmployeeDTO thì không được ánh xạ vào)
             */
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>();
            CreateMap<FilterEntity<Employee>, FilterEntity<EmployeeDto>>();
            CreateMap<EmployeeCreateDto, Employee>();
            CreateMap<EmployeeUpdateDto, Employee>();
        }
    }
}
