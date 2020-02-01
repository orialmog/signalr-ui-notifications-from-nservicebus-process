using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using NServiceBus;

namespace WaitForEventLocking
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            StartSendOnlyHosting();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            DateTime.Now.Date.ToString("{0:M}");
        }

        private void StartSendOnlyHosting()
        {
            var builder = new ContainerBuilder();

            // Register the MVC controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();

            var busConfiguration = new BusConfiguration();

            busConfiguration.EndpointName("UINotificationFromLongRunningProcess");
            // ExistingLifetimeScope() ensures that IBus is added to the container as well,
            // allowing resolve IBus from the components.
            busConfiguration.UseContainer<AutofacBuilder>(
                customizations: customizations =>
                {
                    customizations.ExistingLifetimeScope(container);
                });
            busConfiguration.UsePersistence<InMemoryPersistence>();
            busConfiguration.EnableInstallers();

            var bus = Bus.CreateSendOnly(busConfiguration);

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
