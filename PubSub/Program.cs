using System;

namespace Homework.PubSub
{
    class Program
    {
        static void Main(string[] args)
        {
            // PubSub object for tracking weather data in multiple cities
            var weatherPubSub = new PubSub<WeatherEventArgs>();

            // We subscribe to the cities of "Damascus" and "Homs" and print the temperature or
            // humidity everytime a new WeatherEventArgs is published to one of these cities
            weatherPubSub.Subscribe("Damascus", (sender, e) => Console.WriteLine("Temperature in Damascus is " + e.Temperature));
            weatherPubSub.Subscribe("Homs", (sender, e) => Console.WriteLine("Humidity in Homs is " + e.Humidity));

            // We publish one WeatherEventArgs to each of "Damascus" and "Homs"
            weatherPubSub.Publish("Damascus", new WeatherEventArgs(10.0, 61.7));
            weatherPubSub.Publish("Homs", new WeatherEventArgs(12.2, 51.5));

            // Prints:
            //  Temperature in Damascus is 10
            //  Humidity in Homs is 51.5
        }
    }
}
