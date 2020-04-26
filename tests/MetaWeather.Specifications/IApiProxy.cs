using System;
using System.Threading.Tasks;

namespace MetaWeather.Specifications
{
    public class IApiProxy
    {
        public IApiProxy() { }

        public Task<ILocationResponse> SubmitLocationRequest(ILocationRequest locationRequest) => throw new NotImplementedException();
    }
}
