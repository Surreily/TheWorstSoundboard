namespace Surreily.TheWorstSoundboard.Model {
    public class SoundModel {
        public SoundModel(string name) {
            Name = name;
        }

        public string Name { get; set; }

        public string? SoundExtension { get; set; }

        public string? ImageExtension { get; set; }

        public bool HasSound => SoundExtension != null;

        public bool HasImage => ImageExtension != null;
    }
}
