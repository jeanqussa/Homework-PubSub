using System;
using System.Collections.Generic;

namespace Homework.PubSub
{
    /// <summary>
    /// PubSub class.
    /// Implements a publish-subscribe pattern with topic support.
    /// </summary>
    /// <typeparam name="T">Type for handler arguments which is derived from <see cref="EventArgs"/>.</typeparam>
    /// <example>
    /// <code>
    /// var pubSub = new PubSub<EventArgs>();
    /// pubSub.Subscribe("topic-1", (sender, e) => Console.WriteLine("Received event on topic-1"));
    /// pubSub.Subscribe("topic-2", (sender, e) => Console.WriteLine("Received event on topic-2"));
    /// pubSub.Publish("topic-1", EventArgs.Empty);
    /// </code>
    /// </example>
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
}