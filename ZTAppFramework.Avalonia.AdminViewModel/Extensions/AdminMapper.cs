using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTAppFramework.ApplicationService.Stared;
using ZTAppFramework.ApplicationService.Stared.Operator.Dto;
using ZTAppFramework.Avalonia.AdminViewModel.Model;
using ZTAppFramework.Avalonia.Stared.ViewModels;
using ZTAppFramewrok.Application.Stared;

namespace ZTAppFramework.Avalonia.AdminViewModel
{
    public class AdminMapper: Profile
    {
        public AdminMapper()
        {
            CreateMap<LoginModel, LoginParam>()
              .ForMember(X => X.Account, opt => opt.MapFrom(str => str.UserName))
              .ForMember(X => X.Password, opt => opt.MapFrom(str => str.Password))
              .ReverseMap();

            CreateMap<MenuModel, SysMenuDto>().ReverseMap();
            CreateMap<DeviceUseModel, DeviceUseDto>().ReverseMap();
            CreateMap<MachineInfoModel, MachineInfoDto>().ReverseMap();
            CreateMap<OperatorWorkModel, OperatorWordDto>().ReverseMap();
        //    CreateMap<UserEditPwdModel, OperatroPasswordParam>().ReverseMap();
            CreateMap<SysOrganizeModel, SysOrganizeDto>().ReverseMap();
            CreateMap<SysOrganizeModel, SysOrganizeParm>().ReverseMap();
        }
    }
}
