using System;

namespace Homework.PubSub
{
    class Program
    {
        static void Main(string[] args)
        {
            var weatherPubSub = new PubSub<WeatherEventArgs>();

            weatherPubSub.Subscribe("Damascus", (sender, e) => Console.WriteLine("Temperature in Damascus is " + e.Temperature));
            weatherPubSub.Subscribe("Homs", (sender, e) => Console.WriteLine("Temperature in Homs is " + e.Temperature));

            weatherPubSub.Publish("Damascus", new WeatherEventArgs(10.0, 14.3));
            weatherPubSub.Publish("Homs", new WeatherEventArgs(12.2, 5.0));
        }
    }
}
