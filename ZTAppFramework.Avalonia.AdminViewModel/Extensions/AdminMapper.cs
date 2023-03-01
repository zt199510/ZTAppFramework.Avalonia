using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTAppFramework.ApplicationService.Stared;
using ZTAppFramework.Avalonia.AdminViewModel.Model.Login;
using ZTAppFramework.Avalonia.Stared.ViewModels;

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
        }
    }
}
