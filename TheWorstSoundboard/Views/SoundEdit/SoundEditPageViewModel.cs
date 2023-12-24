namespace Surreily.TheWorstSoundboard.Views.SoundEdit {
    public class SoundEditPageViewModel {
        private string soundboardFolderName;
        private string soundFileName;

        public SoundEditPageViewModel() {

        }

        private string SoundboardFolderName {
            get => soundboardFolderName;
            set {
                if (soundboardFolderName != value) {
                    soundboardFolderName = value;
                    // TODO: Notify property changed.
                }
            }
        }

        public string SoundFileName {
            get => soundFileName;
            set {
                if (soundFileName != value) {
                    soundFileName = value;
                    // TODO: Notify property changed.
                }
            }
        }

        public async Task SelectSoundFileAsync() {
            try {
                FilePickerFileType filePickerFileType = new FilePickerFileType(
                    new Dictionary<DevicePlatform, IEnumerable<string>> {
                        { DevicePlatform.Android, new string[] { "audio/mpeg" } },
                    });

                PickOptions pickOptions = new PickOptions() {
                    FileTypes = filePickerFileType,
                };

                await FilePicker.PickAsync(pickOptions);
            } catch (Exception ex) {
                // TODO: Handle exception.
                throw;
            }
        }
    }
}
