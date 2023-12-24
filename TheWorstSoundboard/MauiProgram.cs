using Microsoft.Extensions.Logging;
using Surreily.TheWorstSoundboard.Storage.Sound;
using Surreily.TheWorstSoundboard.Storage.Soundboard;
using Surreily.TheWorstSoundboard.Views.SoundboardEdit;
using Surreily.TheWorstSoundboard.Views.SoundboardList;
using Surreily.TheWorstSoundboard.Views.SoundEdit;

namespace Surreily.TheWorstSoundboard {
    public static class MauiProgram {
        public static MauiApp CreateMauiApp() {
            var builder = MauiApp.CreateBuilder()
                .RegisterViews()
                .RegisterViewModels()
                .RegisterStorage();

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts => {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder) {
            mauiAppBuilder.Services.AddTransient<SoundEditPage>();
            mauiAppBuilder.Services.AddTransient<SoundboardEditPage>();
            mauiAppBuilder.Services.AddTransient<SoundboardListPage>();

            return mauiAppBuilder;
        }

        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder) {
            mauiAppBuilder.Services.AddTransient<SoundEditPageViewModel>();
            mauiAppBuilder.Services.AddTransient<SoundboardEditPageViewModel>();
            mauiAppBuilder.Services.AddTransient<SoundboardListPageViewModel>();

            return mauiAppBuilder;
            
        }

        public static MauiAppBuilder RegisterStorage(this MauiAppBuilder mauiAppBuilder) {
            mauiAppBuilder.Services.AddSingleton<ISoundStorage, LocalSoundStorage>();
            mauiAppBuilder.Services.AddSingleton<ISoundboardStorage, LocalSoundboardStorage>();

            return mauiAppBuilder;
        }
    }
}
