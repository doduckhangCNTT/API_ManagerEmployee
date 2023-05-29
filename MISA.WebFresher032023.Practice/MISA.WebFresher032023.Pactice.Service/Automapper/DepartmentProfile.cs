using AutoMapper;
using MISA.WebFresher032023.Pactice.BL.DTO.Departments;
using MISA.WebFresher032023.Practice.DL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Pactice.BL.Automapper
{
    public class DepartmentProfile : Profile
    {
        /*
        - Thực hiện ánh xạ các thuộc tính từ Department thành các trường tương ứng trong DepartmentDTO (những trường ko được 
        khai báo trong EmployeeDTO thì không được ánh xạ vào)
        */
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentDTO>();
            CreateMap<DepartmentDTO, Department>();
            CreateMap<DepartmentCreateDTO, Department>();
            CreateMap<DepartmentUpdateDTO, Department>();
            CreateMap<FilterEntity<Department>, FilterEntity<DepartmentDTO>>();
        }
    }
}
