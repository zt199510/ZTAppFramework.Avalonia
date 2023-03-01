using Prism.Ioc;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTAppFramework.Avalonia.AdminViewModel.Model.Login;
using ZTAppFramework.Avalonia.Stared.ViewModels;

namespace ZTAppFramework.Avalonia.AdminViewModel.ViewModel
{
    public class LoginViewModel : DialogViewModelBase
    {
        #region UI
        private ObservableCollection<LoginModel> _AccountList;
        private LoginModel _Login;
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

        public override void OnDialogOpened(IDialogParameters parameters)
        {
            base.OnDialogOpened(parameters);
            AccountList=new ObservableCollection<LoginModel>();
            AccountList.Add(new LoginModel() { UserName = "1231231", Password = "1231231" });
        }
    }
}
