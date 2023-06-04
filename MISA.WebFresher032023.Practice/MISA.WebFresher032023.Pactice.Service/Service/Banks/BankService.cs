using AutoMapper;
using MISA.WebFresher032023.Pactice.BL.DTO.Banks;
using MISA.WebFresher032023.Pactice.BL.Service.Bases;
using MISA.WebFresher032023.Practice.DL.Entity;
using MISA.WebFresher032023.Practice.DL.Repository.Banks;
using MISA.WebFresher032023.Practice.DL.Repository.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Pactice.BL.Service.Banks
{
    public class BankService : BaseService<Bank, BankDto, BankUpdateDto, BankCreateDto>, IBankService
    {
        public BankService(IBankRepository bankRepository, IMapper mapper) : base(bankRepository, mapper)
        {
        }
    }
}
