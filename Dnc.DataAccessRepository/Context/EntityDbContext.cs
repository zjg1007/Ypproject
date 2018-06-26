using Dnc.Entities.Application;
using Dnc.Entities.Business;
using Microsoft.EntityFrameworkCore;

namespace Dnc.DataAccessRepository.Context
{
    public class EntityDbContext:DbContext
    {
        public EntityDbContext(DbContextOptions<EntityDbContext> options)
            : base(options){}

        public DbSet<Actor> Actor { get; set; }
        public DbSet<Cinema> Cinema { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Deal> Deal { get; set; }
        public DbSet<Home> Home { get; set; }
        public DbSet<Launch> Launch { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<Readyplay> Readyplay { get; set; }
        public DbSet<MType> MType { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<ApplicationGroup> ApplicationGroup { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            {
                //optionsBuilder.UseSqlServer(
                //    @"Server=SC-201804102054;Initial Catalog=CPMD_Team20140208; uid=sa;pwd=123;MultipleActiveResultSets=True");
                //base.OnConfiguring(optionsBuilder);
                optionsBuilder.UseMySQL(@"Server=47.98.212.255;port=3306;database=cpmd_team20140208;uid=zjg;pwd=123456;SslMode=None");
                base.OnConfiguring(optionsBuilder);
            }
        }
    }
}
