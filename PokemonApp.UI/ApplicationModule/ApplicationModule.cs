using KVerbruggen.Framework.Maui.ViewModels;
using KVerbruggen.Framework.Models;
using PokemonApp.Client.Shared.Models;
using PokemonApp.Client.Shared.ViewModels;
using PokemonApp.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PokemonApp.UI.ApplicationModule
{
    /// <summary>
    /// A module defining the services used by the application
    /// </summary>
    public class ApplicationModule : Module
    {
        /// <summary>
        /// Add the application services to the service container builder
        /// </summary>
        /// <param name="builder">The service container builder</param>
        protected override void Load(ContainerBuilder builder)
        {
            // builder.RegisterType<AuthorizationHelper>().As<IAuthorizationHelper>().SingleInstance();

            RegisterModels(builder);
            RegisterViewModels(builder);
            RegisterViews(builder);

            // Register the view model provider
            builder.Register(componentContext =>
            {
                ILifetimeScope currentScope = componentContext.Resolve<ILifetimeScope>();
                return new ViewModelProvider(currentScope);
            }).As<IViewModelProvider>().InstancePerDependency();

            RegisterNavigation(builder);
        }

        /// <summary>
        /// Register the view models
        /// </summary>
        /// <param name="builder">The service container builder</param>
        private static void RegisterViewModels(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(MainViewModel).Assembly).AssignableTo(typeof(IViewModel)).AsImplementedInterfaces().AsSelf().InstancePerDependency();
            builder.RegisterAssemblyTypes(typeof(ProfileViewModel).Assembly).AssignableTo(typeof(IViewModel)).AsImplementedInterfaces().AsSelf().InstancePerDependency();
        }

        /// <summary>
        /// Register the models
        /// </summary>
        /// <param name="builder">The service container builder</param>
        private static void RegisterModels(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(MainModel).Assembly).AssignableTo(typeof(IModelBase)).AsImplementedInterfaces().AsSelf().InstancePerDependency();
        }

        /// <summary>
        /// Register the views
        /// </summary>
        /// <param name="builder">The container builder</param>
        private static void RegisterViews(ContainerBuilder builder)
        {
            builder.RegisterType<MainPage>().AsSelf();
            builder.RegisterType<SelectProfilePage>().AsSelf();
        }

        /// <summary>
        /// Register the platform-specific navigation service
        /// </summary>
        /// <param name="builder"></param>
        private static void RegisterNavigation(ContainerBuilder builder)
        {
#if WINDOWS
            builder.RegisterType<KVerbruggen.Framework.Maui.Platforms.Windows.NavigationService>().As<INavigationService>().InstancePerLifetimeScope();
#endif

            builder.RegisterType<ViewProvider<MainViewModel, MainPage>>().AsImplementedInterfaces();
            builder.RegisterType<ViewProvider<ProfileViewModel, SelectProfilePage>>().AsImplementedInterfaces();
        }
    }
}
