using System;
using Xunit;
using Homework.PubSub;

namespace Homework.PubSub.Tests
{
    public class Publisher_Test
    {
        private Publisher<WeatherEventArgs> publisher;

        public Publisher_Test() {
            publisher = new Publisher<WeatherEventArgs>();
        }

        [Fact]
        public void InvocationListEmpty()
        {
            Assert.True(publisher.IsInvocationListEmpty());
        }

        [Fact]
        public void PublisherAddedAndRemoved()
        {
            EventHandler<WeatherEventArgs> ev = (sender, e) => {};

            publisher.DataReceived += ev;
            Assert.False(publisher.IsInvocationListEmpty());

            publisher.DataReceived -= ev;
            Assert.True(publisher.IsInvocationListEmpty());
        }

        [Fact]
        public void CounterIncrementedFourTimes()
        {
            int timesCalled = 0;
            double sumTemperature = 0.0;
            double sumHumidity = 0.0;

            EventHandler<WeatherEventArgs> ev = (sender, e) => {
                timesCalled++;
                sumTemperature += e.Temperature;
                sumHumidity += e.Humidity;
            };

            publisher.DataReceived += ev;
            publisher.DataReceived += ev;

            publisher.OnDataReceived(new WeatherEventArgs(1.0, 2.0));
            publisher.OnDataReceived(new WeatherEventArgs(1.0, 2.0));

            Assert.Equal(4, timesCalled);
            Assert.Equal(4.0, sumTemperature);
            Assert.Equal(8.0, sumHumidity);
        }
    }
}
