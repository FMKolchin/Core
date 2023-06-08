using _4.Models;
using System.Data.Entity;

namespace _4
{
    public class Context : DbContext
    {
        public Context()
            :base("data source=sql;initial catalog=TaskManagementProject;intergrated security=True") 
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<TaskTODO> TaskTODOs { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<Context>(
                new DropCreateDatabaseIfModelChanges<Context>());
            base.OnModelCreating(modelBuilder);
        }
    }
}
