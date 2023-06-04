using AutoMapper;
using MISA.WebFresher032023.Practice.Common.Enum;
using MISA.WebFresher032023.Practice.Common.Exception;
using MISA.WebFresher032023.Practice.Common.Resources;
using MISA.WebFresher032023.Practice.DL.Entity;
using MISA.WebFresher032023.Practice.DL.Repository.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Pactice.BL.Service.Bases
{
    /// <summary>
    /// - Tầng dịch vụ chung, sử dụng để xử lí logic gọi lên tầng repository
    /// </summary>
    /// <typeparam name="TEntity">Lớp thực thể</typeparam>
    /// <typeparam name="TEntityDto">Lớp thực thể truyền tải</typeparam>
    /// <typeparam name="TEntityUpdateDto">Lớp thực thể cập nhật truyền tải</typeparam>
    /// <typeparam name="TEntityCreateDto">Lớp thực thể tạo truyền tải</typeparam>
    /// CreatedBy: DDKhang (24/5/2023)
    public abstract class BaseService<TEntity, TEntityDto, TEntityUpdateDto, TEntityCreateDto> : IBaseService<TEntityDto, TEntityUpdateDto, TEntityCreateDto>
    {
        #region Field
        // Khai báo đối tượng DL
        protected readonly IBaseRepository<TEntity> _baseRepository;
        protected readonly IMapper _mapper;
        #endregion

        #region Constructor
        public BaseService(IBaseRepository<TEntity> baseRepository, IMapper mapper)
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
        public virtual async Task<int> CreateAsync(TEntityCreateDto entity)
        {
            // - Kiểm tra các dữ liệu khác có đúng định dạng không
            var entityCreate = _mapper.Map<TEntity>(entity);
            int qualityRecordAdd = await _baseRepository.CreateAsync(entityCreate);
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
        /// <returns>FilterEntity<TEntity></returns>
        /// Create By: DDKhang (24/5/2023)
        public virtual async Task<FilterEntity<TEntityDto>> EntitysFilterAsync(int? pageSize, int? pageNumber, string? entityFilter)
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
            FilterEntity<TEntity> entities = await _baseRepository.EntityFilterAsync(pageSize, pageNumber, entityFilter, skip);

            // Ánh xạ các trường -> Dto
            FilterEntity<TEntityDto> entitiesDto = _mapper.Map<FilterEntity<TEntityDto>>(entities);

            // Trả thông tin đã lọc
            return entitiesDto;
        }

        /// <summary>
        /// - Thực hiện lấy thông tin enitty theo id
        /// </summary>
        /// <param name="id">Mã Entity</param>
        /// <returns>TEntityDto</returns>
        /// <exception cref="InternalException">Middleware</exception>
        /// Create By: DDKhang (24/5/2023)
        public virtual async Task<TEntityDto?> GetAsync(Guid id)
        {
            var entity = await _baseRepository.GetAsync(id);
            if (entity == null)
            {
                // Middleware
                //throw new InternalException(ResourceVN.Validate_NotFoundAssests);
                throw new InternalException(ResourceVN.Validate_NotFoundAssests);
                //return default;
            }

            /*
                - Tự động ánh xạ các thuộc tính từ entity -> entityDTO
             */
            var entityDto = _mapper.Map<TEntityDto>(entity);
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
        public virtual async Task<int> UpdateAsync(Guid entityId, TEntityUpdateDto entityDto)
        {
            // Số bản ghi ảnh hưởng
            int qualityRecordUpdate = 0;
            // Tên bảng
            string tableName = typeof(TEntity).Name;

            // Kiểm tra thực thể null -> lấy ra dữ liệu thực thể cũ
            TEntityDto entityOld = await GetAsync(entityId);
            if (entityOld == null) throw new NotFoundException(ResourceVN.Validate_NotFoundAssests);

            // 1. Sử dụng reflection để đặt giá trị cho các thuộc tính của TEntity lấy các thuộc tính của entity
            // 1.1 Lấy các thuộc tính ban đầu
            PropertyInfo[] properties = typeof(TEntityDto).GetProperties();
            // 1.2 Lấy các thuộc tính muốn update
            PropertyInfo[] propertiesNew = typeof(TEntityUpdateDto).GetProperties();

            // 2. Tạo đối tượng chứa dữ liệu mới và cũ cho entity
            TEntityDto entityObject = Activator.CreateInstance<TEntityDto>();
            // 3. Thực hiện cập nhật các dữ liệu mới và cũ vào bên trong đối tượng
            for (int i = 0; i < properties.Length; i++)
            {
                PropertyInfo property = properties[i];
                string test = property.Name;
                // Thuộc tính GenderName thì không nằm trong thuộc tính của entity
                if (property.Name != "GenderName")
                {
                    object valueOld = property.GetValue(entityOld);

                    for (int j = 0; j < propertiesNew.Length; j++)
                    {
                        PropertyInfo propertyNew = propertiesNew[j];
                        object valueNew = propertyNew.GetValue(entityDto);

                        // Kiểm tra trùng lặp thuộc tính cũ và mới
                        if (property.Name == propertyNew.Name)
                        {
                            // Xét giá trị mới nếu trùng tên thuộc tính
                            property.SetValue(entityObject, valueNew);
                            break;
                        }
                        else
                        {
                            // Xét giá trị cx nếu không trùng lặp thuộc tính
                            property.SetValue(entityObject, valueOld);

                        }
                    }
                }
            }

            if (entityObject != null)
            {
                // Ánh xạ TEntityUpdateDto -> TEntity
                var entityUpdate = _mapper.Map<TEntity>(entityObject);

                qualityRecordUpdate = await _baseRepository.UpdateAsync(entityUpdate);
                return qualityRecordUpdate;
            }
            return qualityRecordUpdate;
        }

        /// <summary>
        /// - Kiểm tra thực thể có tồn tại 
        /// </summary>
        /// <param name="entityId">Mã thực thể</param>
        /// <returns>TEntityDto</returns>
        /// CreatedBy: DDKhang (27/5/2023)
        public virtual async Task<TEntityDto> CheckEntityExist(Guid entityId)
        {
            TEntity entity = await _baseRepository.CheckEntityExist(entityId);

            var entityDto = _mapper.Map<TEntityDto>(entity);
            return entityDto;
        }

        /// <summary>
        /// - Xóa nhiều bản ghi
        /// </summary>
        /// <param name="listEntityId">Danh sách mã bản ghi được nối bằng ","</param>
        /// <returns>Số bản ghi được xóa</returns>
        /// CreatedBy: DDKhang (27/5/2023)
        public virtual async Task<int> DeleteMutilEntityAsync(string listEntityId)
        {
            int result = await _baseRepository.DeleteMutilEntityAsync(listEntityId);
            return result;
        }
    }
}
