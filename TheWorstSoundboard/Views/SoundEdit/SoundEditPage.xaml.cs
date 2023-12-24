namespace Surreily.TheWorstSoundboard.Views.SoundEdit {
    public partial class SoundEditPage : ContentPage {
        public SoundEditPage() {
            InitializeComponent();

            ViewModel = new SoundEditPageViewModel();

            BindingContext = ViewModel;
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
