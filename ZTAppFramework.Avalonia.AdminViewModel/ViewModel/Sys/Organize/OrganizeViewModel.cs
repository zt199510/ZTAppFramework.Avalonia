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
        void Add()
        {
            //_DialogService.Show("Message", null, res =>
            //{
            //    var data = res;
            //}, "HooKDialog");

            _DialogService.ShowDialog(AppPages.OrganizeModifyPage, new DialogParameters(), r =>
            {
                if (r.Result == ButtonResult.Yes)
                {
                  
                }
                else
                {
                   
                }
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
