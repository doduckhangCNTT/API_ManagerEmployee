using MISA.WebFresher032023.Practice.Common.Enum;
using MISA.WebFresher032023.Practice.Common.Resources;
using MISA.WebFresher032023.Practice.DL.Entity;

namespace MISA.WebFresher032023.Practice.Model
{
    /// <summary>
    /// - Lớp nhân viên, chứa thông tin các thuộc tính của nhân viên
    /// Created By: DDKhang (24/5/2023)
    /// </summary>
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
        /// Created By: DDKhang (24/5/2023)
        /// </summary>
        public string? GenderName
        {
            get
            {
                switch(Gender)
                {
                    case (int)GenderEnum.Male:
                        return ResourceVN.Gender_Male;
                    case (int)GenderEnum.Female:
                        return ResourceVN.Gender_Femal;
                    default:
                        return ResourceVN.Gender_Other;
                }
            }
        }

        /// <summary>
        /// - Địa chỉ Email
        /// </summary>
        /// Created By: DDKhang (24/5/2023)
        public string? Email {get; set;}

        /// <summary>
        /// - Địa chỉ
        /// </summary>
        /// Created By: DDKhang (24/5/2023)
        public string? Address { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        /// Created By: DDKhang (24/5/2023)
        public string? PhoneNumber {get; set;}

        /// <summary>
        /// - Số điện thoại cố định
        /// </summary>
        /// Created By: DDKhang (24/5/2023)
        public string? LandlinePhone {get; set;}

        /// <summary>
        /// - Chứng minh thư nhân dân
        /// </summary>
        /// Created By: DDKhang (24/5/2023)
        public string? IdentityNumber { get; set; }

        /// <summary>
        /// - Ngày cấp
        /// </summary>
        /// Created By: DDKhang (24/5/2023)
        public string? IdentityDate { get; set; }

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

        //public DateTime? CreatedDate { get; set; }
        //public string? CreatedBy { get; set; }
        //public DateTime? ModifiedDate { get; set; }
        //public string? ModifiedBy { get; set; }

        /// <summary>
        /// - Mã chi nhánh
        /// </summary>
        /// Created By: DDKhang (24/5/2023)
        public Guid? BankId { get; set; }
    }
}
