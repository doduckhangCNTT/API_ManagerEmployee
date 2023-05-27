using MISA.WebFresher032023.Practice.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Practice.DL.Entity
{
    public class Employee : BaseEntity
    {
        /// <summary>
        /// - Mã nhân viên
        /// </summary>
        /// Created By: DDKhang (24/5/2023)
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// - Tên đầy đủ
        /// </summary>
        /// Created By: DDKhang (24/5/2023)
        public string FullName { get; set; }

        /// <summary>
        /// - Mã nhân viên
        /// </summary>
        /// Created By: DDKhang (24/5/2023)
        public string EmployeeCode { get; set; }

        /// <summary>
        /// - Ngày sinh
        /// </summary>
        /// Created By: DDKhang (24/5/2023)
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// - Giới tính
        /// </summary>
        /// Created By: DDKhang (24/5/2023)
        public int? Gender { get; set; }

        /// <summary>
        /// - Tên giới tính
        /// </summary>
        /// Created By: DDKhang (24/5/2023)
        public string? GenderName
        {
            get
            {
                switch (Gender)
                {
                    case (int)GenderEnum.Male:
                        return "Nam";
                    case (int)GenderEnum.Female:
                        return "Nữ";
                    default:
                        return "Không xác định";
                }
            }
        }

        /// <summary>
        /// - Địa chỉ Email
        /// </summary>
        /// Created By: DDKhang (24/5/2023)
        public string? Email { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        /// Created By: DDKhang (24/5/2023)
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// - Số điện thoại cố định
        /// </summary>
        /// Created By: DDKhang (24/5/2023)
        public string? LandlinePhone { get; set; }

        /// <summary>
        /// - Chứng minh thư nhân dân
        /// </summary>
        /// Created By: DDKhang (24/5/2023)
        public string? IdentityNumber { get; set; }

        /// <summary>
        /// - Ngày cấp
        /// </summary>
        /// Created By: DDKhang (24/5/2023)
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// - Nơi cấp
        /// </summary>
        /// Created By: DDKhang (24/5/2023)
        public string? IdentityPlace { get; set; }

        /// <summary>
        /// - Mã Phòng ban
        /// </summary>
        /// Created By: DDKhang (24/5/2023)
        public Guid? DepartmentId { get; set; }

        /// <summary>
        /// - Mã chi nhánh
        /// </summary>
        /// Created By: DDKhang (24/5/2023)
        public Guid? BankId { get; set; }

    }
}
