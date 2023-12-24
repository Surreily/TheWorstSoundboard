using Surreily.TheWorstSoundboard.Model;

namespace Surreily.TheWorstSoundboard.Storage.Soundboard {
    public class LocalSoundboardStorage : ISoundboardStorage {
        private const string SoundboardsFolderName = "Soundboards";

        public IList<SoundboardModel> GetSoundboardModels() {
            string soundboardsFolderPath = Path.Combine(
                FileSystem.Current.AppDataDirectory,
                SoundboardsFolderName);

            Directory.CreateDirectory(soundboardsFolderPath);

            return Directory.GetDirectories(soundboardsFolderPath)
                .Select(soundboardFolderPath => new SoundboardModel {
                    Name = Path.GetFileName(soundboardFolderPath),
                })
                .ToList();
        }

        public void CreateSoundboard(string soundboardName) {
            string soundboardFolderPath = Path.Combine(
                FileSystem.Current.AppDataDirectory,
                SoundboardsFolderName,
                soundboardName);

            Directory.CreateDirectory(soundboardFolderPath);
        }

        public void DeleteSoundboard(string soundboardName) {
            string soundboardFolderPath = Path.Combine(
                FileSystem.Current.AppDataDirectory,
                SoundboardsFolderName,
                soundboardName);

            Directory.Delete(soundboardFolderPath, true);
        }
    }
}
