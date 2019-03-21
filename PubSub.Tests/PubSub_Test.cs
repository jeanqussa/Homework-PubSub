using System;
using Xunit;
using Homework.PubSub;

namespace Homework.PubSub.Tests
{
    public class PubSub_Test
    {
        private PubSub<WeatherEventArgs> pubSub;

        public PubSub_Test() {
            pubSub = new PubSub<WeatherEventArgs>();
        }

        [Fact]
        public void EverythingWorks()
        {
            int topicOne = 0;
            int topicTwo = 0;
            double sumTemperature = 0.0;
            double sumHumidity = 0.0;

            EventHandler<WeatherEventArgs> ev1 = (sender, e) => {
                topicOne++;
                sumTemperature += e.Temperature;
                sumHumidity += e.Humidity;
            };
            EventHandler<WeatherEventArgs> ev2 = (sender, e) => {
                topicTwo++;
                sumTemperature += e.Temperature;
                sumHumidity += e.Humidity;
            };
            
            // First pass:
            // ev1 is subscribed to topicOne
            // ev2 is subscribed to topicTwo

            pubSub.Subscribe("topicOne", ev1);
            pubSub.Subscribe("topicTwo", ev2);

            pubSub.Publish("topicOne", new WeatherEventArgs(1.0, 5.5));
            pubSub.Publish("topicTwo", new WeatherEventArgs(2.5, 3.1));

            Assert.Equal(1, topicOne);
            Assert.Equal(1, topicTwo);
            Assert.Equal(3.5, sumTemperature);
            Assert.Equal(8.6, sumHumidity);

            // Second pass:
            // ev2 is subscribed to topicOne
            // ev2 is subscribed to topicTwo

            pubSub.Subscribe("topicOne", ev2);
            pubSub.Unsubscribe("topicOne", ev1);
            pubSub.Publish("topicOne", new WeatherEventArgs(1.0, 5.5));
            pubSub.Publish("topicTwo", new WeatherEventArgs(2.5, 3.1));

            Assert.Equal(1, topicOne);
            Assert.Equal(3, topicTwo);
            Assert.Equal(7.0, sumTemperature);
            Assert.Equal(17.2, sumHumidity);
        }
    }
}
