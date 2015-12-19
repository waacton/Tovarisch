namespace Wacton.Tovarisch.Delegates
{
    using System;
    using System.Threading.Tasks;

    public static class DelegateExtensions
    {
        public static async void InvokeAfterDelay(this Action action, TimeSpan delay)
        {
            await Task.Delay(delay);
            action();
        }

        public static async void InvokeAfterDelay<T>(this Action<T> action, T parameter, TimeSpan delay)
        {
            await Task.Delay(delay);
            action(parameter);
        }
    }
}
