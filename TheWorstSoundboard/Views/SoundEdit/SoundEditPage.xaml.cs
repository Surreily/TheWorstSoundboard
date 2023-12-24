namespace Surreily.TheWorstSoundboard.Views.SoundEdit {
    public partial class SoundEditPage : ContentPage {
        public SoundEditPage() {
            InitializeComponent();

            ViewModel = new SoundEditPageViewModel();
        }

        public SoundEditPageViewModel ViewModel { get; set; }

        private async void SelectSoundButton_Clicked(object sender, EventArgs e) {
            await ViewModel.SelectSoundFileAsync();
        }
    }
}
