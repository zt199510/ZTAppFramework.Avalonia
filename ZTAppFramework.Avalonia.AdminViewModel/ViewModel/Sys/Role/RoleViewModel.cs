using Prism.Commands;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTAppFramework.ApplicationService.Service;
using ZTAppFramework.Avalonia.Stared;
using ZTAppFramework.Avalonia.Stared.ViewModels;
using ZTAppFramework.Template.Dialog;

namespace ZTAppFramework.Avalonia.AdminViewModel.ViewModel
{
    public class RoleViewModel: NavigaViewModelBase
    {

        #region UI
        private List<SysRoleModel> _RoleList;
        public List<SysRoleModel> RoleList
        {
            get { return _RoleList; }
            set { SetProperty(ref _RoleList, value); }
        }

        private List<SysRoleModel> _SelectList = new List<SysRoleModel>();
        public List<SysRoleModel> SelectList
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
        public DelegateCommand<SysRoleModel> ModifCommand => new DelegateCommand<SysRoleModel>(Modif);
        public DelegateCommand<SysRoleModel> DeleteSeifCommand => new DelegateCommand<SysRoleModel>(DeleteSeif);
        public DelegateCommand<SysRoleModel> CheckedCommand { get; }
        public DelegateCommand<SysRoleModel> UncheckedCommand { get; }
        #endregion

        #region 服务
        private readonly RoleService _RoleService;
        private readonly IDialogService _DialogService;
        #endregion


        public RoleViewModel(RoleService  roleService, IDialogService DialogService)
        {
            _RoleService = roleService;
            _DialogService = DialogService;
        }

        #region Execute
        void Modif(SysRoleModel Param)
        {
            DialogParameters dialogParameter = new DialogParameters();
            dialogParameter.Add("Title", "编辑");
            dialogParameter.Add("Param", Param);
            _DialogService.ShowDialog(AppPages.RoleModifyPage, dialogParameter, async r =>
            {
                if (r.Result == ButtonResult.Yes)
                {
                    await GetRoleInfo();
                }
                else
                {

                }
                await GetRoleInfo();
            }, "DefaultWindow");
        }

        void DeleteSeif(SysRoleModel Param)
        {
            ShowDialog("提示", "确定要删除码", async x =>
            {
                if (x.Result == ButtonResult.Yes)
                {

                    List<string> strings = new List<string>();
                    var rd = RoleList.Where(x => x.ParentIdList.Contains(Param.Id.ToString()));
                    if (rd != null)
                    {
                        foreach (var item in rd)
                            strings.Add(item.Id.ToString());
                    }
                    strings.Add(Param.Id.ToString());
                    string DelIdStr = string.Join(',', strings);
                    var r = await _RoleService.Delete(DelIdStr);
                    if (r.Success)
                    {
                        Show("消息", "删除成功!");
                        await GetRoleInfo();
                        return;
                    }
                }
            }, Stared.Enums.MessageEnums.YesOrNO);
        }

        void Add()
        {

            _DialogService.ShowDialog(AppPages.RoleModifyPage, new DialogParameters(), async r =>
            {
                if (r.Result == ButtonResult.Yes)
                {

                }
                else
                {

                }
                await GetRoleInfo();
            }, "DefaultWindow");
        }
        async Task GetRoleInfo(string Query = "")
        {
            var r = await _RoleService.GetList(Query);
            if (r.Success)
                RoleList = Map<List<SysRoleModel>>(r.data).OrderBy(X => X.Sort).ToList();
            SelectList.Clear();
        }
        #endregion


        #region Override
        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
          
            await GetRoleInfo();
        }
        #endregion




    }



}
