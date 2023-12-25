using Surreily.TheWorstSoundboard.Storage.Sound;

namespace Surreily.TheWorstSoundboard.Views.SoundEdit {
    public class SoundEditPageViewModel : ViewModelBase {
        private readonly ISoundStorage soundStorage;

        private string? soundboardName;
        private string? soundName;
        private string? selectedSoundFilePath;
        private string? selectedImageFilePath;
        private FileResult? selectedSoundFileResult;
        private FileResult? selectedImageFileResult;

        public SoundEditPageViewModel(
            ISoundStorage soundStorage) {

            this.soundStorage = soundStorage;

            SoundName = "New Sound";
        }

        public string? SoundboardName {
            get => soundboardName;
            set {
                if (soundboardName != value) {
                    soundboardName = value;
                    OnPropertyChanged(nameof(SoundboardName));
                }
            }
        }

        public string? SoundName {
            get => soundName;
            set {
                if (soundName != value) {
                    soundName = value;
                    OnPropertyChanged(nameof(SoundName));
                }
            }
        }

        public string? SelectedSoundFilePath {
            get => selectedSoundFilePath;
            set {
                if (selectedSoundFilePath != value) {
                    selectedSoundFilePath = value;
                    OnPropertyChanged(nameof(SelectedSoundFilePath));
                    OnPropertyChanged(nameof(SelectedSoundFileName));
                }
            }
        }

        public string? SelectedSoundFileName => Path.GetFileName(SelectedSoundFilePath);

        public string? SelectedImageFilePath {
            get => selectedImageFilePath;
            set {
                if (selectedImageFilePath != value) {
                    selectedImageFilePath = value;
                    OnPropertyChanged(nameof(SelectedImageFilePath));
                    OnPropertyChanged(nameof(SelectedImageFileName));
                }
            }
        }

        public string? SelectedImageFileName => Path.GetFileName(SelectedImageFilePath);

        public async Task SelectSoundFileAsync() {
            try {
                FilePickerFileType filePickerFileType = new FilePickerFileType(
                    new Dictionary<DevicePlatform, IEnumerable<string>> {
                        { DevicePlatform.Android, new string[] { "audio/mpeg" } },
                    });

                PickOptions pickOptions = new PickOptions() {
                    FileTypes = filePickerFileType,
                };

                selectedSoundFileResult = await FilePicker.PickAsync(pickOptions);
            } catch (Exception ex) {
                // TODO: Handle exception.
                throw;
            }
        }

        public async Task SelectImageFileAsync() {
            try {
                FilePickerFileType filePickerFileType = new FilePickerFileType(
                    new Dictionary<DevicePlatform, IEnumerable<string>> {
                        { DevicePlatform.Android, new string[] { "image/jpeg", "image/png" } },
                    });

                PickOptions pickOptions = new PickOptions() {
                    FileTypes = filePickerFileType,
                };

                selectedImageFileResult = await FilePicker.PickAsync(pickOptions);
            } catch (Exception ex) {
                // TODO: Handle exception.
                throw;
            }
        }

        public async Task SaveAsync() {
            if (SoundboardName == null || SoundName == null) {
                // TODO: Show error message.
                return;
            }

            if (selectedSoundFileResult != null) {
                using (Stream stream = await selectedSoundFileResult.OpenReadAsync()) {
                    await soundStorage.SaveSoundFileAsync(
                        SoundboardName,
                        SoundName,
                        Path.GetExtension(selectedSoundFileResult.FileName),
                        stream);
                }
            }

            if (selectedImageFileResult != null) {
                using (Stream stream = await selectedImageFileResult.OpenReadAsync()) {
                    await soundStorage.SaveSoundFileAsync(
                        SoundboardName,
                        SoundName,
                        Path.GetExtension(selectedImageFileResult.FileName),
                        stream);
                }
            }
        }
    }
}
