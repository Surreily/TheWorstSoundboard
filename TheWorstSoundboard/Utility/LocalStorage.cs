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
    }
}
