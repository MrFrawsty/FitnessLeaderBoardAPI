using FitnessLeaderBoardAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FitnessLeaderBoardAPI.Data
{
    public class FitnessLeaderboardDbContext : DbContext
    {
        public FitnessLeaderboardDbContext(DbContextOptions options) : base(options) { }
        
        public DbSet<ActivityModel> Activities { get; set; }

        public DbSet<UserModel> Users { get; set; }
    }
}
