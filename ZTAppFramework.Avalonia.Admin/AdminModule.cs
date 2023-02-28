using Prism.Ioc;
using Prism.Modularity;
using ZTAppFramework.Avalonia.Admin.Views;
using ZTAppFramework.Avalonia.Admin.Windows;
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
           
            services.RegisterForNavigation<MainWindow, MainWindowViewModel>(AppViews.MainName);

            services.RegisterDialogWindow<DefaultWindow>("DefaultWindow");
            
            services.RegisterDialog<LoginView, LoginViewModel>(AppViews.LoginName);
        }
    }
}
