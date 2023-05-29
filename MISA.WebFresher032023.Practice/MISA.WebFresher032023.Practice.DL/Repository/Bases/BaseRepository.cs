using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MISA.WebFresher032023.Practice.Common.Enum;
using MISA.WebFresher032023.Practice.Common.Exception;
using MISA.WebFresher032023.Practice.Common.Resources;
using MISA.WebFresher032023.Practice.DL.Entity;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Practice.DL.Repository.Bases
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
    {
        #region Field
        private readonly string _connectionString;
        #endregion

        #region Constructor
        public BaseRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings"] ?? "";
        }
        #endregion

        /// <summary>
        /// - Thực hiện mở kết nối đến database
        /// </summary>
        /// <returns>DbConnection</returns>
        public virtual async Task<DbConnection> GetOpenConnectionAsync()
        {
            var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();
            return connection;
        }

        /// <summary>
        /// - Lấy thông tin của enity theo id
        /// </summary>
        /// <param name="id">Mã entity</param>
        /// <returns>TEntity</returns>
        /// <exception cref="InternalException"></exception>
        /// Created By: DDKhang (24/5/2023)
        public virtual async Task<TEntity> GetAsync(Guid id)
        {
            try
            {
                string tableName = typeof(TEntity).Name;
                // Khởi tạo kết nối với MariaDb
                using var sqlConnection = await GetOpenConnectionAsync();

                // Lấy dữ liệu từ database
                //// === Cách 1: Sử dụng câu truy vấn ===
                //// 1. Câu lệnh truy vấn database
                //string sqlCommand = $"SELECT * FROM {tableName} WHERE {tableName}Id = @Id";
                //DynamicParameters parameters = new DynamicParameters();
                //parameters.Add("@Id", id);

                //// 2. Thực hiện lấy dữ liệu
                //var entity = await sqlConnection.QueryFirstOrDefaultAsync<TEntity>(sqlCommand, param: parameters);

                // === Cách 2: Gọi Stored Procedure ===
                // 1. Khởi tạo lệnh sql gọi đến Stored Procedure
                string sqlCommandProc = "Proc_Get" + tableName + "ById";

                // 2. Thực hiện thêm tham số cho proc
                MySqlCommand command = new MySqlCommand(sqlCommandProc, (MySqlConnection?)sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue($"@m_{tableName}Id", id);

                // 3. Thực thi proc
                using MySqlDataReader reader = await command.ExecuteReaderAsync();

                // Khởi tạo đối tượng chung TEntity
                TEntity entity1 = Activator.CreateInstance<TEntity>();

                // Sử dụng reflection để đặt giá trị cho các thuộc tính của TEntity từ dữ liệu đọc được từ Proc
                PropertyInfo[] properties = typeof(TEntity).GetProperties();

                while (reader.Read())
                {
                    for (int i = 0; i < properties.Length; i++)
                    {
                        // Lấy tên của thuộc tính
                        PropertyInfo property = properties[i];
                        string propertyName = property.Name;

                        if(propertyName != "GenderName")
                        {
                            // Kiểm tra xem cột có tồn tại trong dữ liệu đọc được hay không
                            int columnIndex = reader.GetOrdinal(propertyName);
                            if (columnIndex >= 0 && !reader.IsDBNull(columnIndex))
                            {
                                object value = reader.GetValue(columnIndex);
                                property.SetValue(entity1, value);
                            }
                        }
                    }
                }
                // Đóng kết nối sql
                await sqlConnection.CloseAsync();
                // Trả về kết quả truy vấn cho client
                return await Task.FromResult(entity1);
            }
            catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }

        /// <summary>
        /// - Thực hiện thêm thông tin
        /// - Để sử dụng:
        ///     + Tạo Proc mới cho entity muốn thêm ("Proc_Insert" + entityName)
        ///     + Các tham số của các Proc phải giống nhau (m_Name)
        /// </summary>
        /// <param name="entity">Thông tin của thực thể</param>
        /// <returns>Số bản ghi được thêm</returns>
        /// <exception cref="InternalException"></exception>
        /// Created By: DDKhang (24/5/2023)
        public virtual async Task<int> CreateAsync(TEntity entity)
        {
            try
            {
                // Tạo id mới cho nhân viên mới
                //entity.EmployeeId = Guid.NewGuid();
                //entity.CreatedBy = "abc";
                //string inputDate = entity.IdentityDate;

                // Lấy tên của entity
                string tableName = typeof(TEntity).Name;

                // Kết nối database
                using var sqlConnection = await GetOpenConnectionAsync();
                //using var mySqlConnection = new MySqlConnection(sqlConnection);
                //mySqlConnection.Open();

                string sqlCommandProc = "Proc_Insert" + tableName;
                // Đọc các tham số đầu vào của store procedure
                var sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = sqlCommandProc;
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                // Lấy các tham số của Stored Procedure
                MySqlCommandBuilder.DeriveParameters((MySqlCommand)sqlCommand);

                var dynamicParam = new DynamicParameters();
                foreach (MySqlParameter parameter in sqlCommand.Parameters)
                {
                    // Tên tham số
                    var paramName = parameter.ParameterName;
                    // Bỏ tiền tố "m_" trong tham số
                    var propName = paramName.Replace("@m_", "");
                    // Lấy thuộc tính theo tên trong entity -> kiểm sự tồn tại của thuộc tính trong entity đó
                    var entityProperty = entity?.GetType().GetProperty(propName);
                    if (entityProperty != null)
                    {
                        var propValue = entity?.GetType().GetProperty(propName)?.GetValue(entity);
                        dynamicParam.Add(paramName, propValue);
                    }
                    else
                    {
                        dynamicParam.Add(paramName, null);
                    }
                }
                // Số bản ghi được thêm vào
                var res = sqlConnection.Execute(sql: sqlCommandProc, param: dynamicParam, commandType: System.Data.CommandType.StoredProcedure);
                return await Task.FromResult(res);
            }
            catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }

        /// <summary>
        /// - Thực hiện cập nhật thông tin 
        /// </summary>
        /// <param name="entity">Thông tin thực thể mới</param>
        /// <returns>Int - Số lượng bản ghi cập nhật</returns>
        /// <exception cref="InternalException"></exception>
        /// Created By: DDKhang (24/5/2023)
        public virtual async Task<int> UpdateAsync(TEntity entity)
        {
            try
            {
                string tableName = typeof(TEntity).Name;

                // Kết nối database
                using var mySqlConnection = await GetOpenConnectionAsync();

                string sqlCommandProc = "Proc_Update" + tableName;
                // Đọc các tham số đầu vào của stor
                var sqlCommand = mySqlConnection.CreateCommand();
                sqlCommand.CommandText = sqlCommandProc;
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                // Lấy các tham số của Stored Procedure
                MySqlCommandBuilder.DeriveParameters((MySqlCommand)sqlCommand);

                var dynamicParam = new DynamicParameters();
                foreach (MySqlParameter parameter in sqlCommand.Parameters)
                {
                    // Tên tham số
                    var paramName = parameter.ParameterName;
                    // Bỏ tiền tố "m_" trong tham số
                    var propName = paramName.Replace("@m_", "");
                    var entityProperty = entity?.GetType().GetProperty(propName);
                    if (entityProperty != null)
                    {
                        var propValue = entity?.GetType().GetProperty(propName)?.GetValue(entity);
                        dynamicParam.Add(paramName, propValue);
                    }
                    else
                    {
                        dynamicParam.Add(paramName, null);
                    }
                }
                // Lấy số lượng bản ghi được cập nhật
                var result = mySqlConnection.Execute(sql: sqlCommandProc, param: dynamicParam, commandType: System.Data.CommandType.StoredProcedure);
                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }

        /// <summary>
        /// - Thực hiện xóa entity theo id
        /// </summary>
        /// <param name="id">Mã id muốn xóa</param>
        /// <returns>Số lượng bản ghi đã xóa</returns>
        /// Created By: DDKhang (24/5/2023)
        public virtual async Task<int> DeleteAsync(Guid id)
        {
            // Lấy tên của entity
            string tableName = typeof(TEntity).Name;
            // Khởi tạo kết nối với MariaDb
            using var sqlConnection = await GetOpenConnectionAsync();

            //// === Cách 1: Sử dụng truy vấn 
            //// Thực hiện xóa
            //string sqlCommandDelete = $"DELETE FROM {tableName} WHERE {tableName}Id = '{id}'";
            //// Thực hiện chạy sql
            //int result = await sqlConnection.ExecuteAsync(sqlCommandDelete);

            // === Cách 2: Sử dụng Proc
            // 1. Khởi tạo lệnh sql gọi đến Stored Procedure
            string sqlCommandProc = "Proc_Delete" + tableName + "ById";

            // 2. Thực hiện thêm tham số cho proc
            MySqlCommand command = new MySqlCommand(sqlCommandProc, (MySqlConnection?)sqlConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue($"@m_{tableName}Id", id);

            // 3. Thực thi proc
            int result = await command.ExecuteNonQueryAsync();

            // Đóng kết nối db
            await sqlConnection.CloseAsync();
            // Trả về số bản ghi xóa
            return result;
        }

        /// <summary>
        /// - Lọc thông tin entity và phân trang
        /// - Để sử dụng cần thỏa mãn các yếu tố:
        ///     + Đặt tên "Proc_Filter" + tableName;
        ///     + Các thuộc tính của class và có trong dữ liệu trả về từ Proc
        /// </summary>
        /// <param name="pageSize">Số phần tử trên trang</param>
        /// <param name="pageNumber">Trang hiện tại</param>
        /// <param name="entityFilter">Gía trị muốn lọc theo</param>
        /// <param name="skip"></param>
        /// <returns>FilterEntity<TEntity></returns>
        /// <exception cref="InternalException"></exception>
        /// Created By: DDKhang (24/5/2023)
        public virtual async Task<FilterEntity<TEntity>> EntityFilterAsync(int? pageSize, int? pageNumber, string? entityFilter, int skip)
        {
            try
            {
                // Lấy tên của thực thể
                string tableName = typeof(TEntity).Name;
                // Khởi tạo đối tượng lọc
                var entityFilterResult = new FilterEntity<TEntity>();
                // Khởi tạo danh sách thực thể để lưu lại toàn bộ thông tin được lọc
                List<TEntity> entities = new List<TEntity>();

                // Khởi tạo kết nối với MariaDb
                using var sqlConnection = await GetOpenConnectionAsync();

                // Lấy dữ liệu từ database
                // 1. Câu lệnh truy vấn database
                // Thực hiện đếm toàn bộ số bản ghi
                string sqlCommandTotalEntities = $"SELECT * FROM {tableName}";
                var totalEntities = sqlConnection.Query<TEntity>(sqlCommandTotalEntities);
                if (pageSize == null)
                {
                    pageSize = totalEntities.Count();
                }

                // Kết nối StoredProcedure - Thực hiện lọc 
                string sqlCommandProcFilter = "Proc_Filter" + tableName;
                using (MySqlCommand command = new MySqlCommand(sqlCommandProcFilter, (MySqlConnection?)sqlConnection))
                {
                    // Khai báo sử dụng stored procedure
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    // Thêm các tham số vào stored procedure (nếu có)
                    command.Parameters.AddWithValue($"@{tableName.ToLower()}Filter", entityFilter);
                    command.Parameters.AddWithValue("@pageSize", pageSize);
                    command.Parameters.AddWithValue("@skip", skip);

                    // Thực thi stored procedure
                    using MySqlDataReader reader = command.ExecuteReader();

                    // Khởi tạo đối tượng chung TEntity
                    TEntity entity = Activator.CreateInstance<TEntity>();

                    // Sử dụng reflection để đặt giá trị cho các thuộc tính của TEntity lấy các thuộc tính của entity
                    PropertyInfo[] properties = typeof(TEntity).GetProperties();
                    while (reader.Read()) // Đọc từng bản ghi trong MySqlDataReader
                    {
                        for (int i = 0; i < properties.Length; i++)
                        {
                            // Lấy tên của thuộc tính (của từng cột) từ dữ liệu trả về từ proc
                            PropertyInfo property = properties[i];
                            string propertyName = property.Name;

                            /* Vì trường GenderName không không là thuộc tính trong database mà chỉ là trường bổ sung thêm 
                             * - Việc kiểm tra thuộc tính "GenderName" là bởi giá trị trả về từ Proc không có trường đó -> không lấy ra được "columnIndex"
                             * */
                            if (propertyName != "GenderName")
                            {
                                // Kiểm tra xem cột có tồn tại trong dữ liệu đọc được từ Proc hay không
                                int columnIndex = reader.GetOrdinal(propertyName);
                                if (columnIndex >= 0 && !reader.IsDBNull(columnIndex))
                                {
                                    object value = reader.GetValue(columnIndex);
                                    property.SetValue(entity, value);
                                }
                            }
                        }

                        //if (entity.Gender)
                        //{
                        //    switch (entity.Gender)
                        //    {
                        //        case (int)GenderEnum.Male:
                        //            entity.GenderName = "Nam";
                        //            break;
                        //        case (int)GenderEnum.Female:
                        //            entity.GenderName = "Nữ";
                        //            break;
                        //        default:
                        //            entity.GenderName = "Không xác định";
                        //            break;
                        //    }
                        //}

                        // Thêm TEntity vào danh sách
                        entities.Add(entity);

                        // Tạo instance mới của TEntity để đọc bản ghi tiếp theo
                        entity = Activator.CreateInstance<TEntity>();
                    }
                }
                // Cấu hình dữ liệu trả về cho client
                entityFilterResult.TotalRecord = (int)totalEntities.Count();
                entityFilterResult.TotalPage = (int)Math.Ceiling((float)(totalEntities.Count() / pageSize));
                entityFilterResult.CurrentPage = (int)pageNumber;
                entityFilterResult.CurrentPageRecords = (int)pageSize;
                entityFilterResult.Data = entities.ToArray();

                await sqlConnection.CloseAsync();
                return entityFilterResult;
            }
            catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }

        /// <summary>
        /// - Thực hiện tạo mã mới cho entity
        /// </summary>
        /// <returns>String</returns>
        /// <exception cref="InternalException"></exception>
        /// Created By: DDKhang (24/5/2023)
        public virtual async Task<string> NewEntityCode()
        {
            try
            {
                // Lấy tên của thực thể
                string tableName = typeof(TEntity).Name;
                // Khởi tạo kết nối với MariaDb
                using var sqlConnection = await GetOpenConnectionAsync();

                string sqlCommandProc = "Proc_New" + tableName + "Code";
                MySqlCommand command = new MySqlCommand(sqlCommandProc, (MySqlConnection?)sqlConnection);
                command.CommandType = CommandType.StoredProcedure;

                // Add the output parameter
                command.Parameters.Add(new MySqlParameter("@newCode", MySqlDbType.VarChar, 255));
                command.Parameters["@newCode"].Direction = ParameterDirection.Output;

                // Thực thi stored Procedure
                command.ExecuteNonQuery();

                string newEntityCode = command.Parameters["@newCode"].Value?.ToString() ?? "";

                // Đóng kết nối db
                await sqlConnection.CloseAsync();
                return newEntityCode;
            }
            catch (Exception ex)
            {
                throw new InternalException(ex.Message);
            }
        }

        /// <summary>
        /// - Thực hiện kiểm tra sự tồn tại của thực thể
        /// </summary>
        /// <param name="entityId">Mã thực thể</param>
        /// <returns>TEntity</returns>
        /// CreatedBy: DDKhang (27/5/2023)
        public virtual async Task<TEntity> CheckEntityExist(Guid entityId)
        {
            // Lấy tên của thực thể
            string tableName = typeof(TEntity).Name;
            // Khởi tạo kết nối với MariaDb
            using var sqlConnection = await GetOpenConnectionAsync();

            // Kiểm tra nhân viên có tồn tại
            // 1. Câu lệnh truy vấn database
            string sqlCommand = $"SELECT * FROM {tableName} WHERE {tableName}Id = @{tableName}Id";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@{tableName}Id", entityId);
            // 2. Thực hiện lấy dữ liệu
            TEntity entity = sqlConnection.QueryFirstOrDefault<TEntity>(sqlCommand, param: parameters);

            return entity;
        }

        /// <summary>
        /// - Xóa nhiều bản ghi
        /// </summary>
        /// <param name="listEntityId">Danh sách mã bản ghi được nối bằng ","</param>
        /// <returns>Số bản ghi được xóa</returns>
        /// CreatedBy: DDKhang (27/5/2023)
        public virtual async Task<int> DeleteMutilEntityAsync(string listEntityId)
        {
            StringBuilder formatString = new StringBuilder();
            List<string> listId = listEntityId.Split(',').Select(s => s.Trim()).ToList();
            formatString = formatString.Append(string.Join(",", listId));
            string formatResult = formatString.ToString();


            // Lấy tên của entity
            string tableName = typeof(TEntity).Name;
            // Khởi tạo kết nối với MariaDb
            using var sqlConnection = await GetOpenConnectionAsync();

            // Khởi tạo lệnh sql
            string sqlCommandProc = "Proc_Delete" + tableName + "MultiById";

            MySqlCommand command = new MySqlCommand(sqlCommandProc, (MySqlConnection?)sqlConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue($"@m_List{tableName}Id", formatResult.Trim());
            // Thực thi proc
            int result = await command.ExecuteNonQueryAsync();

            return result;
        }
    }
}
