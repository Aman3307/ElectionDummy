using ElectionDummy.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ElectionDummy
{
    public class ElectionDummyDbContext : DbContext
    {
        public ElectionDummyDbContext(DbContextOptions<ElectionDummyDbContext> options)
            : base(options)
        {
        }

        public DbSet<Block> Block { get; set; }
        public DbSet<BlockPresident> BlockPresident { get; set; }
        public DbSet<BlockVicePresident> BlockVicePresident { get; set; }
        public DbSet<Panchayat> Panchayat { get; set; }
        public DbSet<PanchayatPresident> PanchayatPresident { get; set; }
        public DbSet<PanchayatVicePresident> PanchayatVicePresident { get; set; }
        public DbSet<Village> Village { get; set; }
        public DbSet<VillagePresident> VillagePresident { get; set; }
        public DbSet<VillageVicePresident> VillageVicePresident { get; set; }
        public DbSet<PollingBooth> PollingBooth { get; set; }
        public DbSet<PollingBoothAgent> PollingBoothAgent { get; set; }
        // Add other DbSet properties for your entities

        // Override OnModelCreating if you need to configure the model further
       // protected override void OnModelCreating(ModelBuilder modelBuilder)
       // {
       //     modelBuilder.Entity<Block>().HasMany(b => b.BlockPresident)
       //.WithOne(bp => bp.Block)
       //.HasForeignKey(bp => bp.BlockId);

       //     modelBuilder.Entity<Block>()
       //         .HasMany(b => b.BlockVicePresident)
       //         .WithOne(vp => vp.Block)
       //         .HasForeignKey(vp => vp.BlockId);

       //     modelBuilder.Entity<Block>()
       //         .HasMany(b => b.Panchayat)
       //         .WithOne(p => p.Block)
       //         .HasForeignKey(p => p.BlockId);
       // }
    }
}

