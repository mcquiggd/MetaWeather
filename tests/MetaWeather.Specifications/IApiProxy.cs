using System.Threading.Tasks;

namespace MetaWeather.Specifications
{
    public interface IApiProxy
    {
        Task<ILocationResponse> SubmitLocationRequest(ILocationRequest locationRequest);
    }
}
