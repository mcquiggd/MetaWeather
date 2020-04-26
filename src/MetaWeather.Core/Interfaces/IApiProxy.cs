using System.Threading.Tasks;

namespace MetaWeather.Core.Interfaces
{
    public interface IApiProxy
    {
        Task<ILocationResponse> SubmitLocationRequest(ILocationRequest locationRequest);
    }
}