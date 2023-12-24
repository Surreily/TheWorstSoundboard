namespace Surreily.TheWorstSoundboard.Views.SoundEdit {
    [QueryProperty(nameof(SoundboardName), "SoundboardName")]
    [QueryProperty(nameof(SoundName), "SoundName")]
    public partial class SoundEditPage : ContentPage {
        public SoundEditPage(
            SoundEditPageViewModel viewModel) {

            InitializeComponent();
            BindingContext = viewModel;
        }

        public SoundEditPageViewModel ViewModel => (SoundEditPageViewModel)BindingContext;

        public string? SoundboardName {
            get => ViewModel.SoundboardName;
            set => ViewModel.SoundboardName = value;
        }

        public string? SoundName {
            get => ViewModel.SoundName;
            set => ViewModel.SoundName = value;
        }

        private async void SelectSoundButton_Clicked(object sender, EventArgs e) {
            await ViewModel.SelectSoundFileAsync();
        }

        private async void SelectImageButton_Clicked(object sender, EventArgs e) {
            await ViewModel.SelectImageFileAsync();
        }

        private async void SaveButton_Click(object sender, EventArgs e) {
            await ViewModel.SaveAsync();
        }
    }
}
