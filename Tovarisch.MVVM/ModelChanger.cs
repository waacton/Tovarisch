namespace Wacton.Tovarisch.MVVM
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class ModelChanger
    {
        private readonly ModelChangeNotifier modelChangeNotifier;
        private readonly SynchronizationContext mainContext;

        public ModelChanger(ModelChangeNotifier modelChangeNotifier)
        {
            this.modelChangeNotifier = modelChangeNotifier;
            this.mainContext = SynchronizationContext.Current;
        }

        public void ExecuteAsync<T>(Action<T> command, T parameter, IEnumerable<object> changedModels, string commandName)
        {
            /* if there is a fault during the task action, post a task.Wait() onto the main thread
                * task.Wait() will throw an AggregateException if exception was thrown during the execution of the Task
                * because the Wait() is on the main thread, the thrown exception is will be caught by the unhandled exception handler */
            Task.Factory.StartNew(() => this.Execute(command, parameter, changedModels, commandName))
                .ContinueWith(task => this.mainContext.Post(state => task.Wait(), null), TaskContinuationOptions.OnlyOnFaulted);
        }

        public void Execute<T>(Action<T> command, T parameter, IEnumerable<object> changedModels, string commandName)
        {
            this.Execute(() => command(parameter), changedModels, commandName);
        }

        public void ExecuteAsync(Action command, IEnumerable<object> changedModels, string commandName)
        {
            /* if there is a fault during the task action, post a task.Wait() onto the main thread
                * task.Wait() will throw an AggregateException if exception was thrown during the execution of the Task
                * because the Wait() is on the main thread, the thrown exception is will be caught by the unhandled exception handler */
            Task.Factory.StartNew(() => this.Execute(command, changedModels, commandName))
                .ContinueWith(task => this.mainContext.Post(state => task.Wait(), null), TaskContinuationOptions.OnlyOnFaulted);
        }

        public void Execute(Action command, IEnumerable<object> changedModels, string commandName)
        {
            // can log command name here if necessary
            command();
            this.NotifySubscribersAboutChange(changedModels);
        }

        public void SubscribeToModelChange(object watchedModel, Action actionWhenChanged)
        {
            this.modelChangeNotifier.Subscribe(watchedModel, actionWhenChanged);
        }

        public void NotifySubscribersAboutChange(IEnumerable<object> changedModels)
        {
            foreach (var changedModel in changedModels)
            {
                this.NotifySubscribersAboutChange(changedModel);
            }
        }

        public void NotifySubscribersAboutChange(object changedModel)
        {
            this.modelChangeNotifier.Notify(changedModel);
        }
    }
}
