using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.WebFresher032023.Practice.DL.Entity;
using MISA.WebFresher032023.Practice.DL.Repository.Bases;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Practice.DL.Repository.Employees
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        #region Feild
        //private readonly IConfiguration _configuration;
        //private readonly string _connectionString;
        #endregion

        #region Constructor
        public EmployeeRepository(IConfiguration configuration) : base(configuration)
        {
            //_configuration = configuration; // Do hàm cha đã được khởi tạo Configuration
            //_connectionString = configuration["ConnectionStrings"] ?? "";
        }
        #endregion

        /// <summary>
        /// - Mở kết nối đến database
        /// - Không sử dụng using trước chỗ gọi kết nối, bởi vì khi nhảy ra khỏi hàm thì using sẽ tự động đóng kết nối
        /// </summary>
        /// <returns></returns>
        //public async Task<DbConnection> GetOpenConnection()
        //{
        //    var sqlConnection = new MySqlConnection(_connectionString);
        //    await sqlConnection.OpenAsync();
        //    return sqlConnection;
        //}

        //public async Task<Employee> GetAsync(Guid employeeId)
        //{
        //    // Khởi tạo kết nối với MariaDb
        //    using var sqlConnection = await GetOpenConnectionAsync();

        //    // Lấy dữ liệu từ database
        //    // 1. Câu lệnh truy vấn database
        //    string sqlCommand = $"SELECT * FROM employee WHERE EmployeeId = @EmployeeId";
        //    DynamicParameters parameters = new DynamicParameters();
        //    parameters.Add("@EmployeeId", employeeId);

        //    // 2. Thực hiện lấy dữ liệu
        //    var employee = await sqlConnection.QueryFirstOrDefaultAsync<Employee>(sqlCommand, param: parameters);

        //    await sqlConnection.CloseAsync(); 
        //    // Trả về kết quả truy vấn cho client
        //    return employee;
        //}

        //public override async Task<Employee> UpdateAsync(Guid employeeId, Employee employee)
        //{
        //    var employeeNew = await GetAsync(employeeId);
        //    return employeeNew;
        //}

        public async Task<bool> CheckEmployeeCode(string employeeCode)
        {
            //using var sqlConnection = await GetOpenConnectionAsync();
            throw new NotImplementedException();
        }

        public Task<Employee> DeleteAsync(Guid id, Employee entity)
        {
            throw new NotImplementedException();
        }
    }
}
