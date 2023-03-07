using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTAppFramework.ApplicationService.Stared;
using ZTAppFramework.ApplicationService.Stared.Attributes;
using ZTAppFramework.ApplicationService.Stared.HttpManager;
using ZTAppFramewrok.Application.Stared;

namespace ZTAppFramework.ApplicationService.Service
{
    public class RoleService : AppServiceRepository<SysRoleDto>
    {
    
        public override string ApiServiceUrl => "/api/SysRole";
        public RoleService(ApiClinetRepository apiClinet) : base(apiClinet)
        {
        }

        public override Task<AppliResult<bool>> Modif<SysRoleParm>(SysRoleParm Param)
        {
            return base.Modif(Param);
        }

        public override Task<AppliResult<bool>> Add<SysRoleParm>(SysRoleParm Param)
        {
            return base.Add(Param);
        }
    }
}
