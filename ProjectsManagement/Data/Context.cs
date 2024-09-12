using Microsoft.EntityFrameworkCore;

namespace ProjectsManagement.Data
{
    public class Context: DbContext
    {
        public Context(DbContextOptions<Context> option) : base(option)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
    }
}
