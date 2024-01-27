using Surreily.TheWorstSoundboard.Exceptions;

namespace Surreily.TheWorstSoundboard.Views.SoundEdit {
    [QueryProperty(nameof(SoundboardName), "SoundboardName")]
    [QueryProperty(nameof(SoundName), "SoundName")]
    [QueryProperty(nameof(IsNew), "IsNew")]
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

        public bool IsNew {
            get => ViewModel.IsNew;
            set => ViewModel.IsNew = value;
        }

        private async void SelectSoundButton_Clicked(object sender, EventArgs e) {
            await ViewModel.SelectSoundFileAsync();
        }

        private async void SelectImageButton_Clicked(object sender, EventArgs e) {
            await ViewModel.SelectImageFileAsync();
        }

        private async void DeleteButton_Click(object sender, EventArgs e) {
            bool result = await DisplayAlert(
                "Confirm Delete",
                "Are you sure you want to delete this sound?",
                "Yes",
                "No");

            if (!result) {
                return;
            }

            ViewModel.Delete();
        }

        private async void SaveButton_Click(object sender, EventArgs e) {
            try {
                await ViewModel.SaveAsync();
            } catch (ValidationFailedException ex) {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        
    }
}
