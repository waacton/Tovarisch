namespace Wacton.Skeleton.UI
{
    using Ninject;

    using Wacton.Skeleton.Domain.DesignTime;
    using Wacton.Skeleton.Domain.Mains;
    using Wacton.Skeleton.UI.Mains;
    using Wacton.Tovarisch.Lexicon;
    using Wacton.Tovarisch.MVVM;

    /// <summary>
    /// the bootstrapper base specifies which view model to use as the root
    /// caliburn will locate the associated view, bind with view model, and display it
    /// (requires the View/ViewModel naming conventions)
    /// ---
    /// change between ShellViewModel and BasicShellViewModel to see with and without MahApps
    /// </summary>
    public class AppBootstrapper : Bootstrapper<ShellViewModel>
    {
        protected override void ConfigureApplication(IKernel kernel)
        {
            SetupKernelBindings(kernel);
        }

        /// <summary>
        /// specify which interface implementations to use at runtime
        /// only really need to singleton bind them if injected into more than once place
        /// </summary>
        public static void SetupKernelBindings(IKernel kernel)
        {
            kernel.Bind<IWordRepository>().To<WordRepository>().InSingletonScope();
            kernel.Bind<Main>().ToSelf().InSingletonScope();
        }

        /// <summary>
        /// specify which interface implementations to use at design time
        /// probably best to build runtime bindings and rebind where appropriate
        /// </summary>
        public static void SetupKernelBindingsForDesignTime(IKernel kernel)
        {
            SetupKernelBindings(kernel);
            kernel.Rebind<IWordRepository>().To<DesignTimeWordRepository>().InSingletonScope();
        }
    }
}
