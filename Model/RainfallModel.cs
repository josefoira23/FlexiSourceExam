using Newtonsoft.Json;
using System.Net;

namespace FlexiSourceExam.Model
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string description { get; set; }
        public object? content { get; set; }
    }



    public class stationReading
    {
        [JsonProperty("@context")]
        public string @context { get; set; }
        public meta meta { get; set; }
        public List<stationItem> items { get; set; }
    }


    public class meta
    {
        public string publisher { get; set; }
        public string licence { get; set; }
        public string documentation { get; set; }
        public decimal version { get; set; }
        public string comment { get; set; }
        public List<string> hasFormat { get; set; }
        public int limit { get; set; }


    }
    public class stationItem
    {
        [JsonProperty("@id")]
        public string @id { get; set; }
        public DateTime? datetime { get; set; }
        public string measure { get; set; }
        public decimal value { get; set; }

    }
}
