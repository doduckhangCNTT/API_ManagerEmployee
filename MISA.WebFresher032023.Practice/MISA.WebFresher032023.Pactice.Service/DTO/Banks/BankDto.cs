using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Pactice.BL.DTO.Banks
{
    /// <summary>
    /// - Lớp chứa các thuộc tính sử dụng để truyền dữ liệu
    /// </summary>
    /// CreatedBy: DDKhang (3/6/2023)
    public class BankDto
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
        /// <summary>
        /// - Ngày tạo
        /// </summary>
        /// Created By: DDKhang (3/6/2023)
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// - Người tạo
        /// </summary>
        /// Created By: DDKhang (3/6/2023)
        public string? CreatedBy { get; set; }

        /// <summary>
        /// - Ngày chỉnh sửa
        /// </summary>
        /// Created By: DDKhang (3/6/2023)
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// - Người chỉnh sửa
        /// </summary>
        /// Created By: DDKhang (3/6/2023)
        public string? ModifiedBy { get; set; }
    }
}
