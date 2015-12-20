namespace Wacton.Tovarisch.MVVM
{
    public abstract class ModelCommand
    {
        private readonly ModelChanger modelChanger;
        private readonly object[] changedModels;

        protected ModelCommand(ModelChanger modelChanger, params object[] changedModels)
        {
            this.modelChanger = modelChanger;
            this.changedModels = changedModels;
        }

        protected abstract void Execute();

        public void ExecuteAndNotify()
        {
            this.modelChanger.Execute(this.Execute, this.changedModels, this.CommandName());
        }

        public void ExecuteAsyncAndNotify()
        {
            this.modelChanger.ExecuteAsync(this.Execute, this.changedModels, this.CommandName());
        }

        protected void NotifySubscribersAboutChange()
        {
            this.modelChanger.NotifySubscribersAboutChange(this.changedModels);
        }

        protected void NotifySubscribersAboutChange(object model)
        {
            this.modelChanger.NotifySubscribersAboutChange(model);
        }

        protected virtual string CommandName()
        {
            return this.GetType().Name;
        }
    }

    public abstract class ModelCommand<T>
    {
        private readonly ModelChanger modelChanger;
        private readonly object[] changedModels;

        protected ModelCommand(ModelChanger modelChanger, params object[] changedModels)
        {
            this.modelChanger = modelChanger;
            this.changedModels = changedModels;
        }

        public void ExecuteAndNotify(T parameter)
        {
            this.modelChanger.Execute(this.Execute, parameter, this.changedModels, this.CommandName(parameter));
        }

        public void ExecuteAsyncAndNotify(T parameter)
        {
            this.modelChanger.ExecuteAsync(this.Execute, parameter, this.changedModels, this.CommandName(parameter));
        }

        protected void NotifySubscribersAboutChange()
        {
            this.modelChanger.NotifySubscribersAboutChange(this.changedModels);
        }

        protected void NotifySubscribersAboutChange(object model)
        {
            this.modelChanger.NotifySubscribersAboutChange(model);
        }

        protected abstract void Execute(T parameter);

        protected virtual string CommandName(T parameter)
        {
            return this.GetType().Name + ": " + parameter;
        }
    }
}