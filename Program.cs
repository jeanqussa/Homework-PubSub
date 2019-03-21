using System;
using System.Collections.Generic;

namespace Homework.PubSub
{
    /// <summary>
    /// Weather event args class.
    /// Stores information about weather conditions in a specific place.
    /// </summary>
    public class WeatherEventArgs : EventArgs {
        public double Temperature { get; set; }
        public double Humidity { get; set; }

        /// <summary>
        /// Creates a new instance of the <see cref="WeatherEventArgs"/> class.
        /// </summary>
        public WeatherEventArgs(double temperature, double humidity) {
            Temperature = temperature;
            Humidity = humidity;
        }
    }

    /// <summary>
    /// Publisher class.
    /// Provides a mechanism for handling events.
    /// </summary>
    /// <typeparam name="T">Type for handler arguments which is derived from <see cref="EventArgs"/>.</typeparam>
    public class Publisher<T> where T : EventArgs {
        /// <value>Event handler which is called when we have received data.</value>
        public event EventHandler<T> DataReceived;

        /// <summary>
        /// Calls all registered handler functions with specified <c>args</c>.
        /// </summary>
        /// <param name="args">The args with which to call the event handler.</param>
        public virtual void OnDataReceived(T args) {
            if (DataReceived != null) {
                DataReceived(this, args);
            }
        }

        /// <summary>
        /// Checks whether <c>DataReceived</c> has an empty invocation list.
        /// </summary>
        /// <returns>Whether <c>DataReceived</c> has an empty invocation list.</returns>
        public bool IsInvocationListEmpty() {
            return DataReceived.GetInvocationList().Length == 0;
        }
    }

    /// <summary>
    /// PubSub class.
    /// Implements a publish-subscribe pattern with topics.
    /// </summary>
    /// <typeparam name="T">Type for handler arguments which is derived from <see cref="EventArgs"/>.</typeparam>
    public class PubSub<T> where T : EventArgs {
        /// <value>Dictionary of topic -> publisher mappings.</value>
        private Dictionary<string, Publisher<T>> Publishers = new Dictionary<string, Publisher<T>>();

        /// <summary>
        /// Subscribes a handler to a topic.
        /// </summary>
        /// <param name="topic">The topic to which to subscribe as string.</param>
        /// <param name="handler">The event handler function.</param>
        public void Subscribe(string topic, EventHandler<T> handler) {
            // Check if publisher exists. If not, create it and add it to
            // Publishers
            if (!Publishers.ContainsKey(topic)) {
                Publishers.Add(topic, new Publisher<T>());
            }

            // Add handler to publisher's DataReceived event handler
            Publishers[topic].DataReceived += handler;
        }

        /// <summary>
        /// Unsubscribes a handler from a topic.
        /// </summary>
        /// <param name="topic">The topic from which to unsubscribe as string.</param>
        /// <param name="handler">The event handler function.</param>
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

        /// <summary>
        /// Publishes <c>args</c> to a topic.
        /// </summary>
        /// <param name="topic">The topic to which to publish as string.</param>
        /// <param name="args">The args to publish.</param>
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
