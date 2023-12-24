using Surreily.TheWorstSoundboard.Model;

namespace Surreily.TheWorstSoundboard.Storage.Soundboard {
    public interface ISoundboardStorage {
        IList<SoundboardModel> GetSoundboardModels();

        void CreateSoundboard(string soundboardName);

        void DeleteSoundboard(string soundboardName);
    }
}
