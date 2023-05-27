using Microsoft.Extensions.Configuration;
using MISA.WebFresher032023.Practice.DL.Entity;
using MISA.WebFresher032023.Practice.DL.Repository.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Practice.DL.Repository.Departments
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
