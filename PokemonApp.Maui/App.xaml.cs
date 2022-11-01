using Autofac;
using Autofac.Configuration;
using KVerbruggen.Framework;
using KVerbruggen.Framework.Services.SynchronizationHelper;
using Microsoft.Extensions.Configuration;
using PokemonApp.Maui.Modules;
using IContainer = Autofac.IContainer;

namespace PokemonApp.Maui
{
    public partial class App : Application
    {
        #region Private declarations

        /// <summary>
        /// The current service container;
        /// </summary>
        private IContainer _container;

        #endregion

        #region Constructor

        public App()
        {
            InitializeComponent();
        }

        #endregion

        #region OnStartup

        /// <summary>
        /// Start the application
        /// </summary>
        protected override void OnStart()
        {
            // Setup the configuration provider
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration configuration = configurationBuilder.Build();

            // Setup the Dependency Injection container
            ContainerBuilder containerBuilder = new();
            containerBuilder.Register(context => configuration);

            // Register the options and modules to the Dependency Injection container
            ConfigureOptions(containerBuilder, configuration);
            RegisterModules(containerBuilder, configurationBuilder);

            // Build the dependency injection container
            _container = containerBuilder.Build();

            // Set the app language
            "en-US".SetAsAppLanguage();

            // Create a synchronization context
            SynchronizationHelper.Instance.SetSynchronizationContext();

            // Open the main window and fire the startup events
            //OpenMainWindow();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Open the main window
        /// </summary>
        // private void OpenMainWindow()
        // {
        //     IViewModelProvider viewModelProvider = LifetimeScope.Resolve<IViewModelProvider>();
        // 
        //     MainViewModel mainViewModel = viewModelProvider.Provide<MainViewModel>();
        //     MainPage = LifetimeScope.Resolve<MainPage>();
        //     MainPage.BindingContext = mainViewModel;
        // }

        /// <summary>
        /// Register the AutoFac modules
        /// </summary>
        /// <param name="containerBuilder"></param>
        /// <param name="configurationBuilder"></param>
        private static void RegisterModules(ContainerBuilder containerBuilder, IConfigurationBuilder configurationBuilder)
        {
            // Register the configuration module
            ConfigurationModule module = new(configurationBuilder.Build());
            containerBuilder.RegisterModule(module);

            // Register the Autofac modules
            containerBuilder.RegisterModule<ApplicationModule>();
        }

        /// <summary>
        /// Register all necessary options classes to the dependency injection container
        /// </summary>
        /// <param name="containerBuilder">The dependency injection container to bind the options classes to</param>
        /// <param name="configuration">The configuration instance to bind to the options classes</param>
        private static void ConfigureOptions(ContainerBuilder containerBuilder, IConfiguration configuration)
        {
            // Register options
        }

        #endregion
    }
}