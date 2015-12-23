namespace Wacton.Skeleton.UI.Mains
{
    using Wacton.Skeleton.Domain.Commands;
    using Wacton.Skeleton.Domain.Mains;
    using Wacton.Tovarisch.MVVM;

    public class MainViewModel : ViewModelBase
    {
        private readonly Main main;
        private readonly UpdateModelCommand updateModelCommand;
        private int counter;

        /// <summary>
        /// bindings to this property will be notified automatically, as based on a model that is being watched (see constructor)
        /// </summary>
        public string Word => this.main.Word;

        /// <summary>
        /// bindings to this property need to be notified manually, as view/viewmodel only - not based on the model
        /// </summary>
        private string counterDetails;
        public string CounterDetails
        {
            get
            {
                return this.counterDetails;
            }
            set
            {
                this.counterDetails = value;
                this.NotifyOfPropertyChange(nameof(this.CounterDetails));
            }
        }

        /// <summary>
        /// view model base is told which model objects to watch for changes
        /// when change happens, view for this view model will update all property bindings
        /// </summary>
        public MainViewModel(Main main, UpdateModelCommand updateModelCommand, ModelChanger modelChanger)
            : base(modelChanger, main)
        {
            this.main = main;
            this.updateModelCommand = updateModelCommand;
            this.UpdateCounterDetails();
        }

        /// <summary>
        /// called by the view using cal:Message.Attach
        /// </summary>
        public void Update()
        {
            this.updateModelCommand.ExecuteAndNotify(); // this is what will update the domain model and notify view models of changes
            this.counter++;
            this.UpdateCounterDetails();
        }

        private void UpdateCounterDetails()
        {
            this.CounterDetails = $"updated {this.counter} {(this.counter == 1 ? "time" : "times")}";
        }
    }
}
