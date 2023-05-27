using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Practice.DL.Entity
{
    public class FilterEntity<TEntity>
    {
        /// <summary>
        /// - Tổng số trang
        /// </summary>
        /// Created By: DDKhang (24/5/2023)
        public int TotalPage { get; set; }

        /// <summary>
        /// - Tổng số bản ghi
        /// </summary>
        /// Created By: DDKhang (24/5/2023)
        public int TotalRecord { get; set; }

        /// <summary>
        /// - Trang hiện tại
        /// </summary>
        /// Created By: DDKhang (24/5/2023)
        public int CurrentPage { get; set; }

        /// <summary>
        /// - Tổng số bản ghi trên trang
        /// </summary>
        /// Created By: DDKhang (24/5/2023)
        public int CurrentPageRecords { get; set; }

        /// <summary>
        /// - Dữ liệu trên trang
        /// </summary>
        /// Created By: DDKhang (24/5/2023)
        public IEnumerable<TEntity> Data { get; set; }
    }
}
