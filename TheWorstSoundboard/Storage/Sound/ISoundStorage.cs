using Surreily.TheWorstSoundboard.Model;

namespace Surreily.TheWorstSoundboard.Storage.Sound {
    public interface ISoundStorage {
        IList<SoundModel> GetSoundModels(string soundboardName);
    }
}
