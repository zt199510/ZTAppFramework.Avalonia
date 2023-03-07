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
    /// 
    /// </summary>
    public class SysPostService : AppServiceRepository<SysPostDto>
    {
        public SysPostService(ApiClinetRepository apiClinet) : base(apiClinet)
        {
        }

        public override string ApiServiceUrl => "/api/SysPost";
  

        [ApiUrl("Pages")]
        public async Task<AppliResult<PageResult<SysPostDto>>> GetPostList(PageParam pageParam)
        {
            AppliResult<PageResult<SysPostDto>> result = new AppliResult<PageResult<SysPostDto>>();
            var api = await _apiClinet.GetAsync<PageResult<SysPostDto>>(GetEndpoint(), pageParam);
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

        public override Task<AppliResult<bool>> Add<SysPostParm>(SysPostParm Param)
        {
            return base.Add(Param);
        }

        public override Task<AppliResult<bool>> Modif<SysPostParm>(SysPostParm Param)
        {
            return base.Modif(Param);
        }

      

    
    }
}
