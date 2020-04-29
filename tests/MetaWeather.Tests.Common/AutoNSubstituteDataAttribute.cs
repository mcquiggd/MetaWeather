using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;

namespace MetaWeather.Tests.Common
{
    /// <summary>
    /// Provide an implementation of auto mocking to Xunit, using NSubstitute for auto generation of mock hierarchies of
    /// objects
    /// </summary>
    public class AutoNSubstituteDataAttribute : AutoDataAttribute
    {
        public AutoNSubstituteDataAttribute() : base(() =>
        {
            var fixture = new Fixture().Customize(new AutoNSubstituteCustomization { ConfigureMembers = true });

            return fixture;
        })
        { }
    }
}