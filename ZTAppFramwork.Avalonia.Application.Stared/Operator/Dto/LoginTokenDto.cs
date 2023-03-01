using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTAppFramework.ApplicationService.Stared
{
    public class LoginTokenDto
    {
        public string accessToken { get; set; }

        public OperatorUser userInfo { get; set; }
    }
}
