using Surreily.TheWorstSoundboard.Model;
using Surreily.TheWorstSoundboard.Storage.Sound;

namespace Surreily.TheWorstSoundboard.Views.SoundboardEdit {
    public class SoundboardEditPageViewModel : ViewModelBase {
        private readonly ISoundStorage soundStorage;

        private string? soundboardName;
        private IList<SoundModel>? soundModels;

        public SoundboardEditPageViewModel(
            ISoundStorage soundStorage) {

            this.soundStorage = soundStorage;
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

        public IList<SoundModel>? SoundModels {
            get => soundModels;
            set {
                if (soundModels != value) {
                    soundModels = value;
                    OnPropertyChanged(nameof(SoundModels));
                }
            }
        }

        public void LoadSoundboard() {
            SoundModels = soundStorage.GetSoundModels(SoundboardName!);
        }
    }
}
