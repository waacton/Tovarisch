namespace Wacton.Skeleton.Domain.Commands
{
    using Wacton.Skeleton.Domain.Mains;
    using Wacton.Tovarisch.MVVM;

    public class UpdateModelCommand : ModelCommand
    {
        private readonly Main main;

        /// <summary>
        /// model command is explicitly told which objects will be changed
        /// when the model command is executed
        /// ---
        /// ninject singleton bindings will ensure these objects are the same
        /// as the objects used elsewhere in the domain model and view models
        /// </summary>
        public UpdateModelCommand(ModelChanger modelChanger, Main main)
            : base(modelChanger, main)
        {
            this.main = main;
        }

        protected override void Execute()
        {
            this.main.Update();
        }
    }
}