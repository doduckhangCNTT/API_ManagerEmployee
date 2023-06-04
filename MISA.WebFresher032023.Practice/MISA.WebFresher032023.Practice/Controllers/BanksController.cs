using Microsoft.AspNetCore.Components;
using MISA.WebFresher032023.Pactice.BL.DTO.Banks;
using MISA.WebFresher032023.Pactice.BL.Service.Banks;
using MISA.WebFresher032023.Pactice.BL.Service.Bases;
using MISA.WebFresher032023.Practice.DL.Entity;

namespace MISA.WebFresher032023.Practice.Controllers
{
    [Route("api/v1/[controller]")]
    public class BanksController : BaseController<Bank, BankDto, BankUpdateDto, BankCreateDto>
    {
        public BanksController(IConfiguration configuration, IBankService bankService) : base(bankService)
        {
        }
    }
}
