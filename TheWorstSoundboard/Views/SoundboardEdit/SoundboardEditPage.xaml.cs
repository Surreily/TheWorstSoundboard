using System.ComponentModel;
using CommunityToolkit.Maui.Views;
using Surreily.TheWorstSoundboard.Model;
using Surreily.TheWorstSoundboard.Storage.Sound;

namespace Surreily.TheWorstSoundboard.Views.SoundboardEdit {
    [QueryProperty(nameof(SoundboardName), "SoundboardName")]
    public partial class SoundboardEditPage : ContentPage {
        private readonly ISoundStorage soundStorage;

        public string? SoundboardName {
            get => ViewModel.SoundboardName;
            set => ViewModel.SoundboardName = value;
        }

        public SoundboardEditPage(
            SoundboardEditPageViewModel viewModel,
            ISoundStorage soundStorage) {

            InitializeComponent();
            BindingContext = viewModel;

            this.soundStorage = soundStorage;

            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        private void ViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e) {
            if (e.PropertyName == nameof(ViewModel.SoundModels)) {
                CreateSoundButtons();
            }
        }

        private void CreateSoundButtons() {
            SoundModelsFlexLayout.Clear();

            foreach (SoundModel soundModel in ViewModel.SoundModels!) {
                Frame frame = new Frame {
                    Background = Colors.AliceBlue,
                    Margin = 5,
                    Padding = 0,
                    HeightRequest = 90,
                    WidthRequest = 90,
                    CornerRadius = 45,
                };

                if (soundModel.HasImage) {
                    frame.Content = new Image {
                        Aspect = Aspect.AspectFill,
                        Source = ImageSource.FromFile(
                            soundStorage.GetSoundFilePath(
                                SoundboardName!,
                                soundModel.Name!,
                                soundModel.ImageExtension!)),
                        HeightRequest = 90,
                        WidthRequest = 90,
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Center,
                    };

                    frame.IsClippedToBounds = true;
                }

                // TODO: This is not a good gesture to use, need something else here.
                SwipeGestureRecognizer swipeGestureRecognizer = new SwipeGestureRecognizer {
                    Direction = SwipeDirection.Right,
                };

                swipeGestureRecognizer.Swiped += async (sender, e) => {
                    await NavigateToSoundEditPage(soundModel.Name);
                };

                TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();

                tapGestureRecognizer.Tapped += (sender, e) => PlaySound(soundModel);

                frame.GestureRecognizers.Add(tapGestureRecognizer);
                frame.GestureRecognizers.Add(swipeGestureRecognizer);

                SoundModelsFlexLayout.Add(frame);
            }
        }

        public SoundboardEditPageViewModel ViewModel => (SoundboardEditPageViewModel)BindingContext;

        protected override void OnNavigatedTo(NavigatedToEventArgs args) {
            base.OnNavigatedTo(args);

            ViewModel.LoadSoundboard();
        }

        private void PlaySound(SoundModel soundModel) {
            if (!soundModel.HasSound) {
                return;
            }

            MediaElement.Source = MediaSource.FromFile(
                ViewModel.GetSoundFilePath(soundModel.Name, soundModel.SoundExtension!));
            MediaElement.SeekTo(TimeSpan.Zero);
            MediaElement.Play();
        }

        private async void AddButton_Clicked(object sender, EventArgs e) {
            await NavigateToSoundEditPage();
        }

        private async Task NavigateToSoundEditPage(string? soundName = null) {
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "SoundboardName", SoundboardName! },
                { "SoundName", soundName ?? "New Sound" },
            };

            await Shell.Current.GoToAsync("Sounds/Edit", parameters);
        }

        private void SoundModelsListView_ItemTapped(object sender, ItemTappedEventArgs e) {
            SoundModel soundModel = (SoundModel)e.Item;

            PlaySound(soundModel);
        }
    }
}
