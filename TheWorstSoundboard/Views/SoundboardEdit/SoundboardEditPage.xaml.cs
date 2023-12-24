using System.Runtime.CompilerServices;

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

        protected override async void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(SoundboardFolderPath)) {
                if (SoundboardFolderPath != null) {
                    try {
                        await FilePicker.PickAsync();
                    } catch (Exception ex) {
                        await DisplayAlert("Error", ex.Message, "Bleh"); // TODO: Make this more "professional".
                    }
                    
                }
            }
        }
    }
}
