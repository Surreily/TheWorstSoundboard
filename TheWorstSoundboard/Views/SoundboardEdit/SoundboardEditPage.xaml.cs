namespace Surreily.TheWorstSoundboard.Views.SoundboardEdit {
    [QueryProperty(nameof(SoundboardFolderPath), "SoundboardFolderPath")]
    public partial class SoundboardEditPage : ContentPage {
        private string soundboardFolderPath;

        public string SoundboardFolderPath {
            get => soundboardFolderPath;
            set {
                if (soundboardFolderPath != value) {
                    soundboardFolderPath = value;
                    OnPropertyChanged();
                }
            }
        }

        public SoundboardEditPage() {
            InitializeComponent();
        }

        private async void TemporaryButton_Clicked(object sender, EventArgs e) {
            // TODO: Remove this button and method!

            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "SoundboardName", "Dank Memes" },
                { "SoundName", "Sad Violin" }
            };

            await Shell.Current.GoToAsync("Sounds/Edit", parameters);
        }
    }
}
