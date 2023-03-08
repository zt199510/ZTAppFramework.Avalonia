using System.Runtime.Versioning;
using Avalonia;
using Avalonia.Browser;
using Avalonia.ReactiveUI;
using app;

[assembly: SupportedOSPlatform("browser")]

internal partial class Program
{
    private static void Main(string[] args) => BuildAvaloniaApp()
          .UseSkia()
        .UseReactiveUI()
        .SetupBrowserApp("out");

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>();
}