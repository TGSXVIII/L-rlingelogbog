using API.Migrations;

namespace API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<API.Models.Users> Users { get; set; }
        public DbSet<API.Models.Education> Educations { get; set; }
        public DbSet<API.Models.EducationalStandarts> EducationalStandarts { get; set; }
        public DbSet<API.Models.Tasks> Tasks { get; set; }
        public DbSet<API.Models.PicturesAndVideos> PicturesAndVideos { get; set; }
        public DbSet<API.Models.Users_Education> Users_Education { get; set; }
    }
}
