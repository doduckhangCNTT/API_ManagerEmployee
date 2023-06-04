using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Practice.DL.Entity
{
    /// <summary>
    /// - Lớp ngân hàng
    /// </summary>
    /// CreatedBy: DDKhang (3/6/2023)
    public class Bank : BaseEntity
    {
        /// <summary>
        /// - Mã ngân hàng
        /// </summary>
        /// CreatedBy: DDKhang (3/6/2023)
        public Guid BankId { get; set; }

        /// <summary>
        /// - Số tài khoản ngân hàng
        /// </summary>
        /// CreatedBy: DDKhang (3/6/2023)
        public string? AccountNumber { get; set; }

        /// <summary>
        /// - Tên ngân hàng
        /// </summary>
        /// CreatedBy: DDKhang (3/6/2023)
        public string? BankName { get; set; }

        /// <summary>
        /// - Chi nhánh ngân hàng
        /// </summary>
        /// CreatedBy: DDKhang (3/6/2023)
        public string? Branch { get; set; }
    }
}
