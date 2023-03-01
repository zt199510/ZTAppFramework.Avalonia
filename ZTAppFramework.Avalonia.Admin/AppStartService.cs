using Avalonia;
using Avalonia.Controls;
using ZTAppFramework.Avalonia.Stared;
using Prism.Ioc;
using Prism.Regions;
using Avalonia.Controls.ApplicationLifetimes;

namespace ZTAppFramework.Avalonia.Admin
{
    public class AppStartService
    {
        Application App;
        public void Exit()
        {
            Environment.Exit(0);
        }
        
        public Window CreateShell(Application app)
        {
            App = app;
         
            var container = ContainerLocator.Container;
            var shell = container.Resolve<object>(AppViews.MainName);
            if (shell is Window view)
            {
                if (view.DataContext is INavigationAware navigationAware)
                {
                    navigationAware.OnNavigatedTo(null);
                    return (Window)shell;
                }
            }
            return null;
        }

        public static void Authorization()
        {
          
            if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime ApplicationLifetime)
            {
             
            }
        }
        public static void ExitApplication() => Environment.Exit(0);



    }
}
