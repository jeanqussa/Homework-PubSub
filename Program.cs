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

    public class Publisher<T> where T : EventArgs {
        public event EventHandler<T> DataReceived;

        public virtual void OnDataReceived(T args) {
            if (DataReceived != null) {
                DataReceived(this, args);
            }
        }
    }

    public class PubSub<T> where T : EventArgs {
        private Publisher<T> pub = new Publisher<T>();

        public void Subscribe(string city, EventHandler<T> handler) {
            pub.DataReceived += handler;
        }

        public void Unsubscribe(string city, EventHandler<T> handler) {
            pub.DataReceived -= handler;
        }

        public void Publish(string city, T args) {
            pub.OnDataReceived(args);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var weatherPubSub = new PubSub<WeatherEventArgs>();

            weatherPubSub.Subscribe("Damascus", (sender, e) => Console.WriteLine("Temperature is " + e.Temperature));

            weatherPubSub.Publish("Damascus", new WeatherEventArgs(10.0, 14.3));
            weatherPubSub.Publish("Damascus", new WeatherEventArgs(12.2, 5.0));
        }
    }
}
