using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTAppFramework.Avalonia.AdminViewModel.Model.Login
{
    public class LoginModel: BindableBase
    {

        private string _UserName;//用户

        private string _Password;//密码

        public LoginModel()
        {
            UserName = "";
            Password = "";
        }

        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; RaisePropertyChanged("UserName"); }
        }

        public string Password
        {
            get { return _Password; }
            set { _Password = value; RaisePropertyChanged("Password"); }
        }

    }
}
