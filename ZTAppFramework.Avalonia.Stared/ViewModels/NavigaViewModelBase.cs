using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTAppFramework.Avalonia.Stared.ViewModels
{
    /// <summary>
    /// 页面
    /// </summary>
    public class NavigaViewModelBase : BasicsViewModelBase, INavigationAware, IRegionMemberLifetime, IConfirmNavigationRequest
    {
        /// <summary>
        /// 导航器
        /// </summary>
        public IRegionManager Region;
        /// <summary>
        /// 弹窗服务
        /// </summary>
        public IDialogService Dialog;
        /// <summary>
        /// 事件聚合器
        /// </summary>
        public IEventAggregator Event;
        /// <summary>
        /// VM基类
        /// </summary>
        public NavigaViewModelBase()
        {
            this.Region = ContainerLocator.Container.Resolve<IRegionManager>();
            this.Dialog = ContainerLocator.Container.Resolve<IDialogService>();
            this.Event = ContainerLocator.Container.Resolve<IEventAggregator>();
           
        }
        private DelegateCommand _LoadedCommand;
        public DelegateCommand LoadedCommand =>
            _LoadedCommand ?? (_LoadedCommand = new DelegateCommand(ExecuteLoadedCommand));
        /// <summary>
        ///初始化界面加载
        /// </summary>
        public virtual void ExecuteLoadedCommand()
        {

        }

        public void OpenDrawer(string Page,NavigationParameters Param=null)
        {
            Event.GetEvent<NavPageMessage>().Publish(true);
            Region.Regions[AppPages.Nav_RightDrawerContent]?.RequestNavigate(Page, Param);
        }

        /// <summary>
        /// 标记上一个视图时候被销毁
        /// </summary>
        public bool KeepAlive => false;

        public virtual void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            continuationCallback(true);
        }
        /// <summary>
        /// 导航后的目标视图是否缓存
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <returns></returns>
        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }
        /// <summary>
        /// 导航前
        /// </summary>
        /// <param name="navigationContext"></param>
        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
        /// <summary>
        /// 导航后
        /// </summary>
        /// <param name="navigationContext"></param>
        public virtual async void OnNavigatedTo(NavigationContext navigationContext)
        {
            await OnNavigatedToAsync(navigationContext);
        }


        /// <summary>
        /// 异步刷新方法,当页面导航到达时触发该方法
        /// </summary>
        /// <returns></returns>
        public virtual async Task OnNavigatedToAsync(NavigationContext navigationContext = null) => await Task.CompletedTask;
    }
}
