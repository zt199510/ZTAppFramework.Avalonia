using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTAppFramework.ApplicationService.Service;
using ZTAppFramework.ApplicationService.Stared;
using ZTAppFramework.Avalonia.Stared.ViewModels;

namespace ZTAppFramework.Avalonia.AdminViewModel.ViewModel
{
    public class LoginViewModel : DialogViewModelBase
    {
        #region UI
        private ObservableCollection<LoginModel> _AccountList;
        private LoginModel _Login = new LoginModel();
        private bool _IsSavePwd;
        public ObservableCollection<LoginModel> AccountList
        {
            get { return _AccountList; }
            set { SetProperty(ref _AccountList, value); }
        }

        public LoginModel Login
        {
            get { return _Login; }
            set { SetProperty(ref _Login, value); }
        }

        public bool IsSavePwd
        {
            get { return _IsSavePwd; }
            set { SetProperty(ref _IsSavePwd, value); }
        }

        #endregion

        #region Command

        public DelegateCommand<string> ExecuteCommand => new DelegateCommand<string>(Execute);


        #endregion
        #region 服务
        private readonly AdminService _userLoginService;
        private readonly CaptchaService _captchaService;
        #endregion

        public LoginViewModel(AdminService userLoginService, CaptchaService captchaService)
        {
            _userLoginService = userLoginService;
            _captchaService = captchaService;
        }
        #region Command事件
         void Execute(string Parm)
        {
            switch (Parm)
            {
                case "Login":
                     LoginUserAsync();
                    break;
                default:
                    break;
            }
        }

        private  async void LoginUserAsync()
        {
          
            await SetBusyAsync(async () =>
            {
                var res = await _captchaService.GetCaptchaAsync(Login.CodeKey);
                if (res.Success)
                    Login.Code = res.data;
                else
                {
                   // ShowDialog("消息", res.Message);
                    return;
                }
             //   if (!Verify(Login).IsValid) return;
                res = await _userLoginService.LoginServer(Map<LoginParam>(Login));
                if (!res.Success)
                {
                  //  ShowDialog("消息", res.Message);
                    return;
                }
                await _userLoginService.SaveLocalAccountInfo(IsSavePwd, Map<LoginParam>(Login));
                RaiseRequestClose(new DialogResult(ButtonResult.Yes));
            });
           
        }
        #endregion


        public override async void OnDialogOpened(IDialogParameters parameters)
        {
            base.OnDialogOpened(parameters);
            AccountList = new ObservableCollection<LoginModel>();
            var result = await _userLoginService.GetLocalAccountList();
            if (result.Success)
                AccountList.AddRange(Map<List<LoginModel>>(result.data));

            var r = await _userLoginService.GetLocalAccountInfo();
            if (r.Success && !string.IsNullOrEmpty(r.data.Values))
            {
                IsSavePwd = r.data.Check;
                var mod = AccountList.FirstOrDefault(x => x.UserName == r.data.Values);
                Login.UserName = mod.UserName;
                if (IsSavePwd)
                    Login.Password = mod.Password;
            }
        }

        public override void Cancel()
        {
          
        }

        public override void OnSave()
        {
           
        }
    }
}
