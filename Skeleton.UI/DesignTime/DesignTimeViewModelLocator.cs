namespace Wacton.Skeleton.UI.DesignTime
{
    using Ninject;

    using Wacton.Skeleton.UI.Mains;

    /// <summary>
    /// provides view models for views during design time
    /// </summary>
    public class DesignTimeViewModelLocator
    {
        private static readonly IKernel Kernel;

        public static ShellViewModel ShellViewModel => Kernel.Get<ShellViewModel>();
        public static MainViewModel MainViewModel => ShellViewModel.MainViewModel;

        static DesignTimeViewModelLocator()
        {
            if (Kernel != null)
            {
                return;
            }

            Kernel = new StandardKernel();
            AppBootstrapper.SetupKernelBindingsForDesignTime(Kernel);

            SetupDesignTimeEnvironment();
        }

        private static void SetupDesignTimeEnvironment()
        {
            // setup model for design time data (if required)
        }
    }
}
