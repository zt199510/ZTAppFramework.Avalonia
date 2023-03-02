using Prism.Ioc;
using Prism.Modularity;
using ZTAppFramework.ApplicationService.Stared.HttpManager;
using ZTAppFramework.ApplicationService.Stared;
using ZTAppFramework.Avalonia.Admin.Views;
using ZTAppFramework.Avalonia.Admin.Windows;
using ZTAppFramework.Avalonia.AdminViewModel;
using ZTAppFramework.Avalonia.AdminViewModel.ViewModel;
using ZTAppFramework.Avalonia.Stared;

namespace ZTAppFramework.Avalonia.Admin
{
    public class AdminModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }

        public void RegisterTypes(IContainerRegistry services)
        {
            services.RegisterSingleton<AppStartService>();
            services.RegisterSingleton<AccessTokenManager>();
            services.RegisterSingleton<ApiClinetRepository>();
            services.RegisterApplicationManager();
            services.RegisterValidator();
            services.RegisterForNavigation<MainWindow, MainWindowViewModel>(AppViews.MainName);
            services.RegisterForNavigation<Home, HomeViewModel>(AppPages.HomePage);
            services.RegisterForNavigation<Workbench, WorkbenchViewModel>(AppPages.WorkbenchPage);
            services.RegisterDialogWindow<DefaultWindow>("DefaultWindow");
            services.RegisterDialog<LoginView, LoginViewModel>(AppViews.LoginName);
        }
    }
}
