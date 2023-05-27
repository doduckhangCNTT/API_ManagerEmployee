using Dapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher032023.Pactice.BL.DTO.Departments;
using MISA.WebFresher032023.Pactice.BL.Service.Bases;
using MISA.WebFresher032023.Pactice.BL.Service.Departments;
using MISA.WebFresher032023.Practice.Common.Exception;
using MISA.WebFresher032023.Practice.Common.Resources;
using MISA.WebFresher032023.Practice.DL.Entity;
using MySqlConnector;

namespace MISA.WebFresher032023.Practice.Controllers
{

    [Microsoft.AspNetCore.Components.Route("api/v1/[controller]")]
    public class DepartmentsController : BaseController<Department, DepartmentDTO, DepartmentUpdateDTO>
    {
        #region Field
        private readonly string _connectionString; 
        #endregion

        #region Constructor
        public DepartmentsController(IConfiguration configuration, IDepartmentService departmentService) : base(departmentService)
        {
            _connectionString = configuration["ConnectionStrings"] ?? "";
        }
        #endregion

        /// <summary>
        /// - Lấy toàn bộ phòng ban
        /// </summary>
        /// <returns>IEnumerable<Department></returns>
        /// <exception cref="InternalException"></exception>
        /// CreatedBy: DDKhang (23/5/2023)
        [HttpGet]
        public async Task<IEnumerable<Department>> GetDepartments()
        {
            try
            {
                // Khởi tạo kết nối với MariaDb
                //using var sqlConnection = new MySqlConnection(connectionString);
                using var sqlConnection = new MySqlConnection(_connectionString);
                // Lấy dữ liệu từ database
                // 1. Câu lệnh truy vấn database
                string sqlCommand = "SELECT * FROM department LIMIT 5";
                // 2. Thực hiện lấy dữ liệu
                IEnumerable<Department> departments = sqlConnection.Query<Department>(sqlCommand);

                // Trả về kết quả truy vấn cho client
                return await Task.FromResult(departments);
            }
            catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }
    }
}
