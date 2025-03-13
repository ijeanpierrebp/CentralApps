using Microsoft.EntityFrameworkCore;

namespace ToDoPower.Models;

public class Seed
{
    public static void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Models.Task>().HasData(
            new Models.Task
            {
                TaskId = new Guid(Guid.NewGuid().ToString()),
                TaskName = "Learn French",
                TaskDescription = "Become a francophone",
                TenantName = "CloudSphere"
            },
            new Models.Task
            {
                TaskId = new Guid(Guid.NewGuid().ToString()),
                TaskName = "Run a marathon",
                TaskDescription = "Get really fit",
                TenantName = "CloudSphere"
            },
            new Models.Task
            {
                TaskId = new Guid(Guid.NewGuid().ToString()),
                TaskName = "Write every day",
                TaskDescription = "Finish your book project",
                TenantName = "CloudSphere"
            }
        );
    }
}
