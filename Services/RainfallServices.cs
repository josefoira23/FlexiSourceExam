
using FlexiSourceExam.Interface;
using FlexiSourceExam.Model;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;

namespace FlexiSourceExam.Services
{
    public class RainfallServices : IRainfall
    {
        private string _url;
        public RainfallServices(IOptions<RainfallSettings> settings) 
        {
            _url = settings.Value.root_url;    
        }
            
        public async Task<APIResponse> StationReading (string stationId, int count)
        {
            APIResponse result = new APIResponse ();
            stationReading reading = new stationReading ();
            try
            {

                using (var wb = new WebClient())
                {
                    string url = _url + "id/stations/" + stationId + "/readings?_limit=" + count;

                    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                    request.Method = "GET";
                    string returnstring = string.Empty;

                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        Stream stream = response.GetResponseStream();
                        StreamReader sr = new StreamReader(stream);
                        returnstring = sr.ReadToEnd();
                        reading = JsonConvert.DeserializeObject<stationReading>(returnstring);
                        sr.Close();
                        stream.Dispose();

                    }

                }

                if (reading.items.Count == 0)
                {
                    result.StatusCode = HttpStatusCode.NotFound;
                    result.description = "No readings found for the specified stationId";
                    result.content = "";
                }
                else
                {

                    result.StatusCode = HttpStatusCode.OK;
                    result.description = "A list of rainfall readings successfully retrieved.";
                    result.content = reading;

                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("404"))
                {
                    result.StatusCode = HttpStatusCode.NotFound;
                    result.description = "No readings found for the specified stationId";
                }
                else if (ex.Message.Contains("400"))
                {
                    result.StatusCode = HttpStatusCode.BadRequest;
                    result.description = "Invalid Request";
                }
                else
                {

                    result.StatusCode = HttpStatusCode.InternalServerError;
                    result.description = "Internal Server Error";
                }

                result.content = ex.ToString();
            }

            return result;
        }

    }
}
