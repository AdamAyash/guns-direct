using Microsoft.AspNetCore.Mvc;

namespace WebAPIGateway.Services.Base
{
    public class BaseServerResponse<ResultData>
    {
        public DateTime DateTimeStamp { get; set; }
        public bool IsSuccessful { get; set; }
        public bool IsCacheData { get; set; }
        public ResultData Result { get; set; }

        public BaseServerResponse()
        {
        }
    }
}
