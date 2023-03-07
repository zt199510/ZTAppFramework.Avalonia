using FluentValidation;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTAppFramework.ApplicationService.Stared;
using ZTAppFramework.Avalonia.AdminViewModel.Validations;
using ZTAppFramework.Avalonia.Stared.Validations;

namespace ZTAppFramework.Avalonia.AdminViewModel.Extensions
{
    public static class AdminValidatorExtensions
    {

        /// <summary>
        /// 注册验证
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterValidator(this IContainerRegistry services)
        {
            services.RegisterSingleton<GlobalValidator>();
            services.RegisterScoped<IValidator<SysOrganizeParm>, SysOrganizeParmValidator>();
            services.RegisterScoped<IValidator<SysRoleParm>, SysRoleParmValidator>();
            services.RegisterScoped<IValidator<SysPostParm>, SysPostParmValidator>();
            //services.RegisterScoped<IValidator<SysAdminModel>, SysAdminParmValidator>();


        }
    }
}
