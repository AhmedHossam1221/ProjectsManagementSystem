using Autofac;
using ProjectsManagement.Data;
using ProjectsManagement.Repositories;

namespace ProjectsManagement
{
    public class AutoFacModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Context>().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();


        }
    }
}
