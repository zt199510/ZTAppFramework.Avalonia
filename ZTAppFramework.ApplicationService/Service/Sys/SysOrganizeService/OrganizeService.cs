using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTAppFramework.ApplicationService.Stared.Attributes;
using ZTAppFramework.ApplicationService.Stared.HttpManager;
using ZTAppFramework.ApplicationService.Stared;
using ZTAppFramewrok.Application.Stared;

namespace ZTAppFramework.ApplicationService.Service
{
    /// <summary>
    ///********************************************
    /// 创建人        ：  ZT
    /// 创建时间      ：  2022/9/7 8:26:15 
    /// Description   ：  组织管理服务
    ///********************************************/
    /// </summary>
    public class OrganizeService : AppServiceRepository<SysOrganizeDto>
    {

        public override string ApiServiceUrl => "/api/SysOrganize";
        public OrganizeService(ApiClinetRepository apiClinet) : base(apiClinet)
        {


        }
        public override Task<AppliResult<bool>> Modif<SysOrganizeDto>(SysOrganizeDto Param)
        {
            return base.Modif(Param);
        }

        public override Task<AppliResult<bool>> Add<SysOrganizeParm>(SysOrganizeParm Param)
        {
            return base.Add(Param);
        }

    }
}
