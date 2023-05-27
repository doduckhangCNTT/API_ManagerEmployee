using MISA.WebFresher032023.Practice.Common.Enum;
using MISA.WebFresher032023.Practice.DL.Entity;

namespace MISA.WebFresher032023.Practice.Model
{
    public class Employee : BaseEntity
    {
        public Guid EmployeeId { get; set; }
        public string FullName { get; set; }
        public string EmployeeCode { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? Gender { get; set; }

        public string? GenderName
        {
            get
            {
                switch(Gender)
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

        public string? Email {get; set;}
        public string? PhoneNumber {get; set;}
        public string? LandlinePhone {get; set;}
        public string? IdentityNumber { get; set; }
        public string? IdentityDate { get; set; }
        public string? IdentityPlace { get; set; }
        public Guid? DepartmentId { get; set; }
        //public DateTime? CreatedDate { get; set; }
        //public string? CreatedBy { get; set; }
        //public DateTime? ModifiedDate { get; set; }
        //public string? ModifiedBy { get; set; }
        public Guid? BankId { get; set; }
    }
}
