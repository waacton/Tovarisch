namespace Wacton.Tovarisch.MVVM
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Windows;

    using Caliburn.Micro;

    using Ninject;

    public abstract class Bootstrapper<T> : BootstrapperBase
    {
        protected IKernel Kernel;

        protected Bootstrapper()
        {
            this.Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            this.DisplayRootViewFor<T>();
        }

        protected override void Configure()
        {
            this.Kernel = new StandardKernel();

            this.Kernel.Bind<IWindowManager>().To<WindowManager>().InSingletonScope();
            this.Kernel.Bind<IEventAggregator>().To<EventAggregator>().InSingletonScope();

            this.Kernel.Bind<CommandInvoker>().ToSelf().InSingletonScope();

            this.ConfigureApplication();
        }

        protected abstract void ConfigureApplication();

        protected override object GetInstance(Type service, string key)
        {
            if (service != null)
            {
                return this.Kernel.Get(service);
            }

            throw new ArgumentNullException(nameof(service));
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return this.Kernel.GetAll(service);
        }

        protected override void BuildUp(object instance)
        {
            this.Kernel.Inject(instance);
        }

        // the assemblies where Caliburn will look for convention-named Views and ViewModels
        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return new[] { Assembly.GetExecutingAssembly(), typeof(T).Assembly };
        }
    }
}
