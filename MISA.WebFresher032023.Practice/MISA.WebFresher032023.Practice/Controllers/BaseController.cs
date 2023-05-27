using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher032023.Pactice.BL.DTO;
using MISA.WebFresher032023.Pactice.BL.Service.Bases;
using MISA.WebFresher032023.Pactice.BL.Service.Employees;
using MISA.WebFresher032023.Practice.DL.Entity;
using MISA.WebFresher032023.Practice.Model;

namespace MISA.WebFresher032023.Practice.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public abstract class BaseController<TEntity, TEntityDto, TEntityUpdateDto> : ControllerBase
    {
        #region Field
        // Khai báo gọi lên BL
        protected readonly IBaseService<TEntity, TEntityDto, TEntityUpdateDto> _baseService;
        //private IEmployeeService employeeService;
        #endregion

        #region Constructor
        protected BaseController(IBaseService<TEntity, TEntityDto, TEntityUpdateDto> baseService)
        {
            _baseService = baseService;
        }

        //protected BaseController(IEmployeeService employeeService)
        //{
        //    this.employeeService = employeeService;
        //}
        #endregion

        /// <summary>
        /// - Thực hiện lấy entity theo id
        /// </summary>
        /// <param name="id">Mã thực thể</param>
        /// <returns>EntityDto</returns>
        /// Created By: DDKhang (23/5/2023)
        [HttpGet("{id}")]
        public virtual async Task<TEntityDto> GetAsync(Guid id)
        {
            var entityDto = await _baseService.GetAsync(id);
            return entityDto;
        }

        /// <summary>
        /// - Thực hiện tạo mã mới
        /// </summary>
        /// <returns>String</returns>
        /// Create By: DDKhang (24/5/2023)
        [HttpGet("NewCode")]
        public virtual async Task<string> NewEntityCode()
        {
            string newEntityCode = await _baseService.NewEntityCode();
            return newEntityCode;
        }

        /// <summary>
        /// - Thực hiện thêm thông tin 
        /// </summary>
        /// <param name="entity">Thông tin thực thể</param>
        /// <returns>Số lượng bản ghi được thêm</returns>
        /// Create By: DDKhang (24/5/2023)
        [HttpPost]
        public virtual async Task<int> CreateAsync(TEntity entity)
        {
            int qualityRecordAdd = await _baseService.CreateAsync(entity);
            return qualityRecordAdd;
        }

        /// <summary>
        /// - Thực hiện lọc thông tin theo kiểu của mỗi Entity (trong Proc) và phân trang
        /// </summary>
        /// <param name="pageSize">Số lượng sản phẩm trên trang</param>
        /// <param name="pageNumber">Trang hiện tại</param>
        /// <param name="entityFilter">Thông tin cần lọc tùy thuộc việc định nghĩa Proc của mỗi entity</param>
        /// <returns>FilterEntity<TEntity></returns>
        /// Create By: DDKhang (24/5/2023)
        [HttpGet("filter")]
        public virtual async Task<FilterEntity<TEntity>> EntitysFilter(int? pageSize, int? pageNumber, string? entityFilter="")
        {
            var entityFilterDto = await _baseService.EntitysFilterAsync(pageSize, pageNumber, entityFilter);
            return entityFilterDto;
        }
        
        /// <summary>
        /// - Thực hiện xóa Entity theo id
        /// </summary>
        /// <param name="id">Mã thực thể</param>
        /// <returns>Số lượng bản ghi đã xóa</returns>
        /// Created By: DDKhang (23/5/2023)
        [HttpDelete("delete")]
        public virtual async Task<int> DeleteAsync(Guid employeeId)
        {
            var result = await _baseService.DeleteAsync(employeeId);
            return result;
        }

        /// <summary>
        /// - Thực hiện cập nhật thông tin
        /// </summary>
        /// <param name="entity">Thông tin thực thể</param>
        /// <returns>Task<IActionResult></returns>
        /// Created By: DDKhang (23/5/2023)
        [HttpPut]
        public virtual async Task<IActionResult> UpdateAsync(TEntity entity)
        {
            return Ok(0);
        }
    }
}
