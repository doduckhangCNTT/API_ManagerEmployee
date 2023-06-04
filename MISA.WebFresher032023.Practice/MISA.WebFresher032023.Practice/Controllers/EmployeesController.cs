using Dapper;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher032023.Pactice.BL.DTO.Emoloyees;
using MISA.WebFresher032023.Pactice.BL.Service.Employees;
using MISA.WebFresher032023.Practice.Common.Enum;
using MISA.WebFresher032023.Practice.Common.Resources;
using MISA.WebFresher032023.Practice.DL.Entity;
using MySqlConnector;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.WebFresher032023.Practice.Controllers
{
    [Route("api/v1/[controller]")]
    //[ApiController]
    public class EmployeesController : BaseController<Employee, EmployeeDto, EmployeeUpdateDto, EmployeeCreateDto>
    {
        #region Feild
        //private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly IEmployeeService _employeeService;
        #endregion

        #region Constructor
        public EmployeesController(IConfiguration configuration, IEmployeeService employeeService) : base(employeeService)
        {
            _employeeService = employeeService;
            _connectionString = configuration["ConnectionStrings"] ?? "";
        }

        //public EmployeesController(Employee employee, IConfiguration configuration, IEmployeeService employeeService)
        //{
        //    //_configuration = configuration;
        //    //_connectionString = configuration["ConnectionStrings"] ?? "";
        //    _employeeService = employeeService;
        //}
        #endregion

        #region Functions
        /// <summary>
        /// - Xử lí các ngoại lệ
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <param name="contentErrorForUser">Nội dung lỗi</param>
        /// <returns></returns>
        /// Created By: DDKhang (24/5/2023)
        private IActionResult HandleException(Exception ex, string contentErrorForUser)
        {
            var error = new ErrorService();
            error.DevMsg = ex.Message;
            error.UserMsg = contentErrorForUser;
            error.Data = ex.Data;
            return StatusCode(500, error);
        }

        /// <summary>
        /// - Kiểm tra nhân viên tồn tại
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns>Employee</returns>
        /// Created By: DDKhang (24/5/2023)
        //private Employee CheckEmployeeExist(Guid employeeId)
        //{
        //    // Kiểm tra nhân viên có tồn tại
        //    using var sqlConnection = new MySqlConnection(_connectionString);
        //    // 1. Câu lệnh truy vấn database
        //    string sqlCommand = $"SELECT * FROM employee WHERE EmployeeId = @EmployeeId";
        //    DynamicParameters parameters = new DynamicParameters();
        //    parameters.Add("@EmployeeId", employeeId);
        //    // 2. Thực hiện lấy dữ liệu
        //    Employee employee = sqlConnection.QueryFirstOrDefault<Employee>(sqlCommand, param: parameters);

        //    return employee;
        //}

        /// <summary>
        /// - Kiểm tra trùng mã nhân viên code
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        //private Employee CheckDuplicateEmployeeCode(string employeeCode)
        //{
        //    return new Employee();

        //    //using var sqlConnection = new MySqlConnection(_connectionString);
        //    //string sqlCommand = $"SELECT * FROM employee WHERE EmployeeCode = @EmployeeCode";
        //    //DynamicParameters parameters = new DynamicParameters();
        //    //parameters.Add("@EmployeeCode", employeeCode);
        //    //// 2. Thực hiện lấy dữ liệu
        //    //Employee employee = sqlConnection.QueryFirstOrDefault<Employee>(sqlCommand, param: parameters);

        //    //return employee;
        //}
        #endregion

        string connectionString = "Server = localhost; Port=3306; Database =misa.web202303_mf1622_ddkhang; Uid=root; Pwd=4568527931ab;Allow User Variables=True;";

        /// <summary>
        /// - Thực hiện lấy toàn bộ thông tin nhân viên
        /// </summary>
        /// <returns></returns>
        /// Created By: DDKhang (24/5/2023)
        //[HttpGet]
        //public async Task<IEnumerable<Employee>> GetEmployees()
        //{
        //    return await Task.FromResult(new List<Employee>());
        //    //try
        //    //{
        //    //    // Khởi tạo kết nối với MariaDb
        //    //    //using var sqlConnection = new MySqlConnection(connectionString);
        //    //    using var sqlConnection = new MySqlConnection(_connectionString);
        //    //    // Lấy dữ liệu từ database
        //    //    // 1. Câu lệnh truy vấn database
        //    //    string sqlCommand = "SELECT * FROM employee ORDER BY CreatedDate DESC";
        //    //    // 2. Thực hiện lấy dữ liệu
        //    //    var employees = sqlConnection.Query<Employee>(sqlCommand);

        //    //    // Trả về kết quả truy vấn cho client
        //    //    return Ok(employees);
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    return HandleException(ex, ResourceVN.Error_Exception);
        //    //}
        //}

        /// <summary>
        /// - Thực hiện lấy thông tin nhân viên theo id
        /// </summary>
        /// <param name="employeeId">Mã nhân viên</param>
        /// <returns></returns>
        //[HttpGet("{employeeId}")]
        //public async Task<EmployeeDto> GetEmployeeId(Guid employeeId)
        //{
        //    var employeeDto = await _employeeService.GetAsync(employeeId);
        //    return employeeDto;

        //    try
        //    {
        //        //// Khởi tạo kết nối với MariaDb
        //        ////using var sqlConnection = new MySqlConnection(connectionString);
        //        //using var sqlConnection = new MySqlConnection(_connectionString);
        //        //// Lấy dữ liệu từ database
        //        //// 1. Câu lệnh truy vấn database
        //        //string sqlCommand = $"SELECT * FROM employee WHERE EmployeeId = @EmployeeId";

        //        //DynamicParameters parameters = new DynamicParameters();
        //        //parameters.Add("@EmployeeId", employeeId);

        //        //// 2. Thực hiện lấy dữ liệu
        //        //var employees = await sqlConnection.QueryFirstOrDefaultAsync<Employee>(sqlCommand, param: parameters);

        //        //// Trả về kết quả truy vấn cho client
        //        //return Ok(employees);

        //        //var employee = await _employeeService.GetAsync(employeeId);
        //        //return employee;
        //    }
        //    catch (Exception ex)
        //    {
        //        //return HandleException(ex, Resources.ResourceVN.Error_Exception);

        //        //var error = new ErrorService();
        //        //error.DevMsg = ex.Message;
        //        //error.UserMsg = Resources.ResourceVN.Error_Exception;
        //        //error.Data = ex.Data;
        //        //return StatusCode(500, error);
        //    }
        //}

        /// <summary>
        ///  - Thực hiện lọc thông tin nhân viên & phân trang
        /// </summary>
        /// <param name="pageSize">Số lượng trên trang</param>
        /// <param name="pageNumber">Số trang</param>
        /// <param name="employeeFilter">Gía trị lọc (EmployeeCode - FullName)</param>
        /// <returns></returns>
        //[HttpGet("filter")]
        //public IActionResult EmployeesFilter(int? pageSize, int? pageNumber, string? employeeFilter)
        //{
        //    try
        //    {
        //        var employeeFilterResult = new EmployeeFilter();
        //        // Khởi tạo danh sách nhân viên
        //        List<Employee> employees = new List<Employee>();

        //        // Khởi tạo giá trị ban đầu
        //        int skip = 0;
        //        if (pageNumber == null)
        //        {
        //            pageNumber = 1;
        //        }
        //        if (employeeFilter == null)
        //        {
        //            employeeFilter = "";
        //        }
        //        if (pageSize > 0 && pageNumber > 0)
        //        {
        //            skip = (int)((pageNumber - 1) * pageSize);
        //        }
        //        // Thực hiện kết nối & mở kết nối đến database
        //        //using var sqlConnection = new MySqlConnection(connectionString);
        //        using var sqlConnection = new MySqlConnection(connectionString);
        //        sqlConnection.Open();

        //        string sqlCommandTotalEmployees = $"SELECT * FROM employee e";
        //        var totalEmployees = sqlConnection.Query<Employee>(sqlCommandTotalEmployees);
        //        if (pageSize == null)
        //        {
        //            pageSize = totalEmployees.Count();
        //        }

        //        // Kết nối StoredProcedure - Thực hiện lọc 
        //        string sqlCommandProcFilter = "Proc_FilterEmployee";
        //        using (MySqlCommand command = new MySqlCommand(sqlCommandProcFilter, sqlConnection))
        //        {
        //            // Khai báo sử dụng stored procedure
        //            command.CommandType = System.Data.CommandType.StoredProcedure;
        //            // Thêm các tham số vào stored procedure (nếu có)
        //            command.Parameters.AddWithValue("@employeeFilter", employeeFilter);
        //            command.Parameters.AddWithValue("@pageSize", pageSize);
        //            command.Parameters.AddWithValue("@skip", skip);

        //            // Thực thi stored procedure
        //            using (MySqlDataReader reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    Employee employee = new()
        //                    {
        //                        EmployeeId = reader.GetGuid("EmployeeId"),
        //                        FullName = reader.GetString("FullName"),
        //                        EmployeeCode = reader.GetString("EmployeeCode"),
        //                        Gender = (int)(reader.IsDBNull(reader.GetOrdinal("Gender")) ? 2 : reader.GetInt16("Gender"))
        //                    };
        //                    employees.Add(employee);
        //                }
        //            }
        //        }
        //        // Cấu hình dữ liệu trả về cho client
        //        employeeFilterResult.TotalRecord = (int)totalEmployees.Count();
        //        employeeFilterResult.TotalPage = (int)Math.Ceiling((float)(totalEmployees.Count() / pageSize));
        //        employeeFilterResult.CurrentPage = (int)pageNumber;
        //        employeeFilterResult.CurrentPageRecords = (int)pageSize;
        //        employeeFilterResult.Data = employees.OrderBy(e => e.CreatedDate).ThenByDescending(x => x.CreatedDate).ToArray();

        //        return Ok(employeeFilterResult);
        //    }
        //    catch (Exception ex)
        //    {
        //        return HandleException(ex, Resources.ResourceVN.Error_Exception);
        //    }
        //}


        //[HttpGet("NewEmployeeCode")]
        //public async Task<IActionResult> NewEmployeeCode()
        //{
        //    try
        //    {
        //        using var sqlConnection = new MySqlConnection(_connectionString);
        //        // Lấy dữ liệu từ database
        //        // 1. Câu lệnh truy vấn database
        //        string sqlCommandProc = "Proc_NewEmployeeCode";
        //        using (MySqlConnection connection = new MySqlConnection(_connectionString))
        //        {
        //            connection.Open();

        //            MySqlCommand command = new MySqlCommand(sqlCommandProc, connection);
        //            command.CommandType = CommandType.StoredProcedure;

        //            // Add the output parameter
        //            command.Parameters.Add(new MySqlParameter("@newCode", MySqlDbType.VarChar, 255));
        //            command.Parameters["@newCode"].Direction = ParameterDirection.Output;

        //            // Thực thi stored Procedure
        //            command.ExecuteNonQuery();

        //            string newEmployeeCode = command.Parameters["@newCode"].Value?.ToString() ?? "";

        //            connection.Close();
        //            return Ok(newEmployeeCode);

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return HandleException(ex, ResourceVN.Error_Exception);
        //    }
        //}

        /// <summary>
        /// - Thêm nhân viên mới
        /// </summary>
        /// <param name="employee">Thông tin nhân viên mới</param>
        /// <returns></returns>
        //[HttpPost]
        //public async Task<IActionResult> AddEmployee(Employee employee)
        //{
        //    //return Ok();
        //    try
        //    {
        //        // Tạo id mới cho nhân viên mới
        //        employee.EmployeeId = Guid.NewGuid();
        //        employee.CreatedDate = DateTime.Now;
        //        string inputDate = employee.IdentityDate.ToString();

        //        // Thực hiện validate dữ liệu

        //        // Kết nối database
        //        using var mySqlConnection = new MySqlConnection(connectionString);
        //        //using var mySqlConnection = new MySqlConnection(_connectionString);
        //        mySqlConnection.Open();

        //        string sqlCommandProc = "Proc_InsertEmployee";
        //        // Đọc các tham số đầu vào của store procedure
        //        var sqlCommand = mySqlConnection.CreateCommand();
        //        sqlCommand.CommandText = sqlCommandProc;
        //        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
        //        // Lấy các tham số của Stored Procedure
        //        MySqlCommandBuilder.DeriveParameters(sqlCommand);

        //        var dynamicParam = new DynamicParameters();
        //        foreach (MySqlParameter parameter in sqlCommand.Parameters)
        //        {
        //            // Tên tham số
        //            var paramName = parameter.ParameterName;
        //            // Bỏ tiền tố "m_" trong tham số
        //            var propName = paramName.Replace("@m_", "");
        //            var entityProperty = employee.GetType().GetProperty(propName);
        //            if (entityProperty != null)
        //            {
        //                var propValue = employee.GetType().GetProperty(propName)?.GetValue(employee);
        //                dynamicParam.Add(paramName, propValue);
        //            }
        //            else
        //            {
        //                dynamicParam.Add(paramName, null);
        //            }
        //        }

        //        var res = mySqlConnection.Execute(sql: sqlCommandProc, param: dynamicParam, commandType: System.Data.CommandType.StoredProcedure);
        //        if (res > 0)
        //        {
        //            return StatusCode((int)HttpStatusCodeEnum.CreatedSuccess, res);
        //        }
        //        else
        //        {
        //            return Ok(res);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return HandleException(ex, ResourceVN.Error_Exception);
        //    }
        //}

        /// <summary>
        /// - Thực hiện cập nhật thông tin nhân viên
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        /// Created By: DDKhang (24/5/2023)
        //[HttpPut]
        //public override async Task<IActionResult> UpdateAsync(EmployeeUpdateDto employee)
        //{
        //    try
        //    {
        //        // Thực hiện validate dữ liệu

        //        // Kiểm tra nhân viên có tồn tại
        //        EmployeeDto employeeDto = await _baseService.CheckEntityExist(employee.EmployeeId);

        //        if (employeeDto != null)
        //        {
        //            int qualityRecordUpdate = await _baseService.UpdateAsync(employee);
        //            return Ok(qualityRecordUpdate);
        //        }
        //        else
        //        {
        //            return BadRequest(ResourceVN.Validate_EmployeeNotFound);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return HandleException(ex, ResourceVN.Error_Exception);
        //    }
        //}

        /// <summary>
        /// - Xóa nhân viên theo id
        /// </summary>
        /// <param name="employeId">Mã nhân viên</param>
        /// <returns></returns>
        /// Created By: DDKhang (24/5/2023)
        //[HttpDelete]
        //public async Task<int> DeleteEmployee(Guid employeeId)
        //{
        //    return await _employeeService.DeleteAsync(employeeId);

        //    // ============== Thực hiện xóa bằng Stored Procedure
        //    //try
        //    //{
        //    //    if (employeeId != null)
        //    //    {
        //    //        // Kiểm tra nhân viên có tồn tại
        //    //        Employee e = CheckEmployeeExist(employeeId);

        //    //        if (e != null)
        //    //        {
        //    //            string sqlCommandProcDeleteEmployee = "Proc_DeleteEmployeeById";
        //    //            // Thực hiện kết nối & mở kết nối đến database
        //    //            //using MySqlConnection connection = new MySqlConnection(connectionString);
        //    //            using var connection = new MySqlConnection(_connectionString);

        //    //            using MySqlCommand command = new MySqlCommand(sqlCommandProcDeleteEmployee, connection);
        //    //            command.CommandType = CommandType.StoredProcedure;

        //    //            // Thêm tham số vào stored procedure
        //    //            command.Parameters.Add(new MySqlParameter("@m_EmployeeId", employeeId));
        //    //            // Thêm các tham số khác tùy thuộc vào số lượng và kiểu dữ liệu của stored procedure

        //    //            // Mở kết nối với cơ sở dữ liệu
        //    //            connection.Open();

        //    //            // Thực hiện stored procedure
        //    //            command.ExecuteNonQuery();
        //    //        }
        //    //        else
        //    //        {
        //    //            return StatusCode(400, ResourceVN.Validate_EmployeeNotFound);
        //    //        }
        //    //    }
        //    //    else
        //    //    {
        //    //        return StatusCode((int)HttpStatusCodeEnum.BadRequest, ResourceVN.Validate_EmployeeNotFound);
        //    //    }
        //    //    return StatusCode((int)HttpStatusCodeEnum.Success, ResourceVN.Notification_Delete_Success);
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    return HandleException(ex, ResourceVN.Error_Exception);
        //    //}
        //}

        [HttpGet("export")]
        public async Task<IActionResult> ExportToExcel()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = $@"
                    SELECT  EmployeeCode, FullName, Gender, DateOfBirth, d.DepartmentName 
                    FROM Employee e
                    LEFT JOIN Department d on e.DepartmentId = d.DepartmentId
                    ";
                var data = connection.Query<Employee>(query).ToList();

                // Tiêu đề cột
                var columnHeaders = new string[] { "STT", "Mã nhân viên", "Tên nhân viên", "Giới tính", "Ngày sinh", "Chức danh", "Tên đơn vị", "Số tài khoản", "Tên ngân hàng" };

                // Tạo một file Excel mới
                using (var package = new ExcelPackage())
                {
                    // Tạo một worksheet mới
                    var worksheet = package.Workbook.Worksheets.Add("Sheet 1");

                    // Tiêu đề bảng
                    worksheet.Cells["A1:I1"].Merge = true;
                    worksheet.Cells["A1:I1"].Value = "DANH SÁCH NHÂN VIÊN";
                    worksheet.Cells["A1:I1"].Style.Font.Bold = true;
                    worksheet.Cells["A1:I1"].Style.Font.Size = 16;

                    // // Xét Fill cho dòng
                    //worksheet.Cells["A1:I1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    // // Không xét Fill cho dòng
                    //worksheet.Cells["A1:I1"].Style.Fill.PatternType = ExcelFillStyle.None;

                    //worksheet.Cells["A1:I1"].Style.Fill.BackgroundColor.SetColor(Color.White);
                    //worksheet.Cells["A1:I1"].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                    worksheet.Cells["A1:I1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    // Tiêu đề cột
                    for (int i = 0; i < columnHeaders.Length; i++)
                    {
                        var columnHeaderCell = worksheet.Cells[3, i + 1];
                        columnHeaderCell.Value = columnHeaders[i];
                        columnHeaderCell.Style.Font.Bold = true;
                        columnHeaderCell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        columnHeaderCell.Style.Fill.SetBackground(Color.LightGray);
                        columnHeaderCell.Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                    }

                    // Dữ liệu bảng
                    for (int i = 0; i < data.Count; i++)
                    {
                        string genderName = "";
                        string dateOfBirth = "";

                        if(data[i].DateOfBirth != null)
                        {
                            DateTime inputDate = (DateTime)data[i].DateOfBirth;
                            string formattedDate = inputDate.ToString("M/d/yyyy");
                            dateOfBirth = formattedDate;

                        }
                        switch (data[i].Gender)
                        {
                            case 0:
                                genderName = ResourceVN.Gender_Male;
                                break;
                            case 1:
                                genderName = ResourceVN.Gender_Femal;
                                break;
                            default:
                                genderName = ResourceVN.Gender_Other;
                                break;
                        }

                        var rowData = new string[] {(i+1).ToString(), data[i].EmployeeCode.ToString(), data[i].FullName, genderName, dateOfBirth, "", "", "", "" };
                        for (int j = 0; j < rowData.Length; j++)
                        {
                            var cell = worksheet.Cells[i + 4, j + 1];
                            cell.Value = rowData[j];
                            cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                            // Căn chỉnh độ rộng cột dựa trên độ dài dữ liệu
                            worksheet.Column(j + 1).AutoFit();

                            //// Đặt màu cho ô
                            //if (i % 2 == 0)
                            //{
                            //    cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            //    cell.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                            //}

                            // Thêm border cho ô
                            cell.Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                        }
                        // Thêm kẻ dòng
                        //worksheet.Row(i + 4).Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        //worksheet.Row(i + 4).Style.Border.Bottom.Color.SetColor(Color.Black);
                    }

                    // Merge cột STT
                    //worksheet.Cells["A4:A" + (data.Count + 3)].Merge = true;

                    // Trả về file Excel
                    var stream = new MemoryStream(package.GetAsByteArray());
                    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "export.xlsx");
                }

            }
        }
    }
}
