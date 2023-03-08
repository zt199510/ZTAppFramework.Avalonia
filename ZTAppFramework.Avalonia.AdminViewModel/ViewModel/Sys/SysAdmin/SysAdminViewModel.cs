using Prism.Commands;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTAppFramework.ApplicationService.Service;
using ZTAppFramework.ApplicationService.Stared.Commom;
using ZTAppFramework.Avalonia.Stared;
using ZTAppFramework.Avalonia.Stared.ViewModels;
using ZTAppFramework.Template.Dialog;

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
        public DelegateCommand AddCommand => new DelegateCommand(Add);
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
        private readonly IDialogService _DialogService;
        #endregion

        public SysAdminViewModel(RoleService SysroleService, SysAdminService sysAdminService, IDialogService DialogService)
        {
            _sysAdminService = sysAdminService;
            _SysroleService = SysroleService;
            _DialogService = DialogService;


        }

        #region 私有
        void Add()
        {
            _DialogService.ShowDialog(AppPages.SysAdminModifyPage, new DialogParameters(), async r =>
            {
                if (r.Result == ButtonResult.Yes)
                {
                    await GetListInfo();
                    SysRoles.First().IsSelected = true;
                }
                else
                {

                }
            }, "DefaultWindow");
        }

        private async void QueryParam(SysRoleModel Param)
        {
            if (Param.Id == 0)
                await GetListInfo();
            else
                await GetListInfo("", Param.Id);

        }

        private async void Query()
        {
            await SetBusyAsync(async () =>
            {
                await GetListInfo(QueryStr);
            });

        }
        async Task GetListInfo(string Key = "", long Id = 0)
        {
            var r = await _sysAdminService.GetPostList(new PageParam()
            {
                Id = Id == 0 ? null : Id,
                Key = Key == "" ? null : Key
            });

            if (r.Success)
            {
                SysAdminList = Map<List<SysAdminModel>>(r.data.Items);
            }

            SelectList.Clear();
        }
        async Task GetSysRoleList()
        {
            SysRoles.Clear();
            var r = await _SysroleService.GetList("");
            if (r.Success)
            {
                var list = Map<List<SysRoleModel>>(r.data).OrderBy(X => X.Sort).ToList();
                foreach (var item in list)
                {
                    var info = list.FirstOrDefault(x => x.Id == item.ParentId);
                    if (info != null)
                    {
                        info.Childer = info.Childer ?? new List<SysRoleModel>();
                        info.Childer.Add(item);
                    }
                }
                SysRoles.Add(new SysRoleModel() { Name = "所有", Id = 0 });
                SysRoles.AddRange(list.Where(x => x.ParentId == 0));
                SysRoles.First().IsSelected = true;
            }
        }
        #endregion

        #region Override
        public override async Task OnNavigatedToAsync(NavigationContext navigationContext = null)
        {
            await GetListInfo();
            await GetSysRoleList();
        }


        #endregion
    }
}
