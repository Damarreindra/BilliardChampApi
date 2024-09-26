using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using billiardchamps.Model;
using billiardchamps.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace billiardchamps.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
        {

        }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<MatchPlayer> MatchPlayers {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // This line ensures the Identity configuration is applied
            base.OnModelCreating(modelBuilder);

            // Your custom configurations here
            modelBuilder.Entity<Player>()
                .HasIndex(p => p.Username)
                .IsUnique();

            modelBuilder.Entity<MatchPlayer>()
      .HasKey(mp => new { mp.MatchId, mp.PlayerId });  // Composite key for the join table

            modelBuilder.Entity<MatchPlayer>()
                .HasOne(mp => mp.Match)
                .WithMany(m => m.MatchPlayers)
                .HasForeignKey(mp => mp.MatchId);

            modelBuilder.Entity<MatchPlayer>()
                .HasOne(mp => mp.Player)
                .WithMany(p => p.MatchPlayers)
                .HasForeignKey(mp => mp.PlayerId);
        }

    }
}