using Surreily.TheWorstSoundboard.Model;

namespace Surreily.TheWorstSoundboard.Utility {
    public static class LocalStorage {
        public static async Task AddFileAsync(Stream stream, string fileName, string soundboardName, string soundName) {
            string destinationFolderPath = Path.Combine(
                FileSystem.Current.AppDataDirectory,
                soundboardName);

            Directory.CreateDirectory(destinationFolderPath);

            string destinationFilePath = Path.Combine(
                destinationFolderPath,
                soundName + Path.GetExtension(fileName));

            using (FileStream fileStream = new FileStream(destinationFilePath, FileMode.Create)) {
                await stream.CopyToAsync(fileStream);
            }
        }

        public static List<string> GetSoundboardNames() {
            string soundboardsFolderPath = FileSystem.Current.AppDataDirectory;

            return Directory.GetDirectories(soundboardsFolderPath)
                .Select(soundboardFolderPath => Path.GetFileName(soundboardFolderPath))
                .ToList();
        }

        public static IList<SoundModel> GetSoundModels(string soundboardName) {
            string soundboardFolderPath = Path.Combine(
                FileSystem.Current.AppDataDirectory,
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
