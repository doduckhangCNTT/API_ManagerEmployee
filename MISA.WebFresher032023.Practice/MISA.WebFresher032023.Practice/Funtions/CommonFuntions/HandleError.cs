using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher032023.Practice.Model;

namespace MISA.WebFresher032023.Practice.Funtions.CommonFuntions
{
    public class HandleError
    {
        public HandleError()
        {

        }

        /// <summary>
        /// - Xử lí các ngoại lệ
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <param name="contentErrorForUser">Nội dung lỗi</param>
        /// <returns></returns>
        public static Task<ErrorService> HandleExceptionError(Exception ex, string contentErrorForUser)
        {
            ErrorService error = new ErrorService();
            error.DevMsg = ex.Message;
            error.UserMsg = contentErrorForUser;
            error.TraceId = ex.StackTrace;
            error.MoreInfor = ex.HelpLink;
            error.Data = ex.Data;
            return Task.FromResult(error);
        }
    }
}
