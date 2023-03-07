using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTAppFramework.ApplicationService.Service;
using ZTAppFramework.Avalonia.Stared;
using ZTAppFramework.Avalonia.Stared.ViewModels;

namespace ZTAppFramework.Avalonia.AdminViewModel.ViewModel
{
    public class HomeViewModel : NavigaViewModelBase
    {

        #region UI

        private List<MenuModel> _MenuList;
        public List<MenuModel> MenuList
        {
            get { return _MenuList; }
            set { SetProperty(ref _MenuList, value); }
        }
        private List<MenuModel> _PageList;
        public List<MenuModel> PageList
        {
            get { return _PageList; }
            set { SetProperty(ref _PageList, value); }
        }

        private MenuModel _SelectMenu;
        public MenuModel SelectMenu
        {
            get { return _SelectMenu; }
            set
            {
                if (SetProperty(ref _SelectMenu, value))
                {
                    PageList = value.Childer;
                }
            }
        }

        private MenuModel _SelectPage;
        public MenuModel SelectPage
        {
            get { return _SelectPage; }
            set
            {
                if (SetProperty(ref _SelectPage, value))
                {
                    if (value == null) return;
                    switch (value.name)
                    {
                        case "个人信息":
                           //_RegionManager?.Regions[AppPages.Nav_HomeContent]?.RequestNavigate(AppPages.UserCenterName);
                            break;
                        case "工作台":
                            _RegionManager?.Regions[AppPages.Nav_HomeContent]?.RequestNavigate(AppPages.WorkbenchPage); break;
                        case "机构管理":
                            _RegionManager?.Regions[AppPages.Nav_HomeContent]?.RequestNavigate(AppPages.OrganizePage);
                            break;
                        case "角色管理":
                           _RegionManager?.Regions[AppPages.Nav_HomeContent]?.RequestNavigate(AppPages.RolePage); break;
                        //case "职位管理":
                        //    _RegionManager?.Regions[AppView.HomeName]?.RequestNavigate(AppView.SysPostName); break;
                        //case "用户管理":
                        //    _RegionManager?.Regions[AppView.HomeName]?.RequestNavigate(AppView.SysAdminName); break;
                        //case "资源管理":
                        //    _RegionManager?.Regions[AppView.HomeName]?.RequestNavigate(AppView.SysMenuName); break;
                        //case "权限设置":
                        //    _RegionManager?.Regions[AppView.HomeName]?.RequestNavigate(AppView.SysAuthorizeName); break;
                        //case "系统日志":
                        //    _RegionManager?.Regions[AppView.HomeName]?.RequestNavigate(AppView.SysLogsName); break;
                        default:
                            //_RegionManager?.Regions[AppView.HomeName]?.RequestNavigate(AppView.UserPerferfabName); 
                            
                            break;

                    }
                    //if (DisplayMenus == null)
                    //    DisplayMenus = new DisplayMenuModel();

                    //DisplayMenus.MenuName = SelectMenu.name;
                    //DisplayMenus.PageName = SelectPage.name;
                }
            }
        }
        #endregion

        #region 服务
        private readonly MenuService _menuService;
        private readonly IRegionManager _RegionManager;
        #endregion




        public HomeViewModel(MenuService menuService, IRegionManager regionManager)
        {
            _menuService = menuService;
            _RegionManager = regionManager;
        }
        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            var Ar = await _menuService.GetMenuList();
            if (Ar.Success)
            {
                MenuList = Map<List<MenuModel>>(Ar.data);
                SelectMenu = MenuList.First();
                SelectPage = MenuList.First().Childer.First();
                MenuList.First().IsSelected = true;
                SelectPage.IsSelected = true;

            }
        }
    }
}
