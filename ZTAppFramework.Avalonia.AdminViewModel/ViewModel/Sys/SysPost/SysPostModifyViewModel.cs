using Prism.Commands;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTAppFramework.ApplicationService.Service;
using ZTAppFramework.ApplicationService.Stared;
using ZTAppFramework.Avalonia.Stared.ViewModels;
using ZTAppFramework.Template.Dialog;

namespace ZTAppFramework.Avalonia.AdminViewModel.ViewModel
{
    public class SysPostModifyViewModel : DialogViewModelBase
    {

        #region UI



        private SysPostModel _PostModel;


        public SysPostModel PostModel
        {
            get { return _PostModel; }
            set { SetProperty(ref _PostModel, value); }
        }

        #endregion

        #region Command
        public DelegateCommand<string> StatusChanageCommand { get; }
        #endregion

        #region Service
        private readonly SysPostService _sysPostService;
        #endregion

        #region 属性
        public bool IsEdit { get; set; }
        #endregion
        public SysPostModifyViewModel(SysPostService sysPostService)
        {
            _sysPostService = sysPostService;
            StatusChanageCommand = new(StatusChanage);
        }
        #region Event
        private void StatusChanage(string Param)
        {
            try
            {
                bool Status = bool.Parse(Param);
                PostModel.Status = Status;
            }
            catch (Exception)
            {


            }
        }


        async Task<bool> Add()
        {

            var Version = Verify(Map<SysPostParm>(PostModel));
            if (!Version.IsValid)
            {
                Show("提示", string.Join('\n', Version.Errors));
                return false;
            }

            var r = await _sysPostService.Add(Map<SysPostParm>(PostModel));
            if (r.Success)
            {
                Show("提示", r.Message);
                return true;
            }
            return false;
        }
        async Task<bool> Modif()
        {
            var r = await _sysPostService.Modif(Map<SysPostParm>(PostModel));
            if (r.Success)
            {
                Show("提示", r.Message);
                return true;
            }
            return false;
        }
        #endregion



        #region Override
        public override void Cancel()
        {
            OnDialogClosed(ButtonResult.No);
        }

        public override async void OnSave()
        {
            await SetBusyAsync(async () =>
            {
                await Task.Delay(1000);
                if (IsEdit)
                {
                    if (!await Modif())
                        return;
                }
                else
                {
                    if (!await Add())
                        return;
                }
                OnDialogClosed(ButtonResult.Yes);
            });
        }

        public override  void OnDialogOpened(IDialogParameters parameters)
        {
            base.OnDialogOpened(parameters);
            IsEdit = true;
            var Model = parameters.GetValue<SysPostModel>("Param");
            if (Model == null)
            {
                PostModel = new SysPostModel();
                IsEdit = false;
            }
            else
            {
                PostModel = DeepCopy<SysPostModel>(Model);
            }
        }
        #endregion
    }
}
