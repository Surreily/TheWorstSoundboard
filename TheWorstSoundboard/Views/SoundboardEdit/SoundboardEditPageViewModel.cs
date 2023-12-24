using Surreily.TheWorstSoundboard.Model;
using Surreily.TheWorstSoundboard.Utility;

namespace Surreily.TheWorstSoundboard.Views.SoundboardEdit {
    public class SoundboardEditPageViewModel : ViewModelBase {
        private string? soundboardName;
        private IList<SoundModel>? soundModels;

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
            SoundModels = LocalStorage.GetSoundModels(SoundboardName!);
        }
    }
}
