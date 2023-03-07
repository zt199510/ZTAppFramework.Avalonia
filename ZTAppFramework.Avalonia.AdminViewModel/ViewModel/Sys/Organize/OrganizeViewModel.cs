using Prism.Commands;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTAppFramework.ApplicationService.Service;
using ZTAppFramework.Avalonia.AdminViewModel.Model;
using ZTAppFramework.Avalonia.Stared;
using ZTAppFramework.Avalonia.Stared.ViewModels;
using ZTAppFramework.Template.Dialog;

namespace ZTAppFramework.Avalonia.AdminViewModel.ViewModel
{
    public class OrganizeViewModel : NavigaViewModelBase
    {

        #region UI
        private List<SysOrganizeModel> _OrganizesList;
        public List<SysOrganizeModel> OrganizesList
        {
            get { return _OrganizesList; }
            set { SetProperty(ref _OrganizesList, value); }
        }

        private List<SysOrganizeModel> _SelectList = new List<SysOrganizeModel>();
        public List<SysOrganizeModel> SelectList
        {
            get { return _SelectList; }
            set { SetProperty(ref _SelectList, value); }
        }



        #endregion
        #region Command
        public DelegateCommand AddCommand => new DelegateCommand(Add);
        public DelegateCommand DeleteSelectCommand => new DelegateCommand(DeleteSelect);
        public DelegateCommand CheckedAllCommand { get; }
        public DelegateCommand UnCheckedAllCommand { get; }
        public DelegateCommand QueryCommand { get; }
        public DelegateCommand<SysOrganizeModel> ModifCommand => new DelegateCommand<SysOrganizeModel>(Modif);
        public DelegateCommand<SysOrganizeModel> DeleteSeifCommand => new DelegateCommand<SysOrganizeModel>(DeleteSeif);
        #endregion
        #region 服务
        private readonly OrganizeService _OrganizeService;
        private readonly IDialogService _DialogService;
        #endregion
        public OrganizeViewModel(OrganizeService organizeService, IDialogService DialogService)
        {
            _OrganizeService = organizeService;
            _DialogService = DialogService;
        }
        #region Execute
        void Modif(SysOrganizeModel Param)
        {
            DialogParameters dialogParameter = new DialogParameters();
            dialogParameter.Add("Title", "编辑");
            dialogParameter.Add("Param", Param);
            _DialogService.ShowDialog(AppPages.OrganizeModifyPage, dialogParameter, async r =>
            {
                if (r.Result == ButtonResult.Yes)
                {
                    await GetOrganizeInfo();
                }
                else
                {

                }
                await GetOrganizeInfo();
            }, "DefaultWindow");
        }
        void DeleteSeif(SysOrganizeModel Param)
        {
            ShowDialog("提示", "确定要删除码", async x =>
            {
                if (x.Result == ButtonResult.Yes)
                {

                    List<string> strings = new List<string>();
                    var rd = OrganizesList.Where(x => x.ParentIdList.Contains(Param.Id.ToString()));
                    if (rd != null)
                    {
                        foreach (var item in rd)
                            strings.Add(item.Id.ToString());
                    }
                    strings.Add(Param.Id.ToString());
                    string DelIdStr = string.Join(',', strings);
                    var r = await _OrganizeService.Delete(DelIdStr);
                    if (r.Success)
                    {
                        Show("消息", "删除成功!");
                        await GetOrganizeInfo();
                        return;
                    }
                }
            });
        }

        void Add()
        {

            _DialogService.ShowDialog(AppPages.OrganizeModifyPage, new DialogParameters(), async r =>
            {
                if (r.Result == ButtonResult.Yes)
                {

                }
                else
                {

                }
                await GetOrganizeInfo();
            }, "DefaultWindow");
        }
        void DeleteSelect()
        {

        }
        async Task GetOrganizeInfo(string Query = "")
        {
            var r = await _OrganizeService.GetList(Query);
            if (r.Success)
                OrganizesList = Map<List<SysOrganizeModel>>(r.data).OrderBy(X => X.Sort).ToList();
            SelectList.Clear();
        }
        #endregion


        #region Override

        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            await GetOrganizeInfo();
        }
        #endregion

    }
}
