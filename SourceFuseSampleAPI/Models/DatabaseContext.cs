using Microsoft.EntityFrameworkCore;

namespace SourceFuseSampleAPI.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
        {
        }

        #region MySql connector
        /// <summary>
        /// Overriding the MySQL connection and sets the connection string to open connection
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("host=127.0.0.1; port=3306; database=Customers; user=root; password=mysql;");
        }
        #endregion

        #region Database tables under DB context
        public DbSet<CustomerMaster> CustomerMaster { get; set; } = null!;
        public DbSet<OrderHeader> OrderHeader { get; set; } = null!;

        #endregion
    }
}
