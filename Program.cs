using System;

namespace Homework.PubSub
{
    public class WeatherEventArgs : EventArgs {
        public double Temperature { get; set; }
        public double Humidity { get; set; }

        public WeatherEventArgs(double temperature, double humidity) {
            Temperature = temperature;
            Humidity = humidity;
        }
    }

    public class WeatherPublisher {
        public event EventHandler<WeatherEventArgs> WeatherDataReceived;

        public virtual void OnWeatherDataReceived(WeatherEventArgs args) {
            if (WeatherDataReceived != null) {
                WeatherDataReceived(this, args);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            WeatherPublisher weatherPub = new WeatherPublisher();

            weatherPub.WeatherDataReceived += (sender, e) => Console.WriteLine("Temperature is " + e.Temperature);

            weatherPub.OnWeatherDataReceived(new WeatherEventArgs(10.0, 14.3));
            weatherPub.OnWeatherDataReceived(new WeatherEventArgs(12.2, 5.0));
        }
    }
}
