using MISA.WebFresher032023.Pactice.BL.DTO;
using MISA.WebFresher032023.Practice.DL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Pactice.BL.Service.Bases
{
    public interface IBaseService<IEntity, IEntityDto, TEntityUpdateDto>
    {
        /// <summary>
        /// - Thực hiện lấy thông tin entity theo id
        /// </summary>
        /// <param name="id">Mã entity</param>
        /// <returns>IEntityDto</returns>
        /// Create By: DDKhang (24/5/2023)
        Task<IEntityDto> GetAsync(Guid id);

        /// <summary>
        /// - Thực hiện thêm thông tin 
        /// </summary>
        /// <param name="entity">Thông tin thực thể</param>
        /// <returns>Số bản ghi được thêm</returns>
        /// Create By: DDKhang (24/5/2023)
        Task<int> CreateAsync(IEntity entity);

        /// <summary>
        /// - Thực hiện xóa entity theo id
        /// </summary>
        /// <param name="id">Mã entity</param>
        /// <returns>int - Số bản ghi bị xóa</returns>
        /// <exception cref="NotFoundException"></exception>
        /// Create By: DDKhang (24/5/2023)
        Task<int> DeleteAsync(Guid id);

        /// <summary>
        /// - Thực hiện cập nhật thông tin thực thể
        /// </summary>
        /// <param name="entity">Thông tin thực thể mới</param>
        /// <returns>Số bản ghi được cập nhật</returns>
        Task<int> UpdateAsync(IEntity entity);

        /// <summary>
        /// - Thực hiện lọc thông tin của entity, phân trang
        /// </summary>
        /// <param name="pageSize">Số lượng entity trên trang</param>
        /// <param name="pageNumber">Trang hiện tại</param>
        /// <param name="entityFilter">Gía trị lọc</param>
        /// <returns>IEntity</returns>
        /// Create By: DDKhang (24/5/2023)
        Task<FilterEntity<IEntity>> EntitysFilterAsync(int? pageSize, int? pageNumber, string? entityFilter);

        /// <summary>
        /// - Tạo mã code mới cho entity
        /// </summary>
        /// <returns>String</returns>
        /// Create By: DDKhang (24/5/2023)
        Task<string> NewEntityCode();
    }
}
