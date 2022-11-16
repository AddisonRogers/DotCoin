using Avalonia;
using System;

namespace DotCoin3
{
    class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args) => BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace();
    }
}

// TODO A-Z etc sorting linked
/*
    TODO Show the predicted Price
    TODO Show crypto market cap as graph (Fully rely on cache and only search it once per boot as otherwise shit will hit the fan
    TODO Crypto Info
    TODO Error page suggestions for the search
TODO Search drop down and suggestion ??
TODO Reformat and speed up cache by logging everything into different sections ie all of the cryptos current and all of the cryptos history with dotsearch only updating the first one. Total every 5 seconds (as its one api request)*/