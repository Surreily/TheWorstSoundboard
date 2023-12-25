using Surreily.TheWorstSoundboard.Model;

namespace Surreily.TheWorstSoundboard.Storage.Sound {
    public interface ISoundStorage {
        IList<SoundModel> GetSoundModels(string soundboardName);

        Task SaveSoundFileAsync(
            string soundboardName, string soundName, string extension, Stream stream);
    }
}
