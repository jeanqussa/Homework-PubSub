using System;
using System.Collections.Generic;

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

        public bool IsInvocationListEmpty() {
            return DataReceived.GetInvocationList().Length == 0;
        }
    }

    public class PubSub<T> where T : EventArgs {
        private Dictionary<string, Publisher<T>> Publishers = new Dictionary<string, Publisher<T>>();

        public void Subscribe(string topic, EventHandler<T> handler) {
            // Check if publisher exists. If not, create it and add it to
            // Publishers
            if (!Publishers.ContainsKey(topic)) {
                Publishers.Add(topic, new Publisher<T>());
            }

            // Add handler to publisher's DataReceived event handler
            Publishers[topic].DataReceived += handler;
        }

        public void Unsubscribe(string topic, EventHandler<T> handler) {
            var publisher = Publishers[topic];

            // Remove handler from publisher's DataReceived event handler
            publisher.DataReceived -= handler;

            // If publisher's DataReceived event has an empty invocation list,
            // remove publisher
            if (publisher.IsInvocationListEmpty()) {
                Publishers.Remove(topic);
            }
        }

        public void Publish(string topic, T args) {
            // Publish event only if the publisher for the intended topic exists
            // in Publishers
            if (Publishers.ContainsKey(topic)) {
                Publishers[topic].OnDataReceived(args);
            }
        }
    }

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
