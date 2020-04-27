using System.Threading.Tasks;

using MetaWeather.Api.Controllers;
using MetaWeather.Core.Entities;
using MetaWeather.Core.Interfaces;

using Microsoft.AspNetCore.Mvc;

using NSubstitute;

using Xunit;

namespace MetaWeather.Api.UnitTests
{
    public class WeatherControllerTests

    {
        IApiProxy _apiProxy;
        readonly WeatherController _weatherController;
        TestWeatherResponseBuilder _weatherResponseBuiler;

        public WeatherControllerTests()
        {
            _apiProxy = Substitute.For<IApiProxy>();
            _weatherResponseBuiler = new TestWeatherResponseBuilder();

            _weatherController = new WeatherController(_apiProxy);
        }

        [Fact]
        public async Task WeatherController_GetWithValidWoeId_ReturnsValidWeatherResponse()
        {
            //Arrange
            _apiProxy.SubmitWeatherRequest(Arg.Any<IWeatherRequest>())
                .Returns(_weatherResponseBuiler.Default().WithBelfast().Build());

            //Act
            var result = await _weatherController.Post(new WeatherRequest());

            //Assert
            Assert.IsAssignableFrom<ActionResult<IWeatherResponse>>(result);
        }
    }
}
