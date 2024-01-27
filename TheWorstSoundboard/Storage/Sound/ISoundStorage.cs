using Surreily.TheWorstSoundboard.Model;

namespace Surreily.TheWorstSoundboard.Storage.Sound {
    public interface ISoundStorage {
        IList<SoundModel> GetSoundModels(string soundboardName);

        string GetSoundFilePath(string soundboardName, string soundName, string extension);

        Task SaveSoundFileAsync(string soundboardName, string soundName, string extension, Stream stream);

        void DeleteSound(string soundboardName, string soundName);
    }
}
