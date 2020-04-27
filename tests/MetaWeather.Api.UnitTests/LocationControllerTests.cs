using System.Threading.Tasks;

using MetaWeather.Api.Controllers;
using MetaWeather.Core.Entities;
using MetaWeather.Core.Interfaces;
using MetaWeather.Tests.Common;

using Microsoft.AspNetCore.Mvc;

using NSubstitute;

using Xunit;

namespace MetaWeather.Api.UnitTests
{
    public class LocationControllerTests

    {
        IApiProxy _apiProxy;
        readonly LocationController _locationController;
        TestLocationResponseBuilder _locationResponseBuilder;
        TestWeatherResponseBuilder _weatherBuilder;

        public LocationControllerTests()
        {
            _apiProxy = Substitute.For<IApiProxy>();
            _locationResponseBuilder = new TestLocationResponseBuilder();

            _locationController = new LocationController(_apiProxy);
        }

        [Fact]
        public async Task LocationController_GetWithValidWoeId_ReturnsValidLocationAsync()
        {
            //Arrange
            _apiProxy.SubmitLocationRequest(Arg.Any<ILocationRequest>())
                .Returns(_locationResponseBuilder.Default().WithBelfast().Build());

            //Act
            var result = await _locationController.Get(new LocationRequest());

            //Assert
            Assert.IsAssignableFrom<ActionResult<ILocationResponse>>(result);
        }
    }
}
