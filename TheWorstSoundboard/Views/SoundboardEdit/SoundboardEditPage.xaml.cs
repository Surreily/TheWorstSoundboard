namespace Surreily.TheWorstSoundboard.Views.SoundboardEdit {
    [QueryProperty(nameof(SoundboardFolderPath), "SoundboardFolderPath")]
    public partial class SoundboardEditPage : ContentPage {
        public string SoundboardFolderPath { get; set; }

        public SoundboardEditPage() {
            InitializeComponent();
        }
    }
}
