using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTAppFramework.Application_Stared.HttpManager;

namespace ZTAppFramework.Application_Stared
{
    public class AccessTokenManager
    {
        public AccessTokenManager()
        {
            AuthenticateResult = new AuthenticateResultModel();
        }
        public OperatorUser userInfo { get; set; }
        public AuthenticateResultModel AuthenticateResult { get; set; }
        public string GetAccessToken()
        {
            if (AuthenticateResult == null)
            {
                throw new ApplicationException("You have to authenticate first!");
            }

            return AuthenticateResult.AccessToken;
        }
    }
}
