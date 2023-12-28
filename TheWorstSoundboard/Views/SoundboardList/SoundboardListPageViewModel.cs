using System.Collections.ObjectModel;
using Surreily.TheWorstSoundboard.Model;
using Surreily.TheWorstSoundboard.Storage.Sound;
using Surreily.TheWorstSoundboard.Storage.Soundboard;
using Surreily.TheWorstSoundboard.Utility;

namespace Surreily.TheWorstSoundboard.Views.SoundboardList {
    public class SoundboardListPageViewModel : ViewModelBase {
        private readonly ISoundboardStorage soundboardStorage;
        private readonly ISoundStorage soundStorage;
        private readonly FolderUtility folderUtility;

        private ObservableCollection<SoundboardModel> soundboardModels;

        public SoundboardListPageViewModel(
            ISoundboardStorage soundboardStorage,
            ISoundStorage soundStorage,
            FolderUtility folderUtility) {

            this.soundboardStorage = soundboardStorage;
            this.soundStorage = soundStorage;
            this.folderUtility = folderUtility;

            soundboardModels = new ObservableCollection<SoundboardModel>();
        }

        public ObservableCollection<SoundboardModel> SoundboardModels {
            get => soundboardModels;
            set {
                if (soundboardModels != value) {
                    soundboardModels = value;
                    OnPropertyChanged(nameof(SoundboardModels));
                }
            }
        }

        public void LoadSoundboards() {
            SoundboardModels = new ObservableCollection<SoundboardModel>(
                soundboardStorage.GetSoundboardModels());
        }

        public void CreateSoundboard(string name) {
            soundboardStorage.CreateSoundboard(name);

            soundboardModels.Add(new SoundboardModel {
                Name = name,
            });
        }

        public async Task ImportSoundboard(string folderPath) {
            string soundboardName = Path.GetFileName(folderPath);

            soundboardStorage.CreateSoundboard(soundboardName);

            IList<SoundModel> soundModels = folderUtility.GetSoundModels(folderPath);

            foreach (SoundModel soundModel in soundModels) {
                string soundFilePath = Path.Combine(
                    folderPath,
                    soundModel.Name + soundModel.SoundExtension);

                using (FileStream stream = new FileStream(
                    soundFilePath, FileMode.Open, FileAccess.Read, FileShare.Read)) {

                    await soundStorage.SaveSoundFileAsync(
                        soundboardName, soundModel.Name, soundModel.SoundExtension!, stream);
                }

                if (soundModel.HasImage) {
                    string imageFilePath = Path.Combine(
                        folderPath,
                        soundModel.Name + soundModel.ImageExtension);

                    using (FileStream stream = new FileStream(
                        imageFilePath, FileMode.Open, FileAccess.Read, FileShare.Read)) {

                        await soundStorage.SaveSoundFileAsync(
                            soundboardName, soundModel.Name, soundModel.ImageExtension!, stream);
                    }
                }
            }
        }
    }
}
