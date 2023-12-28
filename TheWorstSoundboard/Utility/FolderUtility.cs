using Surreily.TheWorstSoundboard.Model;

namespace Surreily.TheWorstSoundboard.Utility {
    public class FolderUtility {
        public IList<SoundModel> GetSoundModels(string folderPath) {
            IEnumerable<string> fileNames = Directory.GetFiles(folderPath)
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
                string soundExtension = Path.GetExtension(soundFileName);

                string? imageFileName = imageFileNames
                    .FirstOrDefault(imageFileName =>
                        Path.GetFileNameWithoutExtension(imageFileName) == name);

                string? imageExtension = imageFileName != null
                    ? Path.GetExtension(imageFileName)
                    : null;

                soundModels.Add(new SoundModel(name) {
                    SoundExtension = soundExtension,
                    ImageExtension = imageExtension,
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
            if (fileName.EndsWith(".png") || fileName.EndsWith(".jpg")) {
                return true;
            }

            return false;
        }
    }
}
