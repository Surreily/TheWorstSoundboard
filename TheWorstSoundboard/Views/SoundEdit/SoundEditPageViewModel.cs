using Surreily.TheWorstSoundboard.Exceptions;
using Surreily.TheWorstSoundboard.Storage.Sound;

namespace Surreily.TheWorstSoundboard.Views.SoundEdit {
    public class SoundEditPageViewModel : ViewModelBase {
        private readonly ISoundStorage soundStorage;

        private string? soundboardName;
        private string? soundName;
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

        public FileResult? SelectedSoundFileResult {
            get => selectedSoundFileResult;
            set {
                if (selectedSoundFileResult != value) {
                    selectedSoundFileResult = value;
                    OnPropertyChanged(nameof(SelectedSoundFileResult));
                    OnPropertyChanged(nameof(SelectedSoundFileName));
                }
            }
        }

        public string? SelectedSoundFileName =>
            SelectedSoundFileResult != null
                ? SelectedSoundFileResult!.FileName
                : null;

        public FileResult? SelectedImageFileResult {
            get => selectedImageFileResult;
            set {
                if (selectedImageFileResult != value) {
                    selectedImageFileResult = value;
                    OnPropertyChanged(nameof(SelectedImageFileResult));
                    OnPropertyChanged(nameof(SelectedImageFileName));
                }
            }
        }

        public string? SelectedImageFileName =>
            SelectedImageFileResult != null
                ? SelectedImageFileResult!.FileName
                : null;

        public async Task SelectSoundFileAsync() {
            try {
                FilePickerFileType filePickerFileType = new FilePickerFileType(
                    new Dictionary<DevicePlatform, IEnumerable<string>> {
                        { DevicePlatform.Android, new string[] { "audio/mpeg" } },
                    });

                PickOptions pickOptions = new PickOptions() {
                    FileTypes = filePickerFileType,
                };

                SelectedSoundFileResult = await FilePicker.PickAsync(pickOptions);
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

                SelectedImageFileResult = await FilePicker.PickAsync(pickOptions);
            } catch (Exception ex) {
                // TODO: Handle exception.
                throw;
            }
        }

        public async Task SaveAsync() {
            if (SoundboardName == null || SoundName == null) {
                throw new InvalidOperationException("SoundboardName or SoundName cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(SoundName)) {
                throw new ValidationFailedException("Name is required.");
            }

            if (SelectedSoundFileResult == null) {
                throw new ValidationFailedException("Sound file is required.");
            }

            using (Stream stream = await selectedSoundFileResult!.OpenReadAsync()) {
                await soundStorage.SaveSoundFileAsync(
                    SoundboardName,
                    SoundName,
                    Path.GetExtension(selectedSoundFileResult.FileName),
                    stream);
            }

            if (selectedImageFileResult != null) {
                using (Stream stream = await selectedImageFileResult!.OpenReadAsync()) {
                    await soundStorage.SaveSoundFileAsync(
                        SoundboardName,
                        SoundName,
                        Path.GetExtension(selectedImageFileResult!.FileName),
                        stream);
                }
            }
        }
    }
}
