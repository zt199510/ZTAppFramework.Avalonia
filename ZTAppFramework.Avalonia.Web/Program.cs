using Avalonia;
using Avalonia.Controls;
using Avalonia.Web;
using Avalonia.Web.Blazor;
using System.Runtime.Versioning;
using ZTAppFramework.Avalonia;

[assembly: SupportedOSPlatform("browser")]

internal partial class Program
{
    private static void Main(string[] args) => BuildAvaloniaApp()
        .UsePlatformDetect()
        .StartWithClassicDesktopLifetime(args);

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>();
}


