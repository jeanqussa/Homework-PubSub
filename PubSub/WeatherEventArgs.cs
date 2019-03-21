using System;

namespace Homework.PubSub
{
    /// <summary>
    /// WeatherEventArgs class.
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
}
