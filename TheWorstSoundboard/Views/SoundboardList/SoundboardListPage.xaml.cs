using CommunityToolkit.Maui.Storage;
using Surreily.TheWorstSoundboard.Model;


namespace Surreily.TheWorstSoundboard.Views.SoundboardList {
    public partial class SoundboardListPage : ContentPage {
        public SoundboardListPage(
            SoundboardListPageViewModel viewModel) {

            InitializeComponent();
            BindingContext = viewModel;
        }

        public SoundboardListPageViewModel ViewModel => (SoundboardListPageViewModel)BindingContext;

        protected override void OnNavigatedTo(NavigatedToEventArgs args) {
            ViewModel.LoadSoundboards();
        }

        public async Task CreateSoundboard() {
            string name = await DisplayPromptAsync(
                "Create Soundboard",
                "Enter a title for your new soundboard.");

            name = name.Trim();

            if (string.IsNullOrWhiteSpace(name)) {
                return;
            }

            ViewModel.CreateSoundboard(name);
        }

        #region Event handlers

        private async void CreateButton_Clicked(object sender, EventArgs e) {
            await CreateSoundboard();
            OnPropertyChanged(nameof(ViewModel.SoundboardModels));
        }

        private async void ImportButton_Clicked(object sender, EventArgs e) {
            FolderPickerResult folderPickerResult = await FolderPicker.Default.PickAsync();

            if (folderPickerResult == null || !folderPickerResult.IsSuccessful) {
                return;
            }

            string folderPath = folderPickerResult.Folder!.Path;

            await ViewModel.ImportSoundboard(folderPath);
        }

        private async void SoundboardModelsListView_ItemTapped(object sender, ItemTappedEventArgs e) {
            SoundboardModel model = (SoundboardModel)e.Item;

            // TODO: Pass in correct soundboard name.
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "SoundboardName", "Dank Memes" },
            };

            await Shell.Current.GoToAsync("Soundboards/Edit", parameters);
        }

        #endregion

    }
}
