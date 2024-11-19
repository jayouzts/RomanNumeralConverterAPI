using Microsoft.EntityFrameworkCore;

namespace LogDBContext
{

    public class LoggingDBContext : DbContext
    {
        public DbSet<RMLoggingModel> RMLoggings { get; set; }

        public LoggingDBContext(DbContextOptions<LoggingDBContext> options)
            : base(options)
        {
        }
               
    }
}

