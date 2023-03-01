using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTAppFramework.Application.Stared.Attributes;
using ZTAppFramework.Application.Stared.HttpManager;

namespace ZTAppFramework.App.Service.Base
{
    internal class AppServiceRepository : AppServiceBase
    {
        public AppServiceRepository(ApiClinetRepository apiClinet) : base(apiClinet)
        {

        }

        [ApiUrl("")]
        public async Task<AppliResult<bool>> Delete(string Id)
        {
            AppliResult<bool> result = new AppliResult<bool>();
            var api = await _apiClinet.DeleteAsync<bool>(GetEndpoint(), new { Ids = Id });
            if (api.success && api.Code == 200)
            {
                result.Success = true;
                result.data = api.data;
                result.Message = api.message;
            }
            else
            {
                result.Success = false;
                result.Message = api.message;
            }
            return result;
        }
    }
}
