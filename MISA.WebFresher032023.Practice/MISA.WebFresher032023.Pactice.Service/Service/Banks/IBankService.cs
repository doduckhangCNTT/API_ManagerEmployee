using MISA.WebFresher032023.Pactice.BL.DTO.Banks;
using MISA.WebFresher032023.Pactice.BL.Service.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Pactice.BL.Service.Banks
{
    /// <summary>
    /// - Interface ngân hàng
    /// </summary>
    /// CreatedBy: DDKhang (3/6/2023)
    public interface IBankService : IBaseService<BankDto, BankUpdateDto, BankCreateDto>
    {
        // Nếu có các phương thức xử lí riêng khác thì thêm vào đây
    }
}
