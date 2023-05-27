using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher032023.Practice.Common.Enum
{
    public enum HttpStatusCodeEnum
    {
        Success = 200,
        CreatedSuccess = 201,
        NoContent = 204,
        BadRequest = 400,
        NoAuthentication = 401,
        ServerError = 500,
    }
}
