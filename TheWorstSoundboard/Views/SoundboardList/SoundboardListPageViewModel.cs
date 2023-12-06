using System.Collections.ObjectModel;
using Surreily.TheWorstSoundboard.Model;

namespace Surreily.TheWorstSoundboard.Views.SoundboardList {
    public class SoundboardListPageViewModel {
        public SoundboardListPageViewModel() {
            SoundboardModels = new ObservableCollection<SoundboardModel>();
        }

        public ObservableCollection<SoundboardModel> SoundboardModels { get; set; }
    }
}
