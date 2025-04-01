using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TeslaACDC.Data.Models;

public class NikolaContext : IdentityDbContext<ApplicationUser>
{
    public NikolaContext(DbContextOptions<NikolaContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    public DbSet<Album> Albums { get; set; }
    public DbSet<Artist> Artists { get; set; }
    public DbSet<Track> Tracks { get; set; }    

    protected override void OnModelCreating(ModelBuilder builder)
    {
         if(builder == null)
         {
            return;
         }

         builder.Entity<Album>().ToTable("Album").HasKey(k => k.Id);
         builder.Entity<Artist>().ToTable("Artist").HasKey(k => k.Id);
         builder.Entity<Track>().ToTable("Track").HasKey(k => k.Id);
         base.OnModelCreating(builder);
    }
}