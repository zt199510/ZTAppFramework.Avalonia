using AutoMapper;
using AutoMapper.Configuration;
using AutoMapper.Execution;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ZTAppFramework.Avalonia.Stared.AutoMapper
{
    public interface IAutoMapperProvider
    {
        IMapper GetMapper();
        void Addprofile(params Profile[] profile);
    }

    public class AutoMapperProvider : IAutoMapperProvider
    {
        private readonly IContainerProvider _container;
        private MapperConfiguration _configuration;



        public AutoMapperProvider(IContainerProvider container)
        {
            _container = container;

        }

        public void Addprofile(params Profile[] profile)
        {
            _configuration = new MapperConfiguration(configure =>
            {
                configure.ConstructServicesUsing(_container.Resolve);

                //扫描profile文件
                configure.AddProfiles(profile);
            });
        }

        public IMapper GetMapper()
        {
            return _configuration.CreateMapper();
        }
    }
}
