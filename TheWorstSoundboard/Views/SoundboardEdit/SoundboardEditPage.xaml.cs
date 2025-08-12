using System.ComponentModel;
using CommunityToolkit.Maui.Views;
using Surreily.TheWorstSoundboard.Model;
using Surreily.TheWorstSoundboard.Storage.Sound;

namespace Surreily.TheWorstSoundboard.Views.SoundboardEdit {
    [QueryProperty(nameof(SoundboardName), "SoundboardName")]
    public partial class SoundboardEditPage : ContentPage {
        private const int ButtonSize = 90;

        private readonly ISoundStorage soundStorage;
        private SoundModel? Playing { get; set; }

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

        #region Create sound buttons

        private void CreateSoundButtons() {
            SoundModelsFlexLayout.Clear();

            foreach (SoundModel soundModel in ViewModel.SoundModels!) {
                if (Playing != null && Playing == soundModel)
                {
                    CreateStopButton(soundModel);
                } else if (soundModel.HasImage) {
                    CreateImageButton(soundModel);
                } else {
                    CreateTextButton(soundModel);
                }
            }

            CreateAddButton();
        }

        private void CreateTextButton(SoundModel soundModel) {
            Frame frame = new Frame {
                Background = Color.FromRgba(223, 223, 223, 255),
                Margin = 5,
                Padding = 0,
                HeightRequest = ButtonSize,
                WidthRequest = ButtonSize,
                CornerRadius = ButtonSize / 2,
                Content = new Label {
                    WidthRequest = ButtonSize,
                    HeightRequest = ButtonSize,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    Text = soundModel.Name,
                    TextColor = Color.FromRgba(31, 31, 31, 255),
                },
            };

            AddPlaySoundGestureRecogniser(soundModel, frame);
            AddEditSoundGestureRecogniser(soundModel, frame);

            SoundModelsFlexLayout.Add(frame);
        }

        private void CreateImageButton(SoundModel soundModel) {
            Frame frame = new Frame {
                Margin = 5,
                Padding = 0,
                HeightRequest = ButtonSize,
                WidthRequest = ButtonSize,
                CornerRadius = ButtonSize / 2,
                IsClippedToBounds = true,
                Content = new Image {
                    Aspect = Aspect.AspectFill,
                    Source = ImageSource.FromFile(
                            soundStorage.GetSoundFilePath(
                                SoundboardName!,
                                soundModel.Name!,
                                soundModel.ImageExtension!)),
                    HeightRequest = ButtonSize,
                    WidthRequest = ButtonSize,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                },
            };

            AddPlaySoundGestureRecogniser(soundModel, frame);
            AddEditSoundGestureRecogniser(soundModel, frame);

            SoundModelsFlexLayout.Add(frame);
        }

        private void CreateStopButton(SoundModel soundModel)
        {
            Frame frame = new Frame
            {
                Background = Color.FromRgba(0, 0, 0, 63),
                Margin = 5,
                Padding = 0,
                HeightRequest = ButtonSize,
                WidthRequest = ButtonSize,
                CornerRadius = ButtonSize / 2,
                Content = new Label
                {
                    WidthRequest = ButtonSize,
                    HeightRequest = ButtonSize,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    Text = "STOP",
                    TextColor = Color.FromRgba(31, 31, 31, 255),
                },
            };

            AddPlaySoundGestureRecogniser(soundModel, frame);

            SoundModelsFlexLayout.Add(frame);
        }

        private void CreateAddButton() {
            Frame frame = new Frame {
                Background = Color.FromRgba(0, 0, 0, 63),
                Margin = 5,
                Padding = 0,
                HeightRequest = ButtonSize,
                WidthRequest = ButtonSize,
                CornerRadius = ButtonSize / 2,
                Content = new Label {
                    WidthRequest = ButtonSize,
                    HeightRequest = ButtonSize,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    FontSize = 60,
                    Text = "+",
                },
            };

            AddCreateSoundGestureRecogniser(frame);

            SoundModelsFlexLayout.Add(frame);
        }

        private void AddPlaySoundGestureRecogniser(SoundModel soundModel, Frame frame) {
            TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();

            tapGestureRecognizer.Tapped += (sender, e) =>
                PlaySound(soundModel);

            frame.GestureRecognizers.Add(tapGestureRecognizer);
        }

        private void AddEditSoundGestureRecogniser(SoundModel soundModel, Frame frame) {
            SwipeGestureRecognizer swipeGestureRecognizer = new SwipeGestureRecognizer {
                Direction = SwipeDirection.Right,
            };

            swipeGestureRecognizer.Swiped += async (sender, e) =>
                await NavigateToSoundEditPage(soundModel.Name);

            frame.GestureRecognizers.Add(swipeGestureRecognizer);
        }

        private void AddCreateSoundGestureRecogniser(Frame frame) {
            TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();

            tapGestureRecognizer.Tapped += async (sender, e) =>
                await NavigateToSoundEditPage();

            frame.GestureRecognizers.Add(tapGestureRecognizer);
        }

        #endregion

        public SoundboardEditPageViewModel ViewModel => (SoundboardEditPageViewModel)BindingContext;

        protected override void OnNavigatedTo(NavigatedToEventArgs args) {
            base.OnNavigatedTo(args);

            ViewModel.LoadSoundboard();
        }

        private void PlaySound(SoundModel soundModel) {
            if (!soundModel.HasSound) {
                return;
            }

            if (MediaElement.CurrentState == CommunityToolkit.Maui.Core.Primitives.MediaElementState.Playing)
            {
                MediaElement.Stop();
                Playing = null;
            } 
            else
            {
                MediaElement.Source = MediaSource.FromFile(
                ViewModel.GetSoundFilePath(soundModel.Name, soundModel.SoundExtension!));
                MediaElement.SeekTo(TimeSpan.Zero);
                MediaElement.Play();
                Playing = soundModel;
            }
            CreateSoundButtons();
        }

        private async Task NavigateToSoundEditPage(string? soundName = null) {
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "SoundboardName", SoundboardName! },
                { "SoundName", soundName ?? string.Empty },
                { "IsNew", soundName == null },
            };

            await Shell.Current.GoToAsync("Sounds/Edit", parameters);
        }

        private void SoundModelsListView_ItemTapped(object sender, ItemTappedEventArgs e) {
            SoundModel soundModel = (SoundModel)e.Item;

            PlaySound(soundModel);
        }
    }
}
