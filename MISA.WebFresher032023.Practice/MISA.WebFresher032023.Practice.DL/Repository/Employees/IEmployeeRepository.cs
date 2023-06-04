using MISA.WebFresher032023.Practice.DL.Entity;
using MISA.WebFresher032023.Practice.DL.Repository.Bases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Practice.DL.Repository.Employees
{
    /// <summary>
    /// - Interface nhân viên chứa các giao diện phương thức chung và riêng
    /// </summary>
    /// CreateBy: DDKhang (3/6/2023)
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        Task<bool> CheckEmployeeCode(string employeeCode);

        //Task<Employee> GetAsync(Guid employeeId);

        ///// <summary>
        ///// - DbConnection: sẽ chứa chuỗi kết nôi trả về
        ///// - Khi gọi hàm sự sẽ tự mở connection ra cho mình
        ///// - Nếu đã kết nối đến database trước đó rồi thì -> tái sử dụng connection đó
        ///// - Nếu chưa có chuỗi kết nối đến database thì -> tạo connection mới
        ///// </summary>
        ///// <returns></returns>
        //Task<DbConnection> GetOpenConnection();

        //Task<Employee> UpdateAsync(Guid employeeId, Employee employee);
    }
}
