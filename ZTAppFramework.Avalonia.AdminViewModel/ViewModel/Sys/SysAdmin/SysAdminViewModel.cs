using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTAppFramework.ApplicationService.Service;
using ZTAppFramework.Avalonia.Stared.ViewModels;

namespace ZTAppFramework.Avalonia.AdminViewModel.ViewModel
{
    public class SysAdminViewModel: NavigaViewModelBase
    {

        #region UI
        private List<SysAdminModel> _SysAdminList;
        public List<SysAdminModel> SysAdminList
        {
            get { return _SysAdminList; }
            set { SetProperty(ref _SysAdminList, value); }
        }

        private ObservableCollection<SysRoleModel> _sysRoles = new ObservableCollection<SysRoleModel>();
        public ObservableCollection<SysRoleModel> SysRoles
        {
            get { return _sysRoles; }
            set { SetProperty(ref _sysRoles, value); }
        }

        private List<SysAdminModel> _SelectList = new List<SysAdminModel>();
        public List<SysAdminModel> SelectList
        {
            get { return _SelectList; }
            set { SetProperty(ref _SelectList, value); }
        }

        private string _QueryStr;

        public string QueryStr
        {
            get { return _QueryStr; }
            set { SetProperty(ref _QueryStr, value); }
        }
        #endregion

        #region Command
        public DelegateCommand AddCommand { get; }
        public DelegateCommand DeleteSelectCommand { get; }
        public DelegateCommand CheckedAllCommand { get; }
        public DelegateCommand UnCheckedAllCommand { get; }

        public DelegateCommand QueryCommand { get; }

        public DelegateCommand<SysAdminModel> ModifCommand { get; }

        public DelegateCommand<SysAdminModel> DeleteSeifCommand { get; }

        public DelegateCommand<SysAdminModel> CheckedCommand { get; }

        public DelegateCommand<SysAdminModel> UncheckedCommand { get; }

        public DelegateCommand<SysRoleModel> QueryParamCommand { get; }
        #endregion

        #region Service
        private readonly SysAdminService _sysAdminService;
        private readonly RoleService _SysroleService;
        #endregion

        public SysAdminViewModel(RoleService SysroleService, SysAdminService sysAdminService)
        {
            _sysAdminService = sysAdminService;
            _SysroleService = SysroleService;
          
        }
    }
}
