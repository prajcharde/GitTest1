using Assignment7.Service;
using Assignment7.Repository.Infrastructure;
using Assignment7.Repository.Repository;
using Autofac;
using Autofac.Integration.Mvc;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace Assignment7
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            var builder = new ContainerBuilder();
            builder.RegisterType<IUnitOfWork>().As<IUnitOfWork>().InstancePerRequest();


            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().InstancePerRequest();

            // Register your repositories all at once using assembly scanning
            builder.RegisterAssemblyTypes(typeof(EmployeeRepository).Assembly).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterAssemblyTypes(typeof(EmployeeService).Assembly).Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);


            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            // OPTIONAL: Enable action method parameter injection (RARE).
            /* builder.InjectActionInvoker();*/

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
