namespace Wacton.Tovarisch.MVVM
{
    using System;
    using System.Collections.Generic;

    public class ModelChangeNotifier
    {
        private readonly List<ModelChangeSubscription> modelChangeSubscriptions = new List<ModelChangeSubscription>();

        public void Subscribe(object watchedModel, Action actionWhenChanged)
        {
            var modelChangeSubscription = new ModelChangeSubscription(new WeakReference(watchedModel), actionWhenChanged);
            this.modelChangeSubscriptions.Add(modelChangeSubscription);
        }

        public void Notify(object changedModel)
        {
            foreach (var modelChangeSubscription in this.modelChangeSubscriptions)
            {
                var watchedModel = modelChangeSubscription.Reference.Target;
                if (changedModel == watchedModel)
                {
                    modelChangeSubscription.Action();
                }
            }
        }
    }
}
