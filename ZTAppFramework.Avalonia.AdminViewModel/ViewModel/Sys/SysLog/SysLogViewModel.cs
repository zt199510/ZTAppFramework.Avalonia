using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTAppFramework.ApplicationService.Service;
using ZTAppFramework.Avalonia.Stared.ViewModels;

namespace ZTAppFramework.Avalonia.AdminViewModel.ViewModel
{
    public class SysLogViewModel: NavigaViewModelBase
    {
        #region Ui
        private List<SysLogMenuModel> _MenuList;


        public List<SysLogMenuModel> MenuList
        {
            get { return _MenuList; }
            set { SetProperty(ref _MenuList, value); }
        }

        private List<SysLogModel> _SysLogList;

        public List<SysLogModel> SysLogList
        {
            get { return _SysLogList; }
            set { SetProperty(ref _SysLogList, value); }
        }
        #endregion
        #region Command
        public DelegateCommand CheckedAllCommand { get; }
        public DelegateCommand UnCheckedAllCommand { get; }
        public DelegateCommand CheckedCommand { get; }
        public DelegateCommand UncheckedCommand { get; }
        #endregion

        #region Service
        private readonly SysLogSerivce _SysLogSerivce;
        #endregion

        #region 属性

        #endregion
    }
}
