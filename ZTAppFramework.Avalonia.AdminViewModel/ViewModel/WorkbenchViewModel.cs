using Avalonia.Threading;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTAppFramework.ApplicationService.Service;
using ZTAppFramework.Avalonia.AdminViewModel.Model;
using ZTAppFramework.Avalonia.Stared.ViewModels;

namespace ZTAppFramework.Avalonia.AdminViewModel.ViewModel
{
    public class WorkbenchViewModel: NavigaViewModelBase
    {
        #region UI
        private DateTime _CurrentTime;//当前时间

        public DateTime CurrentTime
        {
            get { return _CurrentTime; }
            set { SetProperty(ref _CurrentTime, value); }
        }

        private DeviceUseModel _DeviceUseModel;
        public DeviceUseModel DeviceUseModel
        {
            get { return _DeviceUseModel; }
            set { SetProperty(ref _DeviceUseModel, value); }
        }

        private List<MachineInfoModel> _Machines;

        public List<MachineInfoModel> Machines
        {
            get { return _Machines; }
            set { SetProperty(ref _Machines, value); }
        }

        private double _ColumnWidth;
        public double ColumnWidth
        {
            get { return _ColumnWidth; }
            set { SetProperty(ref _ColumnWidth, value); }
        }
        #endregion

        #region 服务
        private readonly WorkbenchService _workbenchService;
        #endregion

        #region 属性
        DispatcherTimer Timer = new DispatcherTimer();

        #endregion
        public WorkbenchViewModel(WorkbenchService workbenchService)
        {
            Timer.Interval = TimeSpan.FromSeconds(1);
            Timer.Tick += Timer_Tick; ;
            _workbenchService = workbenchService;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            CurrentTime = DateTime.Now;

            UpdateDeviceInfo();
        }

        async void UpdateDeviceInfo()
        {
            var r = await _workbenchService.GetMachineUse();
            if (r.Success)
            {
                DeviceUseModel = Map<DeviceUseModel>(r.data);
            }
        }

        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {
            Timer.Stop();
            base.OnNavigatedFrom(navigationContext);
        }


        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            Timer.Start();
            CurrentTime = DateTime.Now;
            UpdateDeviceInfo();
            var r = await _workbenchService.GetMachineInfo();
            if (r.Success)
                Machines = Map<List<MachineInfoModel>>(r.data);

        }
    }
}
