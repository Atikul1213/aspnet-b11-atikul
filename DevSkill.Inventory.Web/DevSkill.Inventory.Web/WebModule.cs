using Autofac;

namespace DevSkill.Inventory.Web
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // builder.RegisterType<Item>().As<IItem>().InstancePerLifetimeScope();
            //builder.RegisterType<Item>().AsSelf().InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}
