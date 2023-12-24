using Surreily.TheWorstSoundboard.Views.SoundboardEdit;

namespace Surreily.TheWorstSoundboard {
    public partial class AppShell : Shell {
        public AppShell() {
            InitializeComponent();

            Routing.RegisterRoute("Soundboards/Edit", typeof(SoundboardEditPage));
        }
    }
}
