using Microsoft.Maui.Storage;
using Surreily.TheWorstSoundboard.Model;

namespace Surreily.TheWorstSoundboard.Storage.Sound {
    public class LocalSoundStorage : ISoundStorage {
        private const string SoundboardsFolderName = "Soundboards";

        public IList<SoundModel> GetSoundModels(string soundboardName) {
            string soundboardFolderPath = Path.Combine(
                FileSystem.Current.AppDataDirectory,
                SoundboardsFolderName,
                soundboardName);

            IEnumerable<string> fileNames = Directory.GetFiles(soundboardFolderPath)
                .Select(filePath => Path.GetFileName(filePath))
                .ToList();

            IEnumerable<string> soundFileNames = fileNames
                .Where(fileName => GetIsSoundFileName(fileName))
                .ToList();

            IEnumerable<string> imageFileNames = fileNames
                .Where(fileName => GetIsImageFileName(fileName))
                .ToList();

            IList<SoundModel> soundModels = new List<SoundModel>();

            foreach (string soundFileName in soundFileNames) {
                string name = Path.GetFileNameWithoutExtension(soundFileName);

                string? imageFileName = imageFileNames
                    .FirstOrDefault(imageFileName =>
                        Path.GetFileNameWithoutExtension(imageFileName) == name);

                soundModels.Add(new SoundModel {
                    Name = name,
                    SoundFileName = soundFileName,
                    ImageFileName = imageFileName,
                });
            }

            return soundModels;
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

        private static bool GetIsSoundFileName(string fileName) {
            if (fileName.EndsWith(".mp3")) {
                return true;
            }

            return false;
        }

        private static bool GetIsImageFileName(string fileName) {
            if (fileName.EndsWith(".png")) {
                return true;
            }

            return false;
        }
    }
}
