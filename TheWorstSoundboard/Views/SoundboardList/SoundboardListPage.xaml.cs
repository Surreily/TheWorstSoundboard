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

        private async void OnCreateButtonClicked(object sender, EventArgs e) {
            await CreateSoundboard();
            OnPropertyChanged(nameof(ViewModel.SoundboardModels));
        }

        #endregion

    }
}
