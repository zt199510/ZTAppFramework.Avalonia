using Prism.Commands;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
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
    public class SysPostViewModel: NavigaViewModelBase
    {
        #region UI
        private List<SysPostModel> _SysPostList;
        public List<SysPostModel> SysPostList
        {
            get { return _SysPostList; }
            set { SetProperty(ref _SysPostList, value); }
        }

        private List<SysPostModel> _SelectList = new List<SysPostModel>();
        public List<SysPostModel> SelectList
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
        public DelegateCommand<SysPostModel> ModifCommand => new DelegateCommand<SysPostModel>(Modif);
        public DelegateCommand<SysPostModel> DeleteSeifCommand => new DelegateCommand<SysPostModel>(DeleteSeif);
        public DelegateCommand<SysPostModel> CheckedCommand { get; }
        public DelegateCommand<SysPostModel> UncheckedCommand { get; }
        #endregion

     
        #region 服务
        private readonly SysPostService _SysPostService;
        private readonly IDialogService _DialogService;
        #endregion


        public SysPostViewModel(SysPostService sysPostService, IDialogService DialogService)
        {
            _SysPostService = sysPostService;
            _DialogService = DialogService;
        }



        #region Event

        private async void Query()
        {
            await SetBusyAsync(async () =>
            {
                await GetListInfo(QueryStr);
            });

        }

        void UnCheckedAll()
        {
            foreach (var item in SysPostList)
            {
                item.IsSelected = false;
                SelectList.Remove(item);
            }
        }
        void CheckedAll()
        {
            foreach (var item in SysPostList)
            {

                item.IsSelected = true;
                SelectList.Add(item);
            }
        }
        void Unchecked(SysPostModel Param) => SelectList.Remove(Param);
        void Checked(SysPostModel Param) => SelectList.Add(Param);
        void DeleteSelect()
        {
            if (SelectList.Count <= 0)
            {
                Show("消息", "请选择要删除得数据");
                return;
            }
            ShowDialog("提示", $"确定要删除{SelectList.Count()}个数据吗？如果删除项中含有子集将会被一并删除", async x =>
            {
                if (x.Result == ButtonResult.Yes)
                {
                    string DelIdStr = string.Join(',', SelectList.Select(X => X.Id));
                    var r = await _SysPostService.Delete(DelIdStr);
                    if (r.Success)
                    {
                        Show("消息", "删除成功!");
                        await GetListInfo();
                        return;
                    }
                }
            }, Stared.Enums.MessageEnums.YesOrNO);
        }
        void Modif(SysPostModel Param)
        {
            DialogParameters dialogParameter = new DialogParameters();
            dialogParameter.Add("Title", "编辑");
            dialogParameter.Add("Param", Param);
            _DialogService.ShowDialog(AppPages.SysPostModifyPage, dialogParameter, async x =>
            {
                if (x.Result == ButtonResult.Yes)
                {
                    await GetListInfo();
                }
            }, "DefaultWindow");
        }
        void DeleteSeif(SysPostModel Param)
        {
            ShowDialog("提示", "确定要删除码", async x =>
            {
                if (x.Result == ButtonResult.Yes)
                {
                    var r = await _SysPostService.Delete(Param.Id.ToString());
                    if (r.Success)
                    {
                        Show("消息", "删除成功!");
                        await GetListInfo();
                        return;
                    }
                }
            }, Stared.Enums.MessageEnums.YesOrNO);
        }
        void Add()
        {
            _DialogService.ShowDialog(AppPages.SysPostModifyPage, new DialogParameters(), async r =>
            {
                if (r.Result == ButtonResult.Yes)
                {
                    await GetListInfo();
                }
                else
                {

                }
             
            }, "DefaultWindow");
        }

        async Task GetListInfo(string Query = "")
        {
            var r = await _SysPostService.GetPostList(new PageParam() { Key = Query == "" ? null : Query });
            if (r.Success)
            {
                SysPostList = Map<List<SysPostModel>>(r.data.Items).OrderBy(X => X.Sort).ToList();
            }

            SelectList.Clear();
        }
        #endregion

        #region Override


        public override async Task OnNavigatedToAsync(NavigationContext navigationContext = null)
        {
            await GetListInfo();
        }


        #endregion
    }
}
