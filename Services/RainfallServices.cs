
using FlexiSourceExam.Interface;
using FlexiSourceExam.Model;
using System.Net;

namespace FlexiSourceExam.Services
{
    public class RainfallServices : IRainfall
    {
        public async Task<APIResponse> StationReading (string id, int count)
        {
            APIResponse result = new APIResponse ();

            using (var wb = new WebClient ())
            {
                string url = "";
            }
            return result;
        }

    }
}
