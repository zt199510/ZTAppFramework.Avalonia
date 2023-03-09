using Avalonia;
using Avalonia.Controls;
using ZTAppFramework.Avalonia.Stared;
using Prism.Ioc;
using Prism.Regions;
using Avalonia.Controls.ApplicationLifetimes;
using ZTAppFramework.Avalonia.Admin.Views;

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
            if (shell is MainWindow view)
            {
                IRegionManager? regionManager = container.Resolve<IRegionManager>();
                RegionManager.SetRegionName(view.RegionPage, AppPages.Nav_MainContent);
                RegionManager.SetRegionName(view.RightRegionPage, AppPages.Nav_RightDrawerContent);
                RegionManager.SetRegionManager(view.RegionPage, regionManager);
                RegionManager.SetRegionManager(view.RightRegionPage, regionManager);
                RegionManager.UpdateRegions();
                regionManager.Regions[AppPages.Nav_MainContent].RequestNavigate(AppPages.HomePage);
                if (view.DataContext is INavigationAware navigationAware)
                {
                    navigationAware.OnNavigatedTo(null);
                    return (Window)shell;
                }
            }
            return null;
        }

    
        public static void ExitApplication() => Environment.Exit(0);



    }
}
