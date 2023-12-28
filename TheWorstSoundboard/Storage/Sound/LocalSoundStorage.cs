using Surreily.TheWorstSoundboard.Model;
using Surreily.TheWorstSoundboard.Utility;

namespace Surreily.TheWorstSoundboard.Storage.Sound {
    public class LocalSoundStorage : ISoundStorage {
        private const string SoundboardsFolderName = "Soundboards";

        private FolderUtility folderUtility;

        public LocalSoundStorage(FolderUtility folderUtility) {
            this.folderUtility = folderUtility;
        }

        public IList<SoundModel> GetSoundModels(string soundboardName) {
            string soundboardFolderPath = Path.Combine(
                FileSystem.Current.AppDataDirectory,
                SoundboardsFolderName,
                soundboardName);

            return folderUtility.GetSoundModels(soundboardFolderPath);
        }

        public string GetSoundFilePath(string soundboardName, string soundName, string extension) {
            return Path.Combine(
                FileSystem.Current.AppDataDirectory,
                SoundboardsFolderName,
                soundboardName,
                soundName + extension);
        }

        public async Task SaveSoundFileAsync(
            string soundboardName, string soundName, string extension, Stream stream) {

            string soundboardFolderPath = Path.Combine(
                FileSystem.Current.AppDataDirectory,
                SoundboardsFolderName,
                soundboardName);

            Directory.CreateDirectory(soundboardFolderPath);

            string filePath = Path.Combine(
                soundboardFolderPath,
                soundName + extension);

            using (FileStream fileStream = new FileStream(filePath, FileMode.Create)) {
                await stream.CopyToAsync(fileStream);
            }
        }
    }
}
