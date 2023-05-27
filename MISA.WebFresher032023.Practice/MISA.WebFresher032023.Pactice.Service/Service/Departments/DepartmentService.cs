using AutoMapper;
using MISA.WebFresher032023.Pactice.BL.DTO.Departments;
using MISA.WebFresher032023.Pactice.BL.Service.Bases;
using MISA.WebFresher032023.Practice.DL.Entity;
using MISA.WebFresher032023.Practice.DL.Repository.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Pactice.BL.Service.Departments
{
    public class DepartmentService : BaseService<Department, DepartmentDTO, DepartmentUpdateDTO>, IDepartmentService
    {
        #region Constructor
        public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper) : base(departmentRepository, mapper)
        {
        }
        #endregion
    }
}
