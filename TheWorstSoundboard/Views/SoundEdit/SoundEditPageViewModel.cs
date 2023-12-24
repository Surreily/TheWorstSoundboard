namespace Surreily.TheWorstSoundboard.Views.SoundEdit {
    public class SoundEditPageViewModel {
        private string soundboardName;
        private string soundName;
        private string selectedSoundFilePath;
        private string selectedImageFilePath;

        public SoundEditPageViewModel() {
            SoundName = "New Sound";
        }

        private string SoundboardName {
            get => soundboardName;
            set {
                if (soundboardName != value) {
                    soundboardName = value;
                    // TODO: Notify property changed.
                }
            }
        }

        public string SoundName {
            get => soundName;
            set {
                if (soundName != value) {
                    soundName = value;
                    // TODO: Notify property changed.
                }
            }
        }

        public string SelectedSoundFilePath {
            get => selectedSoundFilePath;
            set {
                if (selectedSoundFilePath != value) {
                    selectedSoundFilePath = value;
                    // TODO: Notify property changed.
                }
            }
        }

        public string SelectedSoundFileName => Path.GetFileName(SelectedSoundFilePath);

        public string SelectedImageFilePath {
            get => selectedImageFilePath;
            set {
                if (selectedImageFilePath != value) {
                    selectedImageFilePath = value;
                    // TODO: Notify property changed.
                }
            }
        }

        public string SelectedImageFileName => Path.GetFileName(SelectedImageFilePath);

        public string NewSoundFilePath => Path.Combine(
            FileSystem.Current.AppDataDirectory,
            SoundboardName,
            Path.GetFileName(selectedSoundFilePath));

        public string NewImageFilePath => Path.Combine(
            FileSystem.Current.AppDataDirectory,
            SoundboardName,
            Path.GetFileName(selectedImageFilePath));

        public async Task SelectSoundFileAsync() {
            try {
                FilePickerFileType filePickerFileType = new FilePickerFileType(
                    new Dictionary<DevicePlatform, IEnumerable<string>> {
                        { DevicePlatform.Android, new string[] { "audio/mpeg" } },
                    });

                PickOptions pickOptions = new PickOptions() {
                    FileTypes = filePickerFileType,
                };

                FileResult? fileResult = await FilePicker.PickAsync(pickOptions);

                if (fileResult == null) {
                    return;
                }

                selectedSoundFilePath = fileResult.FullPath;
            } catch (Exception ex) {
                // TODO: Handle exception.
                throw;
            }
        }

        public async Task SelectImageFileAsync() {
            // TODO: Implement.
            await Task.CompletedTask;
        }

        public async Task SaveAsync() {
            File.Copy(SelectedSoundFilePath, NewSoundFilePath, true);
        }
    }
}
