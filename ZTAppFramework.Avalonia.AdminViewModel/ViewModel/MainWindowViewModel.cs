using Avalonia.FreeDesktop.DBusIme;
using Prism.Events;
using Prism.Ioc;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTAppFramework.Avalonia.Stared;
using ZTAppFramework.Avalonia.Stared.ViewModels;

namespace ZTAppFramework.Avalonia.AdminViewModel.ViewModel
{
    public class MainWindowViewModel : NavigaViewModelBase
    {
        private bool _OpenDrawer;
        private readonly IEventAggregator _aggregator;

        public bool IsDrawer
        {
            get { return _OpenDrawer; }
            set { SetProperty(ref _OpenDrawer, value); }
        }
        public MainWindowViewModel(IEventAggregator aggregator)
        {
            _aggregator = aggregator;
            _aggregator.GetEvent<NavPageMessage>().Subscribe(x=> IsDrawer = x);
        }
    }
}
