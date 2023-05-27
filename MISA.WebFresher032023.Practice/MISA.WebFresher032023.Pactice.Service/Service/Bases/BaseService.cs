using AutoMapper;
using MISA.WebFresher032023.Practice.Common.Enum;
using MISA.WebFresher032023.Practice.Common.Exception;
using MISA.WebFresher032023.Practice.Common.Resources;
using MISA.WebFresher032023.Practice.DL.Entity;
using MISA.WebFresher032023.Practice.DL.Repository.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Pactice.BL.Service.Bases
{
    public abstract class BaseService<IEntity, IEntityDto, TEntityUpdateDto> : IBaseService<IEntity, IEntityDto, TEntityUpdateDto>
    {
        #region Field
        // Khai báo đối tượng DL
        protected readonly IBaseRepository<IEntity> _baseRepository;
        protected readonly IMapper _mapper;
        #endregion

        #region Constructor
        public BaseService(IBaseRepository<IEntity> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }
        #endregion

        /// <summary>
        /// - Thực hiện thêm thông tin
        /// </summary>
        /// <param name="entity">Thông tin thực thể</param>
        /// <returns>Số bản ghi được thêm</returns>
        /// Create By: DDKhang (24/5/2023)
        public virtual async Task<int> CreateAsync(IEntity entity)
        {
            // Validate:
            // - Kiểm tra có trùng id

            // - Kiểm tra các dữ liệu khác có đúng định dạng không

            int qualityRecordAdd = await _baseRepository.CreateAsync(entity);
            return qualityRecordAdd;
        }

        /// <summary>
        /// - Thực hiện xóa entity theo id
        /// </summary>
        /// <param name="id">Mã entity</param>
        /// <returns>int - Số bản ghi bị xóa</returns>
        /// <exception cref="NotFoundException"></exception>
        /// Create By: DDKhang (24/5/2023)
        public virtual async Task<int> DeleteAsync(Guid id)
        {
            // Kiểm tra thực thể có tồn tại
            var entity = await _baseRepository.GetAsync(id);

            if (entity == null)
            {
                // Bắt lỗi thông qua Middleware
                throw new NotFoundException(ResourceVN.Validate_NotFoundAssests);
            }
            // Thực hiện xóa
            var result = await _baseRepository.DeleteAsync(id);

            // Trả về số bản ghi xóa
            return result;

        }

        /// <summary>
        /// - Thực hiện xóa
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        /// Create By: DDKhang (24/5/2023)
        public virtual async Task DeleteTaskAsync(Guid id)
        {
            // Kiểm tra thực thể có tồn tại
            var entity = await _baseRepository.GetAsync(id);

            if (entity == null)
            {
                // Bắt lỗi thông qua Middleware
                throw new NotFoundException("Không tìm thấy bản ghi");
            }

            //if(entity.Gender == GenderEnum.Female)
            //{
            //    throw new Exception("Không xóa phụ nữ");
            //}

            // Thực hiện xóa
            await _baseRepository.DeleteAsync(id);
        }


        /// <summary>
        /// - Thực lọc thông tin và phân trang
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="entityFilter"></param>
        /// <returns>FilterEntity<IEntity></returns>
        /// Create By: DDKhang (24/5/2023)
        public virtual async Task<FilterEntity<IEntity>> EntitysFilterAsync(int? pageSize, int? pageNumber, string? entityFilter)
        {
            // Khởi tạo giá trị ban đầu
            int skip = 0;
            if (pageNumber == null)
            {
                pageNumber = 1;
            }
            if (entityFilter == null)
            {
                entityFilter = "";
            }
            if (pageSize > 0 && pageNumber > 0)
            {
                skip = (int)((pageNumber - 1) * pageSize);
            }
            // Thực hiện lọc
            FilterEntity<IEntity> entities = await _baseRepository.EntityFilterAsync(pageSize, pageNumber, entityFilter, skip);
            // Trả thông tin đã lọc
            return entities;
        }

        /// <summary>
        /// - Thực hiện lấy thông tin enitty theo id
        /// </summary>
        /// <param name="id">Mã Entity</param>
        /// <returns>IEntityDto</returns>
        /// <exception cref="InternalException">Middleware</exception>
        /// Create By: DDKhang (24/5/2023)
        public virtual async Task<IEntityDto?> GetAsync(Guid id)
        {
            var entity = await _baseRepository.GetAsync(id);
            if (entity == null)
            {
                // Middleware
                //throw new InternalException(ResourceVN.Validate_NotFoundAssests);
                throw new InternalException("Khong tim thay nhung baor server loi");
                //return default;
            }

            /*
                - Tự động ánh xác các thuộc tính từ entity -> entityDTO
             */
            var entityDto = _mapper.Map<IEntityDto>(entity);
            return entityDto;
        }

        /// <summary>
        /// - Tạo mã code mới cho entity
        /// </summary>
        /// <returns>String</returns>
        public virtual async Task<string> NewEntityCode()
        {
            string newEntityCode = await _baseRepository.NewEntityCode();
            return newEntityCode;
        }

        /// <summary>
        /// - Thực hiện cập nhật thông tin thực thể
        /// </summary>
        /// <param name="entity">Thông tin thực thể mới</param>
        /// <returns>Số bản ghi được cập nhật</returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual async Task<int> UpdateAsync(IEntity entity)
        {
            int qualityRecordUpdate = await _baseRepository.UpdateAsync(entity);
            return qualityRecordUpdate;
        }
    }
}
