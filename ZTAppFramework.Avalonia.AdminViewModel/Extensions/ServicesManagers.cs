using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTAppFramework.ApplicationService.Service;
using ZTAppFramework.ApplicationService.Stared;

namespace ZTAppFramework.Avalonia.AdminViewModel
{
    public static class ServicesManagers
    {

        public static void RegisterApplicationManager(this IContainerRegistry services)
        {
            services.RegisterScoped<AdminService>();
            services.RegisterScoped<CaptchaService>();
            services.RegisterScoped<MenuService>();
            services.RegisterScoped<WorkbenchService>();
            services.RegisterScoped<OrganizeService>();
            //services.RegisterScoped<RoleService>();
            //services.RegisterScoped<SysPostService>();
            //services.RegisterScoped<SysLogSerivce>();
        }

        /// <summary>
        /// 注册验证
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterValidator(this IContainerRegistry services)
        {
            //services.RegisterSingleton<GlobalValidator>();

            //services.RegisterScoped<IValidator<UserLoginModel>, UserLoginValidator>();
            //services.RegisterScoped<IValidator<UserEditPwdModel>, UserEditPwdValidator>();
            //services.RegisterScoped<IValidator<SysOrganizeParm>, SysOrganizeParmValidator>();
            //services.RegisterScoped<IValidator<SysRoleParm>, SysRoleParmValidator>();
            //services.RegisterScoped<IValidator<SysPostParm>, SysPostParmValidator>();
            //services.RegisterScoped<IValidator<SysAdminModel>, SysAdminParmValidator>();


        }

    }
}
