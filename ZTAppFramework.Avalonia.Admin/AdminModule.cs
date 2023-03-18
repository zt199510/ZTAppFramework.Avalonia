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
using ZTAppFramework.Avalonia.Stared.DefaultPage;

namespace ZTAppFramework.Avalonia.Admin
{
    public class AdminModule : IModule
    {
        private IZTDialogService ZTDialog;

        public void OnInitialized(IContainerProvider containerProvider)
        {
            ZTDialog = containerProvider.Resolve<IZTDialogService>();
          //  ZTDialog.RegisterDialog<OrganizeModify, OrganizeModifyViewModel>(AppPages.OrganizeModifyPage);
            ZTDialog.RegisterDialog<Message,MessageViewModel>(AppPages.MessagePage);
        }

        public void RegisterTypes(IContainerRegistry services)
        {
            services.RegisterSingleton<AppStartService>();
            services.RegisterApplicationManager();
            services.RegisterValidator();
            
            //页面
            services.RegisterForNavigation<MainWindow, MainWindowViewModel>(AppViews.MainName);
            services.RegisterForNavigation<Home, HomeViewModel>(AppPages.HomePage);
            services.RegisterForNavigation<Workbench, WorkbenchViewModel>(AppPages.WorkbenchPage);
            services.RegisterForNavigation<PersonalInfo, PersonalInfoViewModel>(AppPages.PersonalInfoPage);
            services.RegisterForNavigation<Organize, OrganizeViewModel>(AppPages.OrganizePage);
            services.RegisterForNavigation<Role, RoleViewModel>(AppPages.RolePage);
            services.RegisterForNavigation<SysPost, SysPostViewModel>(AppPages.SysPostPage);
            services.RegisterForNavigation<SysAdmin, SysAdminViewModel>(AppPages.SysAdminPage);
            services.RegisterForNavigation<SysLog, SysLogViewModel>(AppPages.SysLogPage);
            services.RegisterForNavigation<SysMenu, SysMenuViewModel>(AppPages.SysMenuPage);
            services.RegisterForNavigation<FillPage>(AppPages.FillPage);

            ///弹窗
            services.RegisterDialogWindow<DefaultWindow>("DefaultWindow");
            services.RegisterDialog<LoginView, LoginViewModel>(AppViews.LoginName);
            services.RegisterDialog<OrganizeModify, OrganizeModifyViewModel>(AppPages.OrganizeModifyPage);
            services.RegisterDialog<RoleModify, RoleModifyViewModel>(AppPages.RoleModifyPage);
            services.RegisterDialog<SysPostModify, SysPostModifyViewModel>(AppPages.SysPostModifyPage);
            services.RegisterDialog<SysAdminModify, SysAdminModifyViewModel>(AppPages.SysAdminModifyPage);
            services.RegisterDialog<Message, PrismMessageViewModel>(AppPages.MessagePage);



        }
    }
}
