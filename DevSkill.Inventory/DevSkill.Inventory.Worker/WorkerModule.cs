using Autofac;

namespace DevSkill.Inventory.Worker
{
    public class WorkerModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;
        public WorkerModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void Load(ContainerBuilder builder)
        {

            base.Load(builder);
        }
    }
}
