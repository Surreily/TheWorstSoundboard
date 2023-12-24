using Surreily.TheWorstSoundboard.Model;

namespace Surreily.TheWorstSoundboard.Views.SoundboardList {
    public partial class SoundboardListPage : ContentPage {
        public SoundboardListPageViewModel ViewModel { get; set; }

        public SoundboardListPage() {
            InitializeComponent();

            ViewModel = new SoundboardListPageViewModel();

            for (int i = 1; i <= 5; i++) {
                ViewModel.SoundboardModels.Add(new SoundboardModel {
                    Title = $"Soundboard {i}",
                });
            }

            BindingContext = ViewModel;
        }

        public async Task CreateSoundboard() {
            string title = await DisplayPromptAsync(
                "Create Soundboard",
                "Enter a title for your new soundboard.");

            title = title.Trim();

            if (string.IsNullOrWhiteSpace(title)) {
                return;
            }

            ViewModel.SoundboardModels.Add(new SoundboardModel {
                Title = title,
            });
        }

        #region Event handlers

        private async void CreateButton_Clicked(object sender, EventArgs e) {
            await CreateSoundboard();
            OnPropertyChanged(nameof(ViewModel.SoundboardModels));
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
