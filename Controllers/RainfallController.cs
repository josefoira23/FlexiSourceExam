using FlexiSourceExam.Interface;
using FlexiSourceExam.Model;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FlexiSourceExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RainfallController : ControllerBase
    {
        private IRainfall rainfall { get; set; }
        public RainfallController(IRainfall _rainfall) {
            rainfall = _rainfall;
        }

        [HttpGet("id/{stationId}/readings")]
        public async Task<APIResponse> readings(string stationId, int count = 10)
        {
            APIResponse response = new APIResponse();

            if (count < 1 || count >100)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.description = "Parameter count is out of range. Please input 1-100 only.";
                return response;
            }

            try
            {
                response = await rainfall.StationReading(stationId, count);

            }catch (Exception ex)
            {
                if (ex.Message.Contains("404"))
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.description = "No readings found for the specified stationId";
                }
                else if (ex.Message.Contains("400"))
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.description = "Invalid Request";
                }
                else
                {

                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.description = "Internal Server Error";
                }
            }


            return response;
        }
    }
}
