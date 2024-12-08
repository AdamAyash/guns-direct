namespace WebAPIGateway.Services.Base
{
    public class BaseServerResponse<ResultData>
    {
        public DateTime Date { get; set; }
        public bool IsSuccessful { get; set; }
        public ResultData Result { get; set; }

        public BaseServerResponse()
        {
        }
    }
}
