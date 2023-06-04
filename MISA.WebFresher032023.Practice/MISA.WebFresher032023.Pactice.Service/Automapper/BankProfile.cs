using AutoMapper;
using MISA.WebFresher032023.Pactice.BL.DTO.Banks;
using MISA.WebFresher032023.Practice.DL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Pactice.BL.Automapper
{
    public class BankProfile : Profile
    {
        public BankProfile()
        {
            CreateMap<Bank, BankDto>();
            CreateMap<BankDto, Bank>();
            CreateMap<FilterEntity<Bank>, FilterEntity<BankDto>>();
            CreateMap<BankCreateDto, Bank>();
            CreateMap<BankUpdateDto, Bank>();
        }
    }
}
