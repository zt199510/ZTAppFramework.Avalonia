using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTAppFramework.ApplicationService.Stared;
using ZTAppFramework.ApplicationService.Stared.Attributes;
using ZTAppFramework.ApplicationService.Stared.Commom;
using ZTAppFramework.ApplicationService.Stared.HttpManager;
using ZTAppFramewrok.Application.Stared;


namespace ZTAppFramework.ApplicationService.Service
{
    /// <summary>
    ///********************************************
    /// 创建人        ：  ZT
    /// 创建时间    ：  2022/10/12 10:23:13 
    /// Description   ：  日志服务
    ///********************************************/
    /// </summary>
    public class SysLogSerivce : AppServiceBase
    {
        public SysLogSerivce(ApiClinetRepository apiClinet) : base(apiClinet)
        {
        }

        public override string ApiServiceUrl => "/api/SysLog";
      

        [ApiUrl("Pages")]
        public async Task<AppliResult<PageResult<SysLogDto>>> GetPostList(PageParam pageParam)
        {
            AppliResult<PageResult<SysLogDto>> result = new AppliResult<PageResult<SysLogDto>>();
            var api = await _apiClinet.GetAsync<PageResult<SysLogDto>>(GetEndpoint(), pageParam);
            if (api.success && api.Code == 200)
            {
                result.Success = true;
                result.Message = api.message;
                result.data = api.data;
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
