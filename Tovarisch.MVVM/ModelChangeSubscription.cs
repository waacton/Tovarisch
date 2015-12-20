namespace Wacton.Tovarisch.MVVM
{
    using System;

    public class ModelChangeSubscription
    {
        public WeakReference Reference { get; private set; }
        public Action Action { get; private set; }

        public ModelChangeSubscription(WeakReference reference, Action action)
        {
            this.Reference = reference;
            this.Action = action;
        }

        public override string ToString() => $"{this.Reference.Target} >>> {this.Action.Target} ~ {this.Action.Method}";
    }
}
