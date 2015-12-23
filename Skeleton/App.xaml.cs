namespace Wacton.Skeleton
{
    using System;
    using System.Reflection;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Threading;

    using Wacton.Skeleton.UI.Mains;
    using Wacton.Tovarisch.Logging;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// the application entry point
        /// use the opportunity the setup error loggers and the window title
        /// </summary>
        public App()
        {
            Application.Current.DispatcherUnhandledException += OnDispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += OnDomainUnhandledException;
            TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;
            SetWindowTitle();
        }

        private static void SetWindowTitle()
        {
            var assemblyName = Assembly.GetExecutingAssembly().GetName();
            ShellViewModel.WindowTitle = $"{assemblyName.Name} ({assemblyName.Version})";
            BasicShellViewModel.WindowTitle = $"{assemblyName.Name} ({assemblyName.Version})";
        }

        private static void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Logger.Default.Error(e.Exception, "Unhandled exception (dispatcher)");
        }

        private static void OnDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Logger.Default.Error((Exception)e.ExceptionObject, "Unhandled exception (domain)");
        }

        private static void OnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            Logger.Default.Error(e.Exception, "Unhandled exception (unobserved task)");
        }
    }
}
