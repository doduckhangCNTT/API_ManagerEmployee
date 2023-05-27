using MISA.WebFresher032023.Pactice.BL.DTO.Departments;
using MISA.WebFresher032023.Pactice.BL.Service.Bases;
using MISA.WebFresher032023.Practice.DL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Pactice.BL.Service.Departments
{
    public interface IDepartmentService : IBaseService<Department, DepartmentDTO, DepartmentUpdateDTO>
    {
    }
}
