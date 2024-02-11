using FlexiSourceExam.Model;

namespace FlexiSourceExam.Interface
{
    public interface IRainfall
    {
         Task<APIResponse> StationReading(string stationid, int count);
    }
}
