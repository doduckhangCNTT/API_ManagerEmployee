using Microsoft.Extensions.Configuration;
using MISA.WebFresher032023.Practice.DL.Entity;
using MISA.WebFresher032023.Practice.DL.Repository.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Practice.DL.Repository.Banks
{
    /// <summary>
    /// - Lớp base(chung) chứa các phương thức cơ bản, để thao tác với db
    /// </summary>
    /// CreatedBy: DDKhang (23/5/2023) 
    public class BankRepository : BaseRepository<Bank>, IBankRepository
    {
        #region Constructor
        public BankRepository(IConfiguration configuration) : base(configuration)
        {
        } 
        #endregion
    }
}
