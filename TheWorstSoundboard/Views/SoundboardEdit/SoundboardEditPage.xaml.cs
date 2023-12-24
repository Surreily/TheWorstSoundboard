namespace Surreily.TheWorstSoundboard.Views.SoundboardEdit {
    [QueryProperty(nameof(SoundboardName), "SoundboardName")]
    public partial class SoundboardEditPage : ContentPage {
        public string? SoundboardName {
            get => ViewModel.SoundboardName;
            set => ViewModel.SoundboardName = value;
        }

        public SoundboardEditPage(
            SoundboardEditPageViewModel viewModel) {

            InitializeComponent();
            BindingContext = viewModel;
        }

        public SoundboardEditPageViewModel ViewModel => (SoundboardEditPageViewModel)BindingContext;

        protected override void OnNavigatedTo(NavigatedToEventArgs args) {
            base.OnNavigatedTo(args);

            ViewModel.LoadSoundboard();
        }

        private async void TemporaryButton_Clicked(object sender, EventArgs e) {
            // TODO: Remove this button and method!

            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "SoundboardName", "Dank Memes" },
                { "SoundName", "Sad Violin" },
            };

            await Shell.Current.GoToAsync("Sounds/Edit", parameters);
        }
    }
}
