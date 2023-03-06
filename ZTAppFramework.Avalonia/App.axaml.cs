using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;
using Avalonia.Threading;
using DryIoc;
using DryIoc.Microsoft.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Services.Dialogs;
using ReactiveUI;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using ZTAppFramework.Avalonia.Admin;
using ZTAppFramework.Avalonia.Admin.Views;
using ZTAppFramework.Avalonia.Admin.Windows;
using ZTAppFramework.Avalonia.AdminViewModel;
using ZTAppFramework.Avalonia.Stared;
using ZTAppFramework.Template.Dialog;
using ZTAppFramework.Template.Fonts;

namespace ZTAppFramework.Avalonia
{
    public partial class App : PrismApplication
    {

        protected override IAvaloniaObject CreateShell() => null;
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            base.Initialize();
          
        }

        public override void RegisterServices()
        {
            AvaloniaLocator.CurrentMutable.Bind<IFontManagerImpl>().ToConstant(new CustomFontManagerImpl());
            base.RegisterServices();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance<IZTDialogService>(new ZTDialogService());
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<AdminModule>();
        }


        protected override IContainerExtension CreateContainerExtension()
        {

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddAutoMapper(config =>
            {
                config.AddProfile<AdminMapper>();
            });
            return new DryIocContainerExtension(new Container(CreateContainerRules()).WithDependencyInjectionAdapter(serviceCollection));
        }
        
        protected override  void OnInitialized()
        {
            var dialogService = ContainerLocator.Container.Resolve<IDialogService>();
            dialogService.Show(AppViews.LoginName, new DialogParameters(), r =>
            {
                if (r.Result == ButtonResult.Yes)
                {
                    var appStart = Container.Resolve<AppStartService>();
                    InitializeShell(appStart.CreateShell(this));
                    OnFrameworkInitializationCompleted();
                    base.OnInitialized();
                }
                else
                {
                    AppStartService.ExitApplication();
                }
            }, "DefaultWindow");
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            }
            base.OnFrameworkInitializationCompleted();
        }
    }
}
