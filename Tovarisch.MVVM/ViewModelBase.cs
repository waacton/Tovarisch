namespace Wacton.Tovarisch.MVVM
{
    using Caliburn.Micro;

    public abstract class ViewModelBase : PropertyChangedBase
    {
        protected ModelChanger ModelChanger { get; }

        protected ViewModelBase(ModelChanger modelChanger, params object[] watchedModels)
        {
            this.ModelChanger = modelChanger;
            foreach (var watchedModel in watchedModels)
            {
                this.ModelChanger.SubscribeToModelChange(watchedModel, this.NotifyAllPropertyBindings);
            }
        }

        protected virtual void NotifyAllPropertyBindings()
        {
            var properties = this.GetType().GetProperties();
            foreach (var property in properties)
            {
                this.NotifyOfPropertyChange(property.Name);
            }
        }
    }

    public abstract class ViewModelBase<T> : ViewModelBase
    {
        protected ViewModelBase(ModelChanger modelChanger, T watchedModel)
            : base(modelChanger, watchedModel)
        {
        }
    }
}