namespace Surreily.TheWorstSoundboard.Views.SoundEdit {
    [QueryProperty(nameof(SoundboardName), "SoundboardName")]
    [QueryProperty(nameof(SoundName), "SoundName")]
    public partial class SoundEditPage : ContentPage {
        public SoundEditPage() {
            InitializeComponent();

            ViewModel = new SoundEditPageViewModel();

            BindingContext = ViewModel;
        }

        public string? SoundboardName {
            get => ViewModel.SoundboardName;
            set => ViewModel.SoundboardName = value;
        }

        public string? SoundName {
            get => ViewModel.SoundName;
            set => ViewModel.SoundName = value;
        }

        public SoundEditPageViewModel ViewModel { get; set; }

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
