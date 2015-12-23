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
        private IKernel kernel;

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
            this.kernel = new StandardKernel();

            // bind caliburn objects
            this.kernel.Bind<IWindowManager>().To<WindowManager>().InSingletonScope();
            this.kernel.Bind<IEventAggregator>().To<EventAggregator>().InSingletonScope();

            // bind tovarisch MVVM objects
            this.kernel.Bind<ModelChangeNotifier>().ToSelf().InSingletonScope();
            this.kernel.Bind<ModelChanger>().ToSelf().InSingletonScope();

            this.ConfigureApplication(this.kernel);
        }

        protected abstract void ConfigureApplication(IKernel kernel);

        protected override object GetInstance(Type service, string key)
        {
            if (service != null)
            {
                return this.kernel.Get(service);
            }

            throw new ArgumentNullException(nameof(service));
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return this.kernel.GetAll(service);
        }

        protected override void BuildUp(object instance)
        {
            this.kernel.Inject(instance);
        }

        // the assemblies where caliburn will look for convention-named Views and ViewModels
        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return new[] { Assembly.GetExecutingAssembly(), typeof(T).Assembly };
        }
    }
}
