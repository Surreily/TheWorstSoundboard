using Surreily.TheWorstSoundboard.Views.SoundboardEdit;
using Surreily.TheWorstSoundboard.Views.SoundEdit;

namespace Surreily.TheWorstSoundboard {
    public partial class AppShell : Shell {
        public AppShell() {
            InitializeComponent();

            Routing.RegisterRoute("Soundboards/Edit", typeof(SoundboardEditPage));
            Routing.RegisterRoute("Sounds/Edit", typeof(SoundEditPage));
        }
    }
}
