using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTAppFramework.ApplicationService.Service;
using ZTAppFramework.ApplicationService.Stared;
using ZTAppFramework.ApplicationService.Stared.HttpManager;

namespace ZTAppFramework.Avalonia.AdminViewModel
{
    public static class ServicesManagers
    {
        public static void RegisterApplicationManager(this IContainerRegistry services)
        {
            services.RegisterSingleton<AccessTokenManager>();
            services.RegisterSingleton<ApiClinetRepository>();

            services.RegisterScoped<SysAdminService>();
            services.RegisterScoped<CaptchaService>();
            services.RegisterScoped<MenuService>();
            services.RegisterScoped<WorkbenchService>();
            services.RegisterScoped<OrganizeService>();
            services.RegisterScoped<RoleService>();
            services.RegisterScoped<SysPostService>();
            //services.RegisterScoped<SysLogSerivce>();

        }
    }
}
