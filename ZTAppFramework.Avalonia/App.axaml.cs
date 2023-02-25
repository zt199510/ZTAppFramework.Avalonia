using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using ReactiveUI;
using ZTAppFramework.Avalonia.Admin;

namespace ZTAppFramework.Avalonia
{
    public partial class App : PrismApplication
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            base.Initialize();

           
        }
        public override void RegisterServices()
        {
            base.RegisterServices();
        }
        protected override IAvaloniaObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
           
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<AdminModule>();
        }
    }
}
