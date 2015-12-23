namespace Wacton.Skeleton.UI.Mains
{
    using Wacton.Tovarisch.MVVM;

    public class BasicShellViewModel : ViewModelBase
    {
        public static string WindowTitle { get; set; }
        public MainViewModel MainViewModel { get; }

        public BasicShellViewModel(MainViewModel mainViewModel, ModelChanger modelChanger)
            : base(modelChanger)
        {
            this.MainViewModel = mainViewModel;
        }
    }
}
