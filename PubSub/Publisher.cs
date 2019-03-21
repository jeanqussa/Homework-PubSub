using System;

namespace Homework.PubSub
{
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
}