namespace Wacton.Tovarisch.MVVM
{
    using System;

    public class ModelChangeSubscription
    {
        public WeakReference Reference { get; }
        public Action Action { get; }

        public ModelChangeSubscription(WeakReference reference, Action action)
        {
            this.Reference = reference;
            this.Action = action;
        }

        public override string ToString() => $"{this.Reference.Target} >>> {this.Action.Target} ~ {this.Action.Method}";
    }
}
