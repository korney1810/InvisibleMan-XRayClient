namespace InvisibleManXRay.Managers.Initializers
{
    using Models;
    using Managers;
    using Services;
    using Handlers;

    public class ServicesInitializer
    {
        public ServicesManager ServicesManager { get; private set; }

        public void Register()
        {
            ServicesManager = new ServicesManager();
            ServicesManager.AddService(new AnalyticsService());
        }

        public void Setup(HandlersManager handlersManager)
        {
            SetupServiceLocator();
            SetupAnalyticsService();

            void SetupServiceLocator()
            {
                ServiceLocator.Setup(
                    servicesManager: ServicesManager
                );
            }

            void SetupAnalyticsService()
            {
                VersionHandler versionHandler = handlersManager.GetHandler<VersionHandler>();
                ServicesManager.GetService<AnalyticsService>().Setup(
                    getApplicationVersion: GetApplicationVersion
                );

                string GetApplicationVersion()
                {
                    AppVersion appVersion = versionHandler.GetApplicationVersion();
                    return $"{appVersion.Major}.{appVersion.Feature}.{appVersion.BugFix}";
                }
            }
        }
    }
}