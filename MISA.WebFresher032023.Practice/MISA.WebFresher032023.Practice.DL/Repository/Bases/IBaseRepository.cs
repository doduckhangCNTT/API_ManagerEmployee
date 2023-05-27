using MISA.WebFresher032023.Practice.DL.Entity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Practice.DL.Repository.Bases
{
    public interface IBaseRepository<TEntity>
    {
        /// <summary>
        /// - Thực hiện kết nối đến database
        /// - DbConnection: sẽ chứa chuỗi kết nôi trả về
        /// - Khi gọi hàm sự sẽ tự mở connection ra cho mình
        /// - Nếu đã kết nối đến database trước đó rồi thì -> tái sử dụng connection đó
        /// - Nếu chưa có chuỗi kết nối đến database thì -> tạo connection mới
        /// </summary>
        /// <returns></returns>
        /// Created By: DDKhang (22/5/2023)
        Task<DbConnection> GetOpenConnectionAsync();

        /// <summary>
        /// - Thực hiện lấy dữ liệu bằng id
        /// </summary>
        /// <param name="id">Mã</param>
        /// <returns></returns>
        /// Created By: DDKhang (22/5/2023)
        Task<TEntity> GetAsync(Guid id);

        /// <summary>
        /// - Thực hiện thêm mới thông tin entity
        /// </summary>
        /// <param name="entity">Thông tin thực thể</param>
        /// <returns>Số bản ghi được thêm</returns>
        Task<int> CreateAsync(TEntity entity);

        /// <summary>
        /// - Thực hiện cập nhật thông tin "Entity" 
        /// </summary>
        /// <param name="id">Mã</param>
        /// <param name="entity">Thực thể</param>
        /// <returns></returns>
        /// Created By: DDKhang (22/5/2023)
        Task<int> UpdateAsync(TEntity entity);

        /// <summary>
        /// - Thực hiện xóa 
        /// </summary>
        /// <param name="id">Mã</param>
        /// <param name="entity">Thực thể</param>
        /// <returns>Tổng số bản ghi được xóa</returns>
        /// Created By: DDKhang (22/5/2023)
        Task<int> DeleteAsync(Guid id);

        /// <summary>
        /// - Thực hiện lọc thông tin entity và phân trang
        /// </summary>
        /// <param name="pageSize">Số lượng entity trên trang</param>
        /// <param name="pageNumber">Trang hiện tại</param>
        /// <param name="entityFilter">Gía trị lọc</param>
        /// <param name="skip">Số lượng bản ghi bỏ qua</param>
        /// <returns></returns>
        Task<FilterEntity<TEntity>> EntityFilterAsync(int? pageSize, int? pageNumber, string? entityFilter, int skip);

        /// <summary>
        /// - Thực hiện tạo mã code mới
        /// </summary>
        /// <returns>String</returns>
        Task<string> NewEntityCode();
    }
}
