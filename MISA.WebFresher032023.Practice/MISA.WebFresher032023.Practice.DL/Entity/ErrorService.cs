namespace MISA.WebFresher032023.Practice.DL.Entity
{
    public class ErrorService
    {
        /// <summary>
        /// - Mã lỗi cho dev
        /// </summary>
        /// Created By: DDKhang (24/5/2023)
        public string DevMsg { get; set; }

        /// <summary>
        /// - Mã lỗi cho người dùng
        /// </summary>
        /// Created By: DDKhang (24/5/2023)
        public string UserMsg { get; set; } 

        /// <summary>
        /// - Mã lỗi
        /// </summary>
        /// Created By: DDKhang (24/5/2023)
        public string ErrorCode { get; set; }  
        
        /// <summary>
        /// - Thông tin thêm
        /// </summary>
        /// Created By: DDKhang (24/5/2023)
        public string MoreInfor { get; set; }

        /// <summary>
        /// - Dấu vết theo id
        /// </summary>
        /// Created By: DDKhang (24/5/2023)
        public string TraceId { get; set; }

        /// <summary>
        /// - Dữ liệu lỗi
        /// </summary>
        /// Created By: DDKhang (24/5/2023)
        public object Data { get; set; }
    }
}
