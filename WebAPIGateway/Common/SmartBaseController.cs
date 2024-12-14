namespace WebAPIGateway.Common
{
    using Microsoft.AspNetCore.Mvc;
    using WebAPIGateway.Services.Base;

    public class SmartBaseController : ControllerBase
    {
        public SmartBaseController()
        {
        }

        protected BaseServerResponse<ResultType> GenerateAPIResponse<ResultType>(bool isSuccessful, ResultType result)
        {
            BaseServerResponse<ResultType> baseServerResponse = new BaseServerResponse<ResultType>();
            baseServerResponse.DateTimeStamp = DateTime.Now;
            baseServerResponse.IsSuccessful = isSuccessful;
            baseServerResponse.Result = result;

            return baseServerResponse;
        }
    }
}
