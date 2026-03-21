using Microsoft.EntityFrameworkCore;
using MusicBased_IOT_Platform.Models;

namespace MusicBased_IOT_Platform.Data
{
    public class AppDBContext(DbContextOptions<AppDBContext> options) : DbContext(options)
    {
        public DbSet<UserAccount> Users { get; set; }
    }
}
