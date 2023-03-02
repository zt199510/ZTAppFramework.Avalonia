using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTAppFramework.Avalonia.AdminViewModel
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
            set { _UserName = value; RaisePropertyChanged(); }
        }

        public string Password
        {
            get { return _Password; }
            set { _Password = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 验证码
        /// </summary>
        private string _Code;

        public string Code
        {
            get { return _Code; }
            set { _Code = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 验证码Key
        /// </summary>

        private string _CodeKey = "12312312";

        public string CodeKey
        {
            get { return _CodeKey; }
            set { _CodeKey = value; RaisePropertyChanged(); }
        }
    }
}
