using System;
using Xunit;
using Homework.PubSub;

namespace Homework.PubSub.Tests
{
    public class WeatherEventArgs_Test
    {
        [Fact]
        public void TemperatureAndHumidity()
        {
            var args = new WeatherEventArgs(5.0, 8.0);
            Assert.Equal(5.0, args.Temperature);
            Assert.Equal(8.0, args.Humidity);
        }
    }
}
