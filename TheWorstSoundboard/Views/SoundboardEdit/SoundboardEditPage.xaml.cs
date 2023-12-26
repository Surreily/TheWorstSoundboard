using CommunityToolkit.Maui.Converters;
using CommunityToolkit.Maui.Views;
using Surreily.TheWorstSoundboard.Model;

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

        private void PlaySound(SoundModel soundModel) {
            string soundName = Path.GetFileNameWithoutExtension(soundModel.SoundFileName)!;
            string extension = Path.GetExtension(soundModel.SoundFileName)!;

            MediaElement.Source = MediaSource.FromFile(ViewModel.GetSoundFilePath(soundName, extension));
            MediaElement.SeekTo(TimeSpan.Zero);
            MediaElement.Play();
        }

        private async void AddButton_Clicked(object sender, EventArgs e) {
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "SoundboardName", SoundboardName! },
                { "SoundName", "New Sound" },
            };

            await Shell.Current.GoToAsync("Sounds/Edit", parameters);
        }

        private void SoundModelsListView_ItemTapped(object sender, ItemTappedEventArgs e) {
            SoundModel soundModel = (SoundModel)e.Item;

            PlaySound(soundModel);
        }
    }
}
