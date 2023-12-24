using System.Collections.ObjectModel;
using Surreily.TheWorstSoundboard.Model;
using Surreily.TheWorstSoundboard.Storage.Soundboard;

namespace Surreily.TheWorstSoundboard.Views.SoundboardList {
    public class SoundboardListPageViewModel : ViewModelBase {
        private readonly ISoundboardStorage soundboardStorage;

        private ObservableCollection<SoundboardModel> soundboardModels;

        public SoundboardListPageViewModel(ISoundboardStorage soundboardStorage) {
            this.soundboardStorage = soundboardStorage;

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
    }
}
