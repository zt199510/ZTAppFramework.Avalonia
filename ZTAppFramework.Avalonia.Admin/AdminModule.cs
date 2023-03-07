using Prism.Ioc;
using Prism.Modularity;
using ZTAppFramework.ApplicationService.Stared.HttpManager;
using ZTAppFramework.ApplicationService.Stared;
using ZTAppFramework.Avalonia.Admin.Views;
using ZTAppFramework.Avalonia.Admin.Windows;
using ZTAppFramework.Avalonia.AdminViewModel;
using ZTAppFramework.Avalonia.AdminViewModel.ViewModel;
using ZTAppFramework.Avalonia.Stared;
using ZTAppFramework.Template.Dialog;
using ZTAppFramework.Avalonia.AdminViewModel.Extensions;

namespace ZTAppFramework.Avalonia.Admin
{
    public class AdminModule : IModule
    {
        private IZTDialogService ZTDialog;

        public void OnInitialized(IContainerProvider containerProvider)
        {
            ZTDialog = containerProvider.Resolve<IZTDialogService>();
            ZTDialog.RegisterDialog<OrganizeModify>(AppPages.OrganizeModifyPage);
        }

        public void RegisterTypes(IContainerRegistry services)
        {
            services.RegisterSingleton<AppStartService>();
            services.RegisterSingleton<AccessTokenManager>();
            services.RegisterSingleton<ApiClinetRepository>();
            services.RegisterApplicationManager();
            services.RegisterValidator();
            
            //页面
            services.RegisterForNavigation<MainWindow, MainWindowViewModel>(AppViews.MainName);
            services.RegisterForNavigation<Home, HomeViewModel>(AppPages.HomePage);
            services.RegisterForNavigation<Workbench, WorkbenchViewModel>(AppPages.WorkbenchPage);
            services.RegisterForNavigation<PersonalInfo, PersonalInfoViewModel>(AppPages.PersonalInfoPage);
            services.RegisterForNavigation<Organize, OrganizeViewModel>(AppPages.OrganizePage);
          //  
            ///弹窗
            services.RegisterDialogWindow<DefaultWindow>("DefaultWindow");
            services.RegisterDialog<LoginView, LoginViewModel>(AppViews.LoginName);
            services.RegisterDialog<OrganizeModify, OrganizeModifyViewModel>(AppPages.OrganizeModifyPage);
        }
    }
}
