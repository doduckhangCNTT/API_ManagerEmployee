using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Practice.DL.Entity
{
    public class Department : BaseEntity
    {
        /// <summary>
        /// - Mã phòng ban
        /// </summary>
        /// Created By: DDKhang (24/5/2023)
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// - Tên phòng ban
        /// </summary>
        /// Created By: DDKhang (24/5/2023)
        public string? DepartmentName { get; set; }
    }
}
